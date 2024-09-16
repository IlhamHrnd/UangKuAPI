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
            var pagedData = new List<AppStandardReference>();
            var response = new PageResponse<List<AppStandardReference>>(pagedData, 0, 0);

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
                    response = new PageResponse<List<AppStandardReference>>(pagedData, 0, 0)
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

                foreach(DataRow dr in dt.Rows)
                {
                    var asr = new AppStandardReference
                    {
                        StandardReferenceId = (string)dr["StandardReferenceID"],
                        StandardReferenceName = dr["StandardReferenceName"] != DBNull.Value ? (string)dr["StandardReferenceName"] : string.Empty,
                        ItemLength = dr["ItemLength"] != DBNull.Value ? (int)dr["ItemLength"] : 0,
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        IsActive = bool.Parse((string)dr["IsActive"]),
                        Note = dr["Note"] != DBNull.Value ? (string)dr["Note"] : string.Empty,
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"]
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

                response = new PageResponse<List<AppStandardReference>>(pagedData, filter.PageNumber, filter.PageSize)
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
                response = new PageResponse<List<AppStandardReference>>(pagedData, 0, 0)
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

        [HttpGet("GetReferenceID", Name = "GetReferenceID")]
        public ActionResult<Response<Appstandardreference>> GetReferenceID([FromQuery] AppStandardReferenceFilter filter)
        {
            var data = new AppStandardReference();
            var response = new Response<AppStandardReference>();

            try
            {
                if (string.IsNullOrEmpty(filter.ReferenceID))
                {
                    response = new Response<AppStandardReference>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ReferenceID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ReferenceID)
                    };
                    return BadRequest(response);
                }

                var asr = new Appstandardreference();

                if (!asr.LoadByPrimaryKey(filter.ReferenceID))
                {
                    response = new Response<AppStandardReference>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(asr.StandardReferenceID) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(asr.StandardReferenceID)
                    };
                    return NotFound(response);
                }

                data = new AppStandardReference
                {
                    StandardReferenceId = asr.StandardReferenceID,
                    StandardReferenceName = !string.IsNullOrEmpty(asr.StandardReferenceName) ? asr.StandardReferenceName : string.Empty,
                    ItemLength = asr.ItemLength > 0 ? asr.ItemLength : 0
                };
                response = new Response<AppStandardReference>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(asr.StandardReferenceID) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(asr.StandardReferenceID)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<AppStandardReference>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.StandardReferenceId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.StandardReferenceId)
                };
                return BadRequest(response);
            }
        }

        [HttpPost("CreateAppStandardReference", Name = "CreateAppStandardReference")]
        public async Task<IActionResult> CreateAppStandardReference([FromBody] AppStandardReference asr)
        {
            try
            {
                if (asr == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "AppStandardRefence"));

                var data = await _context.AppStandardReferences
                    .FirstOrDefaultAsync(a => a.StandardReferenceId == asr.StandardReferenceId);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, asr.StandardReferenceId));
                
                var a = new AppStandardReference
                {
                    StandardReferenceId = asr.StandardReferenceId, StandardReferenceName = asr.StandardReferenceName, ItemLength = asr.ItemLength,
                    IsUsedBySystem = asr.IsUsedBySystem, IsActive = asr.IsActive, Note = asr.Note, LastUpdateDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserId = asr.LastUpdateByUserId
                };
                _context.AppStandardReferences.Add(a);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Standard Reference", asr.StandardReferenceId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Standard ReferenceID", asr.StandardReferenceId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReference", Name = "UpdateAppStandardReference")]
        public async Task<IActionResult> UpdateAppStandardReference([FromBody] AppStandardReference asr)
        {
            try
            {
                if (string.IsNullOrEmpty(asr.StandardReferenceId))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Standard ReferenceID"));
                
                var data = await _context.AppStandardReferences
                    .FirstOrDefaultAsync(a => a.StandardReferenceId == asr.StandardReferenceId);

                if (data == null)
                    return NotFound(string.Format(AppConstant.NotFoundMsg, asr.StandardReferenceId));

                data.StandardReferenceName = asr.StandardReferenceName;
                data.ItemLength = asr.ItemLength;
                data.IsUsedBySystem = asr.IsUsedBySystem;
                data.IsActive = asr.IsActive;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUserId = asr.LastUpdateByUserId;
                data.Note = asr.Note;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, asr.StandardReferenceId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Standard ReferenceID", asr.StandardReferenceId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}