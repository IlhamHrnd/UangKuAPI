using EbookAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
using UangKuAPI.Model;
using DateTimeFormat = UangKuAPI.Helper.DateTimeFormat;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "AppParameterAPI")]
    [ApiController]
    public class AppParameterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppParameterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllAppParameter", Name = "GetAllAppParameter")]
        public async Task<ActionResult<List<AppParameter>>> GetAllAppParameter([FromQuery] AppParameterFilter filter)
        {
            try
            {
                var validFilter = new AppParameterFilter(filter.PageNumber, filter.PageSize);
                var pageNumber = validFilter.PageNumber;
                var pageSize = validFilter.PageSize;
                var pagedData = await _context.Parameter
                    .Select(p => new AppParameter
                    {
                        ParameterID = p.ParameterID, ParameterName = p.ParameterName,
                        ParameterValue = p.ParameterValue, LastUpdateDateTime = p.LastUpdateDateTime,
                        LastUpdateByUserID = p.LastUpdateByUserID, IsUsedBySystem = p.IsUsedBySystem
                    })
                    .OrderBy(p => p.ParameterID)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var totalRecord = await _context.Parameter.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / validFilter.PageSize);

                string? prevPageLink = validFilter.PageNumber > 1
                    ? Url.Link("GetAllAppParameter", new { PageNumber = validFilter.PageNumber - 1, validFilter.PageSize })
                    : null;

                string? nextPageLink = validFilter.PageNumber < totalPages
                    ? Url.Link("GetAllAppParameter", new { PageNumber = validFilter.PageNumber + 1, validFilter.PageSize })
                    : null;

                var response = new PageResponse<List<AppParameter>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
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
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetParameterID", Name = "GetParameterID")]
        public async Task<ActionResult<AppParameter>> GetParameterID([FromQuery] string parameterID)
        {
            try
            {
                if (string.IsNullOrEmpty(parameterID))
                {
                    return BadRequest($"ParameterID Is Required");
                }

                var response = await _context.Parameter
                    .Select(p => new AppParameter
                    {
                        ParameterID = p.ParameterID,
                        ParameterName = p.ParameterName,
                        ParameterValue = p.ParameterValue,
                        LastUpdateDateTime = p.LastUpdateDateTime,
                        LastUpdateByUserID = p.LastUpdateByUserID,
                        IsUsedBySystem = p.IsUsedBySystem
                    })
                    .Where(p => p.ParameterID == parameterID)
                    .ToListAsync();

                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("App Parameter Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("PostAppParameter", Name = "PostAppParameter")]
        public async Task<IActionResult> PostAppParameter([FromBody] AppParameter ap)
        {
            try
            {
                if (ap == null)
                {
                    return BadRequest($"AppParameter Is Required");
                }

                string date = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);
                int use = ap.IsUsedBySystem == true ? 1 : 0;

                var query = $@"INSERT INTO `AppParameter`(`ParameterID`, `ParameterName`, `ParameterValue`, 
                            `LastUpdateDateTime`, `LastUpdateByUserID`, `IsUsedBySystem`) 
                            VALUES ('{ap.ParameterID}','{ap.ParameterName}','{ap.ParameterValue}',
                            '{date}','{ap.LastUpdateByUserID}','{use}')";

                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

                if (rowsAffected > 0)
                {
                    return Ok($"Parameter {ap.ParameterID} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For ParameterID {ap.ParameterID}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppParameter", Name = "UpdateAppParameter")]
        public async Task<IActionResult> UpdateAppParameter([FromBody] AppParameter ap)
        {
            try
            {
                string date = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);
                int use = ap.IsUsedBySystem == true ? 1 : 0;

                if (string.IsNullOrEmpty(ap.ParameterID))
                {
                    return BadRequest($"ParameterID Is Required");
                }

                var query = $@"UPDATE `AppParameter`
                                SET `ParameterName` = '{ap.ParameterName}',
                                `ParameterValue` = '{ap.ParameterValue}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUserID` = '{ap.LastUpdateByUserID}',
                                `IsUsedBySystem` = '{use}'
                                WHERE `ParameterID` = '{ap.ParameterID}'";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"{ap.ParameterID} Update Successfully");
                }
                else
                {
                    return NotFound($"{ap.ParameterID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
