using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Entity;
using System.Data;
using static UangKuAPI.BusinessObjects.Helper.AppConstant;

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
        public ActionResult<PageResponse<AppStandardReference>> GetAllReferenceID([FromQuery] AppStandardReferenceFilter filter)
        {
            try
            {
                var asrQ = new AppstandardreferenceQuery("asrQ");

                asrQ.Select(asrQ.StandardReferenceID, asrQ.StandardReferenceName, asrQ.ItemLength, asrQ.Note,
                    asrQ.LastUpdateDateTime, asrQ.LastUpdateByUserID,
                    "<CASE WHEN asrQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>",
                    "<CASE WHEN asrQ.IsActive = 1 THEN 'true' ELSE 'false' END AS 'IsActive'>")
                    .OrderBy(asrQ.StandardReferenceID.Ascending);
                DataTable dtRecord = asrQ.LoadDataTable();

                asrQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = asrQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var pagedData = new List<AppStandardReference>();

                foreach (DataRow dr in dt.Rows)
                {
                    var asr = new AppStandardReference
                    {
                        StandardReferenceID = (string)dr["StandardReferenceID"],
                        StandardReferenceName = (string)dr["StandardReferenceName"],
                        ItemLength = (int)dr["ItemLength"],
                        Note = (string)dr["Note"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserID = (string)dr["LastUpdateByUserID"],
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        IsActive = bool.Parse((string)dr["IsActive"])
                    };
                    pagedData.Add(asr);
                }

                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllReferenceID", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllReferenceID", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<AppStandardReference>>(pagedData, filter.PageNumber, filter.PageSize)
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
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetReferenceID", Name = "GetReferenceID")]
        public ActionResult<List<AppStandardReference>> GetReferenceID([FromQuery] AppStandardReferenceFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.ReferenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                var asrQ = new AppstandardreferenceQuery("asrQ");

                asrQ.Select(asrQ.StandardReferenceID, asrQ.StandardReferenceName, asrQ.ItemLength, asrQ.Note,
                    asrQ.LastUpdateDateTime, asrQ.LastUpdateByUserID,
                    "<CASE WHEN asrQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>",
                    "<CASE WHEN asrQ.IsActive = 1 THEN 'true' ELSE 'false' END AS 'IsActive'>")
                    .OrderBy(asrQ.StandardReferenceID.Ascending)
                    .Where(asrQ.StandardReferenceID == filter.ReferenceID);
                DataTable dt = asrQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var response = new List<AppStandardReference>();

                foreach (DataRow dr in dt.Rows)
                {
                    var asr = new AppStandardReference
                    {
                        StandardReferenceID = (string)dr["StandardReferenceID"],
                        StandardReferenceName = (string)dr["StandardReferenceName"],
                        ItemLength = (int)dr["ItemLength"],
                        Note = (string)dr["Note"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserID = (string)dr["LastUpdateByUserID"],
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        IsActive = bool.Parse((string)dr["IsActive"])
                    };
                    response.Add(asr);
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
                    ? Ok($"Standard Reference {asr.StandardReferenceID} Created Successfully")
                    : BadRequest($"Failed To Insert Data For ReferenceID {asr.StandardReferenceID}");
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
                if (string.IsNullOrEmpty(reference.StandardReferenceID))
                {
                    return BadRequest("ReferenceID Is Required");
                }

                var refer = await _context.AppStandardReferences
                    .FirstOrDefaultAsync(asr => asr.StandardReferenceID == reference.StandardReferenceID);

                if (refer == null)
                {
                    return BadRequest($"{reference.StandardReferenceID} Not Found");
                }

                refer.ItemLength = reference.ItemLength;
                refer.IsUsedBySystem = reference.IsUsedBySystem;
                refer.IsActive = reference.IsActive;
                refer.LastUpdateDateTime = DateFormat.DateTimeNow();
                refer.LastUpdateByUserID = reference.LastUpdateByUserID;
                refer.Note = reference.Note;
                _context.Update(refer);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{reference.StandardReferenceID} Updated Successfully")
                    : BadRequest($"Failed To Update Data For ReferenceID {reference.StandardReferenceID}");
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

                var refer = await _context.AppStandardReferences
                    .FirstOrDefaultAsync(asr => asr.StandardReferenceID == filter.ReferenceID);

                if (refer == null)
                {
                    return BadRequest($"{filter.ReferenceID} Not Found");
                }
                _context.Remove(refer);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{filter.ReferenceID} Delete Successfully")
                    : BadRequest($"Failed To Delete Data For ReferenceID {filter.ReferenceID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
