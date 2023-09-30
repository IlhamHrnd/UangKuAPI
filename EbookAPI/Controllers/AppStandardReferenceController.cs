using EbookAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Model;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "AppStandardReferenceAPI")]
    [ApiController]
    public class AppStandardReferenceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppStandardReferenceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllReferenceID", Name = "GetAllReferenceID")]
        public async Task<ActionResult<List<AppStandardReference>>> GetAllReferenceID([FromQuery] AppStandardReferenceFilter filter)
        {
            try
            {
                var validFilter = new AppStandardReferenceFilter(filter.PageNumber, filter.PageSize);
                var pageNumber = validFilter.PageNumber;
                var pageSize = validFilter.PageSize;
                var query = $@"SELECT asr.StandardReferenceID, asr.StandardReferenceName, asr.ItemLength,
                                asr.IsUsedBySystem, asr.IsActive, asr.Note,
                                asr.LastUpdateDateTime, asr.LastUpdateByUserID
                                FROM AppStandardReference AS asr
                                ORDER BY asr.StandardReferenceID
                                OFFSET {(pageNumber - 1) * pageSize} ROWS
                                FETCH NEXT {pageSize} ROWS ONLY;";
                var pagedData = await _context.AppStandardReferences.FromSqlRaw(query).ToListAsync();
                var totalRecord = await _context.AppStandardReferences.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / validFilter.PageSize);

                string? prevPageLink = validFilter.PageNumber > 1
                    ? Url.Link("GetAllReferenceID", new { PageNumber = validFilter.PageNumber - 1, validFilter.PageSize })
                    : null;

                string? nextPageLink = validFilter.PageNumber < totalPages
                    ? Url.Link("GetAllReferenceID", new { PageNumber = validFilter.PageNumber + 1, validFilter.PageSize })
                    : null;

                var response = new PageResponse<List<AppStandardReference>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
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

        [HttpGet("GetReferenceID", Name = "GetReferenceID")]
        public async Task<ActionResult<AppStandardReference>> GetReferenceID([FromQuery] AppStandardReferenceIDFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.ReferenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }
                var query = $@"SELECT asr.StandardReferenceID, asr.StandardReferenceName, asr.ItemLength,
                                asr.IsUsedBySystem, asr.IsActive, asr.Note,
                                asr.LastUpdateDateTime, asr.LastUpdateByUserID
                                FROM AppStandardReference AS asr
                                WHERE asr.StandardReferenceID = '{filter.ReferenceID}';";
                var response = await _context.AppStandardReferences.FromSqlRaw(query).ToListAsync();
                if (response == null)
                {
                    return NotFound("App Standard Reference Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("CreateAppStandardReference", Name = "CreateAppStandardReference")]
        public async Task<IActionResult> CreateAppStandardReference([FromBody] AppStandardReference asr)
        {
            try
            {
                if (asr == null)
                {
                    return BadRequest("User AppStandardReference Are Required");
                }

                DateTime dateTime = DateTime.Now;
                string date = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                bool isdefault = true;

                var query = $@"INSERT INTO `AppStandardReference`(`StandardReferenceID`, `StandardReferenceName`, `ItemLength`,
                                `IsUsedBySystem`, `IsActive`, `Note`, `LastUpdateDateTime`, `LastUpdateByUserID`)
                                VALUES('{asr.StandardReferenceID}', '{asr.StandardReferenceName}', '{asr.ItemLength}',
                                '{isdefault}', '{isdefault}', '{asr.Note}', '{date}', '{asr.LastUpdateByUserID}');";

                await _context.Database.ExecuteSqlRawAsync(query);

                return Ok($"Standard Referece {asr.StandardReferenceName} Created Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReference", Name = "UpdateAppStandardReference")]
        public async Task<IActionResult> UpdateAppStandardReference([FromQuery] string referenceID, [FromQuery] int itemLength, [FromQuery] bool isActive, [FromQuery] bool isUse, [FromQuery] string user)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                string date = $"{dateTime: yyyy-MM-dd HH:mm:ss}";

                if (string.IsNullOrEmpty(referenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                var query = $@"UPDATE `AppStandardReference`
                                SET `ItemLength` = '{itemLength}',
                                `IsUsedBySystem` = '{isUse}',
                                `IsActive` = '{isActive}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUserID` = '{user}'
                                WHERE `StandardReferenceID` = '{referenceID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response >  0)
                {
                    return Ok($"{referenceID} Update Successfully");
                }
                else
                {
                    return NotFound($"{referenceID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteAppStandardReference", Name = "DeleteAppStandardReference")]
        public async Task<IActionResult> DeleteAppStandardReference([FromQuery] string referenceID)
        {
            try
            {
                if (string.IsNullOrEmpty(referenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                var query = $@"DELETE FROM `AppStandardReference`
                                WHERE `StandardReferenceID` = '{referenceID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"{referenceID} Delete Successfully");
                }
                else
                {
                    return NotFound($"{referenceID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
