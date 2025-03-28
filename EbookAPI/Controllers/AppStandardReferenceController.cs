﻿using EbookAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
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
                if (response == null || response.Count == 0 || !response.Any())
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
                    return BadRequest("AppStandardReference Are Required");
                }

                string date = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, DateTime.Now);
                int use = asr.IsUsedBySystem == true ? 1 : 0;
                int active = asr.IsActive == true ? 1 : 0;

                var query = $@"INSERT INTO `AppStandardReference`(`StandardReferenceID`, `StandardReferenceName`, `ItemLength`,
                                `IsUsedBySystem`, `IsActive`, `Note`, `LastUpdateDateTime`, `LastUpdateByUserID`)
                                VALUES('{asr.StandardReferenceID}', '{asr.StandardReferenceName}', '{asr.ItemLength}',
                                '{use}', '{active}', '{asr.Note}', '{date}', '{asr.LastUpdateByUserID}');";

                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

                if (rowsAffected > 0)
                {
                    return Ok($"Standard ReferenceID {asr.StandardReferenceID} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For Standard ReferenceID {asr.StandardReferenceID}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReference", Name = "UpdateAppStandardReference")]
        public async Task<IActionResult> UpdateAppStandardReference([FromQuery] string referenceID, [FromQuery] int itemLength, [FromQuery] bool isActive, [FromQuery] bool isUse, [FromQuery] string user, [FromQuery] string note)
        {
            try
            {
                string date = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, DateTime.Now);

                if (string.IsNullOrEmpty(referenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                int use = isUse ? 1 : 0;
                int active = isActive ? 1 : 0;

                var query = $@"UPDATE `AppStandardReference`
                                SET `ItemLength` = '{itemLength}',
                                `IsUsedBySystem` = '{use}',
                                `IsActive` = '{active}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUserID` = '{user}',
                                `Note` = '{note}'
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
