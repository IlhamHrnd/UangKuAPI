using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Entity.Generated;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

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
        public ActionResult<Response<string>> GetNewReportNo([FromQuery] UserReportFilter filter)
        {
            var data = string.Empty;
            var response = new Response<string>();

            try
            {
                if (string.IsNullOrEmpty(filter.TransType))
                {
                    response = new Response<string>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Transaction Type"),
                        Succeeded = !string.IsNullOrEmpty(data)
                    };
                    return BadRequest(response);
                }

                int number = 1;
                string transDate = DateFormat.DateTimeNow(DateFormat.Shortyearpattern, DateFormat.DateTimeNow());
                string formattedNumber;

                do
                {
                    formattedNumber = Converter.NumberingFormat(number, "D3");
                    data = $"RPT/{filter.TransType}/{transDate}-{formattedNumber}";
                    number++;
                } while (_context.UserPictures.Any(up => up.PictureId == data));

                response = new Response<string>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(data)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<string>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data)
                };
                return BadRequest(response);
            }
        }

        [HttpPost("PostUserReport", Name = "PostUserReport")]
        public async Task<IActionResult> PostUserReport([FromBody] UserReport report)
        {
            if (report == null)
                return BadRequest(string.Format(AppConstant.RequiredMsg, "report"));
                
            
            //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
            var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
            var size = Converter.StringToInt(maxSize, 0);
            var result = Converter.IntToLong(size);

            if (report.Picture != null && report.Picture.Length > result)
                return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", report.ReportNo, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));
            
            var ur = new UserReport
            {
                ReportNo = report.ReportNo, DateErrorOccured = report.DateErrorOccured, SrerrorPossibility = report.SrerrorPossibility, SrerrorLocation = report.SrerrorLocation, ErrorCronologic = report.ErrorCronologic,
                Picture = report.Picture, CreatedDateTime = DateFormat.DateTimeNow(), CreatedByUserId = report.CreatedByUserId, LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserId = report.LastUpdateByUserId,
                PersonId = report.PersonId, SrreportStatus = report.SrreportStatus
            };
            _context.UserReports.Add(ur);
            int rows = await _context.SaveChangesAsync();

            return rows > 0
                ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Report", report.ReportNo))
                : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Report", report.ReportNo));
        }

        [HttpPatch("PatchUserReport", Name = "PatchUserReport")]
        public async Task<IActionResult> PatchUserReport([FromQuery] UserReportFilter filter, [FromBody] UserReport report)
        {
            if (report == null)
                return BadRequest(string.Format(AppConstant.RequiredMsg, "Report"));

            if (string.IsNullOrEmpty(report.ReportNo))
                return BadRequest(string.Format(AppConstant.RequiredMsg, "Report No"));

            var data = await _context.UserReports
                .FirstOrDefaultAsync(ur => ur.ReportNo == report.ReportNo);
            
            if (data == null)
                return NotFound(AppConstant.NotFoundMsg);

            data.SrreportStatus = report.SrreportStatus;
            data.LastUpdateDateTime = DateFormat.DateTimeNow();
            data.LastUpdateByUserId = report.LastUpdateByUserId;

            if (filter.IsApproved.HasValue)
            {
                data.IsApprove = filter.IsApproved;

                if (filter.IsApproved ?? false)
                {
                    data.ApprovedDateTime = DateFormat.DateTimeNow();
                    data.ApprovedByUserId = report.ApprovedByUserId;
                }
                else
                {
                    data.VoidDateTime = DateFormat.DateTimeNow();
                    data.VoidByUserId = report.VoidByUserId;
                }
            }

            _context.Update(data);
            int rows = await _context.SaveChangesAsync();

            return rows > 0
                ? Ok(string.Format(AppConstant.UpdateSuccessMsg, report.ReportNo))
                : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "User Report", report.ReportNo));
        }

        [HttpGet("GetUserReport", Name = "GetUserReport")]
        public ActionResult<PageResponse<UserReport>> GetUserReport([FromQuery] UserReportFilter filter)
        {
            var pagedData = new List<UserReport>();
            var response = new PageResponse<List<UserReport>>(pagedData, 0, 0);

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new PageResponse<List<UserReport>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = pagedData.Count > 0
                    };
                    return BadRequest(response);
                }

                if (!BusinessObjects.Entity.Custom.UserReport.GetPersonID(filter.PersonID))
                {
                    response = new PageResponse<List<UserReport>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = AppConstant.NotFoundMsg,
                        Succeeded = pagedData.Count > 0
                    };
                    return NotFound(response);
                }

                bool isAdmin = BusinessObjects.Entity.Custom.User.IsUserAdmin(filter.PersonID);

                var urQ = new UserreportQuery("urQ");
                var locQ = new AppstandardreferenceitemQuery("locQ");
                var posQ = new AppstandardreferenceitemQuery("posQ");
                var rptQ = new AppstandardreferenceitemQuery("rptQ");

                urQ.Select(urQ.ReportNo);

                if (!isAdmin)
                    urQ.Where(urQ.PersonID == filter.PersonID);
                
                if (filter.IsApproved.HasValue)
                    urQ.Where(urQ.IsApprove == filter.IsApproved);
                DataTable dtRecord = urQ.LoadDataTable();

                if (dtRecord.Rows.Count == 0)
                {
                    response = new PageResponse<List<UserReport>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = pagedData.Count > 0
                    };
                    return NotFound(response);
                }

                if (isAdmin)
                {
                    urQ.Select(urQ.ApprovedDateTime, urQ.ApprovedByUserID, urQ.VoidDateTime, urQ.VoidByUserID, urQ.LastUpdateDateTime, urQ.LastUpdateByUserID);
                }

                urQ.Select(urQ.DateErrorOccured, urQ.ErrorCronologic, urQ.Picture, urQ.IsApprove,
                    urQ.CreatedDateTime, urQ.CreatedByUserID, urQ.PersonID, locQ.ItemName.As("SRErrorLocation"),
                    posQ.ItemName.As("SRErrorPossibility"), rptQ.ItemName.As("SRReportStatus"))
                    .InnerJoin(locQ).On(locQ.StandardReferenceID == "ErrorLocation" && locQ.ItemID == urQ.SRErrorLocation)
                    .InnerJoin(posQ).On(posQ.StandardReferenceID == "ErrorPossibility" && posQ.ItemID == urQ.SRErrorPossibility)
                    .InnerJoin(rptQ).On(rptQ.StandardReferenceID == "ReportStatus" && rptQ.ItemID == urQ.SRReportStatus)
                    .OrderBy(urQ.CreatedDateTime.Ascending)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = urQ.LoadDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    var rpt = new UserReport
                    {
                        ReportNo = (string)dr["ReportNo"],
                        DateErrorOccured = (DateTime)dr["DateErrorOccured"],
                        SrerrorLocation = (string)dr["SRErrorLocation"],
                        SrerrorPossibility = (string)dr["SRErrorPossibility"],
                        ErrorCronologic = (string)dr["ErrorCronologic"],
                        IsApprove = dr["IsApprove"] != DBNull.Value ? bool.Parse((string)dr["IsApprove"]) : null,
                        SrreportStatus = (string)dr["SRReportStatus"],
                        ApprovedDateTime = isAdmin ? (dr["ApprovedDateTime"] != DBNull.Value ? (DateTime)dr["ApprovedDateTime"] : null) : null,
                        ApprovedByUserId = isAdmin ? (dr["ApprovedByUserID"] != DBNull.Value ? (string)dr["ApprovedByUserID"] : null) : null,
                        VoidDateTime = isAdmin ? (dr["VoidDateTime"] != DBNull.Value ? (DateTime)dr["VoidDateTime"] : null) : null,
                        VoidByUserId = isAdmin ? (dr["VoidByUserID"] != DBNull.Value ? (string)dr["VoidByUserID"] : null) : null,
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        CreatedByUserId = (string)dr["CreatedByUserID"],
                        LastUpdateDateTime = isAdmin ? (dr["LastUpdateDateTime"] != DBNull.Value ? (DateTime)dr["LastUpdateDateTime"] : DateFormat.DateTimeNow()) : DateFormat.DateTimeNow(),
                        LastUpdateByUserId = isAdmin ? (dr["LastUpdateByUserID"] != DBNull.Value ? (string)dr["LastUpdateByUserID"] : string.Empty) : string.Empty,
                        PersonId = (string)dr["PersonID"]
                    };
                    pagedData.Add(rpt);
                }

                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetUserReport", new { filter.PersonID, PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetUserReport", new { filter.PersonID, PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<UserReport>>(pagedData, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = pagedData.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new PageResponse<List<UserReport>>(pagedData, 0, 0)
                {
                    TotalPages = pagedData.Count,
                    TotalRecords = pagedData.Count,
                    PrevPageLink = string.Empty,
                    NextPageLink = string.Empty,
                    Message = $"{(pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = pagedData.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetReportNo", Name = "GetReportNo")]
        public ActionResult<Response<UserReport>> GetReportNo([FromQuery] UserReportFilter filter)
        {
            var data = new UserReport();
            var response = new Response<UserReport>();

            try
            {
                if (string.IsNullOrEmpty(filter.ReportNo))
                {
                    response = new Response<UserReport>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ReportNo"),
                        Succeeded = !string.IsNullOrEmpty(data.ReportNo)
                    };
                    return BadRequest(response);
                }

                var ur = new Userreport();
                if (!ur.LoadByPrimaryKey(filter.ReportNo))
                {
                    response = new Response<UserReport>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(ur.ReportNo) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(ur.ReportNo)
                    };
                    return NotFound(response);
                }

                var ErrorLocation = !string.IsNullOrEmpty(ur.SRErrorLocation) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("ErrorLocation", ur.SRErrorLocation) : string.Empty;
                var ErrorPossibility = !string.IsNullOrEmpty(ur.SRErrorPossibility) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("ErrorPossibility", ur.SRErrorPossibility) : string.Empty;
                var ReportStatus = !string.IsNullOrEmpty(ur.SRReportStatus) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("ReportStatus", ur.SRReportStatus) : string.Empty;

                bool isAdmin = BusinessObjects.Entity.Custom.User.IsUserAdmin(filter.PersonID);
                data = new UserReport
                {
                    ReportNo = ur.ReportNo, DateErrorOccured = ur.DateErrorOccured, SrerrorLocation = ErrorLocation, SrerrorPossibility = ErrorPossibility, ErrorCronologic = ur.ErrorCronologic, Picture = ur.Picture,
                    IsApprove = ur.IsApprove.HasValue ? ur.IsApprove == 1 : null, SrreportStatus = ReportStatus, ApprovedDateTime = isAdmin ? ur.ApprovedDateTime : null, ApprovedByUserId = isAdmin ? ur.ApprovedByUserID : string.Empty,
                    VoidDateTime = isAdmin ? ur.VoidDateTime : null, VoidByUserId = isAdmin ? ur.VoidByUserID : string.Empty, CreatedDateTime = ur.CreatedDateTime ?? DateFormat.DateTimeNow(), CreatedByUserId = ur.CreatedByUserID,
                    LastUpdateDateTime = isAdmin ? (ur.LastUpdateDateTime ?? DateFormat.DateTimeNow()) : DateFormat.DateTimeNow(), LastUpdateByUserId = isAdmin ? ur.LastUpdateByUserID : string.Empty,
                    PersonId = ur.PersonID
                };

                response = new Response<UserReport>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data.ReportNo) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(data.ReportNo)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<UserReport>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.ReportNo) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.ReportNo)
                };
                return BadRequest(response);
            }
        }
    }
}