using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;

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
                var pageNumber = filter.PageNumber;
                var pageSize = filter.PageSize;
                var pagedData = await _context.AppStandardReferences
                    .Select(asr => new AppStandardReference
                    {
                        StandardReferenceID = asr.StandardReferenceID, StandardReferenceName = asr.StandardReferenceName, ItemLength = asr.ItemLength,
                        IsUsedBySystem = asr.IsUsedBySystem, IsActive = asr.IsActive, Note = asr.Note,
                        LastUpdateDateTime = asr.LastUpdateDateTime, LastUpdateByUserID = asr.LastUpdateByUserID
                    })
                    .OrderBy(asr => asr.StandardReferenceID)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var totalRecord = await _context.AppStandardReferences.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllReferenceID", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllReferenceID", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var isDataFound = _context.AppStandardReferences.Any();
                var response = new PageResponse<List<AppStandardReference>>(pagedData, pageNumber, pageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = isDataFound ? "Data Found" : "Data Not Found",
                    Succeeded = isDataFound
                };

                return isDataFound ? Ok(response) : NotFound(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetReferenceID", Name = "GetReferenceID")]
        public async Task<ActionResult<AppStandardReference>> GetReferenceID([FromQuery] AppStandardReferenceFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.ReferenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                var response = await _context.AppStandardReferences
                    .Select(asr => new AppStandardReference
                    {
                        StandardReferenceID = asr.StandardReferenceID, StandardReferenceName = asr.StandardReferenceName, ItemLength = asr.ItemLength,
                        IsUsedBySystem = asr.IsUsedBySystem, IsActive = asr.IsActive, Note = asr.Note,
                        LastUpdateDateTime = asr.LastUpdateDateTime, LastUpdateByUserID = asr.LastUpdateByUserID
                    })
                    .Where(asr => asr.StandardReferenceID == filter.ReferenceID)
                    .ToListAsync();

                return response == null || response.Count == 0 || !response.Any()
                    ? (ActionResult<AppStandardReference>)NotFound("App Standard Reference Not Found")
                    : (ActionResult<AppStandardReference>)Ok(response);
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
                    return BadRequest("AppStandardReference Are Required");
                }

                var refer = await _context.AppStandardReferences
                    .Where(sr => sr.StandardReferenceID == asr.StandardReferenceID)
                    .ToListAsync();

                if (refer.Any())
                {
                    return BadRequest($"{asr.StandardReferenceID} Already Exist");
                }

                var refence = new AppStandardReference
                {
                    StandardReferenceID = asr.StandardReferenceID, StandardReferenceName = asr.StandardReferenceName, ItemLength = asr.ItemLength,
                    IsUsedBySystem = asr.IsUsedBySystem, IsActive = asr.IsActive, Note = asr.Note, LastUpdateDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserID = asr.LastUpdateByUserID,
                };
                _context.AppStandardReferences.Add(refence);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Ok($"Parameter {asr.StandardReferenceID} Created Successfully")
                    : BadRequest($"Failed To Insert Data For ParameterID {asr.StandardReferenceID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReference", Name = "UpdateAppStandardReference")]
        public async Task<IActionResult> UpdateAppStandardReference([FromBody] AppStandardReference reference)
        {
            try
            {
                string date = DateFormat.DateTimeNow(Longyearpattern, DateFormat.DateTimeNow());

                if (string.IsNullOrEmpty(reference.StandardReferenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                int use = reference.IsUsedBySystem ? 1 : 0;
                int active = reference.IsActive ? 1 : 0;

                var query = $@"UPDATE `AppStandardReference`
                                SET `ItemLength` = '{reference.ItemLength}',
                                `IsUsedBySystem` = '{use}',
                                `IsActive` = '{active}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUserID` = '{reference.LastUpdateByUserID}',
                                `Note` = '{reference.Note}'
                                WHERE `StandardReferenceID` = '{reference.StandardReferenceID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                return response > 0
                    ? Ok($"{reference.StandardReferenceID} Update Successfully")
                    : NotFound($"{reference.StandardReferenceID} Not Found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteAppStandardReference", Name = "DeleteAppStandardReference")]
        public async Task<IActionResult> DeleteAppStandardReference([FromQuery] AppStandardReferenceFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.ReferenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                var query = $@"DELETE FROM `AppStandardReference`
                                WHERE `StandardReferenceID` = '{filter.ReferenceID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);
                return response > 0 ? Ok($"{filter.ReferenceID} Delete Successfully") : NotFound($"{filter.ReferenceID} Not Found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
