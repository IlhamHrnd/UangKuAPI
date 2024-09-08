using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using static UangKuAPI.BusinessObjects.Helper.Helper;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using static UangKuAPI.BusinessObjects.Helper.Converter;
using static UangKuAPI.BusinessObjects.Helper.AppConstant;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Entity;
using System.Data;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserReportAPI")]
    [ApiController]
    public class UserReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetNewReportNo", Name = "GetNewReportNo")]
        public IActionResult GetNewReportNo([FromQuery] string TransType)
        {
            try
            {
                if (string.IsNullOrEmpty(TransType))
                {
                    return BadRequest($"Trans Type Is Required");
                }
                string transDate = DateFormat.DateTimeNow(Shortyearpattern, DateTime.Now);
                int number = 1;
                string formattedNumber = NumberingFormat(number, "D3");
                string transNo = $"RPT/{TransType}/{transDate}-{formattedNumber}";

                while (_context.Report.Any(r => r.ReportNo == transNo))
                {
                    number++;
                    formattedNumber = NumberingFormat(number, "D3");
                    transNo = $"RPT/{TransType}/{transDate}-{formattedNumber}";
                }

                number++;

                return Ok(transNo);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("PostUserReport", Name = "PostUserReport")]
        public async Task<IActionResult> PostUserReport([FromBody] UserReport2 report)
        {
            try
            {
                if (report == null)
                {
                    return BadRequest($"Report Are Required");
                }

                var addUser = await _context.Report
                    .FirstOrDefaultAsync(ur => ur.ReportNo == report.ReportNo);

                if (addUser != null)
                {
                    return BadRequest($"{report.ReportNo} Already Exist");
                }

                var newReport = new UserReport
                {
                    ReportNo = report.ReportNo, DateErrorOccured = report.DateErrorOccured, SRErrorLocation = report.SRErrorLocation,
                    ErrorCronologic = report.ErrorCronologic, Picture = StringToByte(report.Picture), CreatedDateTime = DateFormat.DateTimeNow(),
                    CreatedByUserID = report.CreatedByUserID, LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserID = report.LastUpdateByUserID,
                    PersonID = report.PersonID, SRReportStatus = "ReportStatus-001", SRErrorPossibility = report.SRErrorPossibility
                };
                _context.Report.Add(newReport);

                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Ok($"User {report.ReportNo} Created Successfully")
                    : BadRequest($"Failed To Insert Data For ReportNo {report.ReportNo}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchUserReport", Name = "PatchUserReport")]
        public async Task<IActionResult> PatchUserReport([FromBody] UserReport2 report)
        {
            try
            {
                if (string.IsNullOrEmpty(report.ReportNo))
                {
                    return BadRequest($"Report No Is Required");
                }

                var _report = await _context.Report
                    .FirstOrDefaultAsync(ur => ur.ReportNo == report.ReportNo);

                if (report == null)
                {
                    return NotFound($"{report.ReportNo} Not Found");
                }

                if (report.IsApprove == null)
                {
                    _report.IsApprove = null;
                }
                else if (report.IsApprove ?? false)
                {
                    _report.IsApprove = report.IsApprove;
                    _report.ApprovedByUserID = report.ApprovedByUserID;
                    _report.ApprovedDateTime = DateFormat.DateTimeNow();
                    _report.VoidByUserID = string.Empty;
                    _report.VoidDateTime = null;
                }
                else
                {
                    _report.IsApprove = report.IsApprove;
                    _report.VoidByUserID = report.VoidByUserID;
                    _report.VoidDateTime = DateFormat.DateTimeNow();
                    _report.ApprovedByUserID = string.Empty;
                    _report.ApprovedDateTime = null;
                }
                _report.SRReportStatus = report.SRReportStatus;
                _report.LastUpdateDateTime = DateFormat.DateTimeNow();
                _report.LastUpdateByUserID = report.LastUpdateByUserID;
                _context.Update(_report);

                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Ok($"User {report.ReportNo} Update Successfully")
                    : BadRequest($"Failed To Update Data For ReportNo {report.ReportNo}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetUserReport", Name = "GetUserReport")]
        public ActionResult<PageResponse<UserReport>> GetUserReport([FromQuery] UserReportFilter filter)
        {
            try
            {
                var urQ = new UserreportQuery("urQ");
                var elQ = new AppstandardreferenceitemQuery("elQ");
                var epQ = new AppstandardreferenceitemQuery("epQ");
                var rsQ = new AppstandardreferenceitemQuery("rsQ");

                urQ.Select(urQ.ReportNo, urQ.DateErrorOccured, urQ.ErrorCronologic, urQ.Picture, urQ.IsApprove,
                    urQ.CreatedDateTime, urQ.CreatedByUserID, urQ.PersonID, elQ.ItemName.As("SRErrorLocation"),
                    epQ.ItemName.As("SRErrorPossibility"), rsQ.ItemName.As("SRReportStatus"))
                    .InnerJoin(elQ).On(elQ.StandardReferenceID == "ErrorLocation" && elQ.ItemID == urQ.SRErrorLocation)
                    .InnerJoin(epQ).On(epQ.StandardReferenceID == "ErrorPossibility" && epQ.ItemID == urQ.SRErrorPossibility)
                    .InnerJoin(rsQ).On(rsQ.StandardReferenceID == "ReportStatus" && rsQ.ItemID == urQ.SRReportStatus)
                    .OrderBy(urQ.CreatedDateTime.Ascending);

                if (!string.IsNullOrEmpty(filter.PersonID))
                {
                    urQ.Where(urQ.PersonID == filter.PersonID);
                }

                DataTable dtRecord = urQ.LoadDataTable();

                urQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = urQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var pagedData = new List<UserReport>();

                foreach (DataRow dr in dt.Rows)
                {
                    var report = new UserReport
                    {
                        ReportNo = (string)dr["ReportNo"],
                        DateErrorOccured = (DateTime)dr["DateErrorOccured"],
                        SRErrorLocation = (string)dr["SRErrorLocation"],
                        SRErrorPossibility = (string)dr["SRErrorPossibility"],
                        ErrorCronologic = (string)dr["ErrorCronologic"],
                        Picture = (byte[]?)dr["Picture"],
                        IsApprove = dr["IsApprove"] != DBNull.Value ? bool.Parse((string)dr["IsApprove"]) : null,
                        SRReportStatus = (string)dr["SRReportStatus"],
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        CreatedByUserID = (string)dr["CreatedByUserID"],
                        PersonID = (string)dr["PersonID"]
                    };
                    pagedData.Add(report);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetUserReport", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetUserReport", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<UserReport>>(pagedData, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = pagedData.Count > 0 ? FoundMsg : NotFoundMsg,
                    Succeeded = pagedData.Count > 0
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetReportNo", Name = "GetReportNo")]
        public ActionResult<UserReport> GetReportNo([FromQuery] UserReportFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.ReportNo))
                {
                    return BadRequest($"ReportNo Is Required");
                }

                var urQ = new UserreportQuery("urQ");
                var elQ = new AppstandardreferenceitemQuery("elQ");
                var epQ = new AppstandardreferenceitemQuery("epQ");
                var rsQ = new AppstandardreferenceitemQuery("rsQ");

                urQ.Select(urQ.ReportNo, urQ.DateErrorOccured, urQ.ErrorCronologic, urQ.Picture, urQ.IsApprove,
                    urQ.ApprovedDateTime, urQ.ApprovedByUserID, urQ.VoidDateTime, urQ.VoidByUserID, urQ.LastUpdateDateTime,
                    urQ.LastUpdateByUserID, urQ.CreatedDateTime, urQ.CreatedByUserID, urQ.PersonID, elQ.ItemName.As("SRErrorLocation"),
                    epQ.ItemName.As("SRErrorPossibility"), rsQ.ItemName.As("SRReportStatus"))
                    .InnerJoin(elQ).On(elQ.StandardReferenceID == "ErrorLocation" && elQ.ItemID == urQ.SRErrorLocation)
                    .InnerJoin(epQ).On(epQ.StandardReferenceID == "ErrorPossibility" && epQ.ItemID == urQ.SRErrorPossibility)
                    .InnerJoin(rsQ).On(rsQ.StandardReferenceID == "ReportStatus" && rsQ.ItemID == urQ.SRReportStatus)
                    .OrderBy(urQ.CreatedDateTime.Ascending)
                    .Where(urQ.ReportNo == filter.ReportNo);
                DataTable dt = urQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                DataRow dr = dt.Rows[0];
                var response = new UserReport
                {
                    ReportNo = (string)dr["ReportNo"],
                    DateErrorOccured = (DateTime)dr["DateErrorOccured"],
                    SRErrorLocation = (string)dr["SRErrorLocation"],
                    SRErrorPossibility = (string)dr["SRErrorPossibility"],
                    ErrorCronologic = (string)dr["ErrorCronologic"],
                    IsApprove = dr["IsApprove"] != DBNull.Value ? bool.Parse((string)dr["IsApprove"]) : null,
                    SRReportStatus = (string)dr["SRReportStatus"],
                    ApprovedDateTime = filter.IsAdmin ? (dr["ApprovedDateTime"] != DBNull.Value ? (DateTime)dr["ApprovedDateTime"] : null) : null,
                    ApprovedByUserID = filter.IsAdmin ? (dr["ApprovedByUserID"] != DBNull.Value ? (string)dr["ApprovedByUserID"] : null) : null,
                    VoidDateTime = filter.IsAdmin ? (dr["VoidDateTime"] != DBNull.Value ? (DateTime)dr["VoidDateTime"] : null) : null,
                    VoidByUserID = filter.IsAdmin ? (dr["VoidByUserID"] != DBNull.Value ? (string)dr["VoidByUserID"] : null) : null,
                    CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                    CreatedByUserID = (string)dr["CreatedByUserID"],
                    LastUpdateDateTime = filter.IsAdmin ? (dr["LastUpdateDateTime"] != DBNull.Value ? (DateTime)dr["LastUpdateDateTime"] : null) : null,
                    LastUpdateByUserID = filter.IsAdmin ? (dr["LastUpdateByUserID"] != DBNull.Value ? (string)dr["LastUpdateByUserID"] : null) : null,
                    PersonID = (string)dr["PersonID"]
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
    }
}
