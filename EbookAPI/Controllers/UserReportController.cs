using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using static UangKuAPI.BusinessObjects.Helper.Helper;

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
        public async Task<IActionResult> PostUserReport([FromBody] PostUserReport report)
        {
            try
            {
                if (report == null)
                {
                    return BadRequest($"Report Are Required");
                }
                string createddate = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);
                string updatedate = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);
                string errordate = DateFormat.DateTimeNow(Longyearpattern, report.DateErrorOccured);
                string reportStatus = "ReportStatus-001";

                var query = $@"INSERT INTO `UserReport`(`ReportNo`, `DateErrorOccured`, `SRErrorLocation`, `SRErrorPossibility`, 
                                `ErrorCronologic`, `Picture`, `CreatedDateTime`, `CreatedByUserID`, 
                                `LastUpdateDateTime`, `LastUpdateByUserID`, `PersonID`, `SRReportStatus`)
                                VALUES('{report.ReportNo}', '{errordate}', '{report.SRErrorLocation}', '{report.SRErrorPossibility}',
                                '{report.ErrorCronologic}', '{report.Picture}', '{createddate}', '{report.CreatedByUserID}',
                                '{updatedate}', '{report.LastUpdateByUserID}', '{report.PersonID}', '{reportStatus}');";
                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

                if (rowsAffected > 0)
                {
                    return Ok($"Report No {report.ReportNo} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For Report No {report.ReportNo}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchUserReport", Name = "PatchUserReport")]
        public async Task<IActionResult> PatchUserReport([FromQuery] bool IsApproved, [FromQuery] string ReportNo, [FromBody] PatchUserReport patch)
        {
            try
            {
                var dateTime = DateFormat.DateTimeNow();

                int approved = IsApproved ? 1 : 0;

                if (string.IsNullOrEmpty(ReportNo))
                {
                    return BadRequest($"Report No Is Required");
                }

                var report = await _context.Report
                    .Where(r => r.ReportNo == ReportNo)
                    .FirstOrDefaultAsync();

                if (report == null)
                {
                    return NotFound($"{ReportNo} Not Found");
                }

                report.IsApprove = IsApproved;
                report.SRReportStatus = patch.SRReportStatus;
                report.LastUpdateDateTime = DateFormat.DateTimeNowDate(dateTime.Year, dateTime.Month, dateTime.Day);
                report.LastUpdateByUserID = patch.LastUpdateByUserID;

                if (IsApproved)
                {
                    report.ApprovedDateTime = DateFormat.DateTimeNowDate(dateTime.Year, dateTime.Month, dateTime.Day);
                    report.ApprovedByUserID = patch.ApprovedByUserID;
                }
                else
                {
                    report.VoidDateTime = DateFormat.DateTimeNowDate(dateTime.Year, dateTime.Month, dateTime.Day);
                    report.VoidByUserID = patch.VoidByUserID;
                }

                _context.Update(report);
                var response = await _context.SaveChangesAsync();

                if (response > 0)
                {
                    return Ok($"{ReportNo} Successfully Update");
                }
                else
                {
                    return NotFound($"{ReportNo} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetUserReport", Name = "GetUserReport")]
        public async Task<ActionResult<GetUserReport>> GetUserReport([FromQuery] UserReportFilter filter)
        {
            try
            {
                var validFilter = new UserReportFilter(filter.PageNumber, filter.PageSize, filter.PersonID);
                var pageNumber = validFilter.PageNumber;
                var pageSize = validFilter.PageSize;
                var pagedData = await _context.Report
                    .Join(
                        _context.AppStandardReferenceItems.Where(asri => asri.StandardReferenceID == "ErrorLocation"),
                        ur => ur.SRErrorLocation,
                        asri => asri.ItemID,
                        (ur, asri) => new { ur, ErrorLocationItemName = asri.ItemName }
                    )
                    .Join(
                        _context.AppStandardReferenceItems.Where(asri => asri.StandardReferenceID == "ErrorPossibility"),
                        combined => combined.ur.SRErrorPossibility,
                        asri => asri.ItemID,
                        (combined, asri) => new { combined.ur, combined.ErrorLocationItemName, ErrorPossibilityItemName = asri.ItemName }
                    )
                    .Join(
                        _context.AppStandardReferenceItems.Where(asri => asri.StandardReferenceID == "ReportStatus"),
                        combined => combined.ur.SRReportStatus,
                        asri => asri.ItemID,
                        (combined, asri) => new { combined.ur, combined.ErrorLocationItemName, combined.ErrorPossibilityItemName, ReportStatusItemName = asri.ItemName }
                    )
                    .Select(result => new GetUserReport
                    {
                        ReportNo = result.ur.ReportNo, DateErrorOccured = result.ur.DateErrorOccured,
                        SRErrorLocation = result.ErrorLocationItemName, SRErrorPossibility = result.ErrorPossibilityItemName,
                        ErrorCronologic = result.ur.ErrorCronologic, Picture = result.ur.Picture,
                        IsApprove = result.ur.IsApprove, SRReportStatus = result.ReportStatusItemName,
                        CreatedDateTime = result.ur.CreatedDateTime, CreatedByUserID = result.ur.CreatedByUserID,
                        PersonID = result.ur.PersonID
                    })
                    .Where(r => (string.IsNullOrEmpty(filter.PersonID) || r.PersonID == filter.PersonID))
                    .OrderByDescending(r => r.CreatedDateTime)
                    .AsNoTracking()
                    .ToListAsync();

                if (pagedData == null || pagedData.Count == 0 || !pagedData.Any())
                {
                    return NotFound("Person ID Not Found");
                }

                var totalRecord = await _context.Report.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / validFilter.PageSize);

                string? prevPageLink = validFilter.PageNumber > 1
                    ? Url.Link("GetUserReport", new { PageNumber = validFilter.PageNumber - 1, validFilter.PageSize })
                    : null;

                string? nextPageLink = validFilter.PageNumber < totalPages
                    ? Url.Link("GetUserReport", new { PageNumber = validFilter.PageNumber + 1, validFilter.PageSize })
                    : null;

                var response = new PageResponse<List<GetUserReport>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink
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
        public async Task<ActionResult<GetUserReport>> GetReportNo([FromQuery] string ReportNo, [FromQuery] bool IsAdmin)
        {
            try
            {
                if (string.IsNullOrEmpty(ReportNo))
                {
                    return BadRequest($"ReportNo Is Required");
                }

                var query = await _context.Report
                    .Where(r => r.ReportNo == ReportNo)
                    .Select(r => IsAdmin
                        ? new UserReport
                        {
                            ReportNo = r.ReportNo, DateErrorOccured = r.DateErrorOccured,
                            SRErrorLocation = r.SRErrorLocation, SRErrorPossibility = r.SRErrorPossibility,
                            ErrorCronologic = r.ErrorCronologic, Picture = r.Picture,
                            IsApprove = r.IsApprove, SRReportStatus = r.SRReportStatus,
                            ApprovedDateTime = r.ApprovedDateTime, ApprovedByUserID = r.ApprovedByUserID,
                            VoidDateTime = r.VoidDateTime, VoidByUserID = r.VoidByUserID,
                            CreatedDateTime = r.CreatedDateTime, CreatedByUserID = r.CreatedByUserID,
                            LastUpdateDateTime = r.LastUpdateDateTime, LastUpdateByUserID = r.LastUpdateByUserID,
                            PersonID = r.PersonID
                        }
                        : new UserReport
                        {
                            ReportNo = r.ReportNo, DateErrorOccured = r.DateErrorOccured,
                            SRErrorLocation = r.SRErrorLocation, SRErrorPossibility = r.SRErrorPossibility,
                            ErrorCronologic = r.ErrorCronologic, Picture = r.Picture,
                            IsApprove = r.IsApprove, SRReportStatus = r.SRReportStatus,
                            CreatedDateTime = r.CreatedDateTime, CreatedByUserID = r.CreatedByUserID,
                            PersonID = r.PersonID
                        })
                    .AsNoTracking()
                    .ToListAsync();


                if (query == null || query.Count == 0 || !query.Any())
                {
                    return NotFound($"{ReportNo} Not Found");
                }

                return Ok(query);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
    }
}
