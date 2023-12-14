using EbookAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
using UangKuAPI.Model;

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
                string transDate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmdd);
                int number = 1;
                string formattedNumber = NumberStringFormat.NumberThreeDigit(number);
                string transNo = $"RPT/{TransType}/{transDate}-{formattedNumber}";

                while (_context.Report.Any(r => r.ReportNo == transNo))
                {
                    number++;
                    formattedNumber = NumberStringFormat.NumberThreeDigit(number);
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
                string createddate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);
                string updatedate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);
                string errordate = DateTimeFormat.DateTimeUser(report.DateErrorOccured, DateStringFormat.Yymmddhhmmss);

                var query = $@"INSERT INTO `UserReport`(`ReportNo`, `DateErrorOccured`, `SRErrorLocation`, `SRErrorPossibility`, 
                                `ErrorCronologic`, `Picture`, `CreatedDateTime`, `CreatedByUserID`, 
                                `LastUpdateDateTime`, `LastUpdateByUserID`, `PersonID`)
                                VALUES('{report.ReportNo}', '{errordate}', '{report.SRErrorLocation}', '{report.SRErrorPossibility}',
                                '{report.ErrorCronologic}', '{report.Picture}', '{createddate}', '{report.CreatedByUserID}',
                                '{updatedate}', '{report.LastUpdateByUserID}', '{report.PersonID}');";
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
        public async Task<IActionResult> PatchUserReport([FromBody] PatchUserReport report)
        {
            try
            {
                if (report == null)
                {
                    return BadRequest($"Report Are Required");
                }
                string errordate = DateTimeFormat.DateTimeUser(report.DateErrorOccured, DateStringFormat.Yymmddhhmmss);
                string updatedate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);

                var query = $@"UPDATE `UserReport`
                                SET `DateErrorOccured` = '{errordate}',
                                `SRErrorLocation` = '{report.SRErrorLocation}',
                                `SRErrorPossibility` = '{report.SRErrorPossibility}',
                                `ErrorCronologic` = '{report.ErrorCronologic}',
                                `Picture` = '{report.Picture}',
                                `LastUpdateDateTime` = '{updatedate}',
                                `LastUpdateByUserID` = '{report.LastUpdateByUserID}'
                                WHERE `ReportNo` = '{report.ReportNo}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"{report.ReportNo} Update Successfully");
                }
                else
                {
                    return NotFound($"{report.ReportNo} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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
                    .Select(r => new GetUserReport
                    {
                        ReportNo = r.ReportNo, DateErrorOccured = r.DateErrorOccured,
                        SRErrorLocation = r.SRErrorLocation, SRErrorPossibility = r.SRErrorPossibility,
                        ErrorCronologic = r.ErrorCronologic, Picture = r.Picture,
                        IsApprove = r.IsApprove, SRReportStatus = r.SRReportStatus,
                        CreatedDateTime = r.CreatedDateTime, CreatedByUserID = r.CreatedByUserID,
                        PersonID = r.PersonID
                    })
                    .Where(r => (string.IsNullOrEmpty(filter.PersonID) || r.PersonID == filter.PersonID))
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
    }
}
