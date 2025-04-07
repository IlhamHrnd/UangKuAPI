using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Response;
using UangKuAPI.EntityFramework.Models;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "AppStandardReferenceAPI")]
    [ApiController]
    public class AppStandardReferenceController : ControllerBase
    {
        private readonly BaseFramework _context;

        public AppStandardReferenceController(BaseFramework context)
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
                var record = (from asr in _context.AppStandardReferences
                              select asr).ToList();

                if (record.Count == 0)
                    return NotFound(response = new PageResponse<List<AppStandardReference>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = pagedData.Count > 0
                    });

                var data = (from asr in _context.AppStandardReferences
                            orderby asr.StandardReferenceId ascending
                            select asr)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToList();

                var totalRecord = record.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllReferenceID", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllReferenceID", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                return Ok(response = new PageResponse<List<AppStandardReference>>(data, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                });
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
        public ActionResult<Response<AppStandardReference>> GetReferenceID([FromQuery] AppStandardReferenceFilter filter)
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

                var query = (from asr in _context.AppStandardReferences
                             where asr.StandardReferenceId == filter.ReferenceID
                             select asr).FirstOrDefault();

                if (string.IsNullOrEmpty(query?.StandardReferenceId))
                    return NotFound(response = new Response<AppStandardReference>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(query?.StandardReferenceId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(query?.StandardReferenceId)
                    });

                return Ok(response = new Response<AppStandardReference>
                {
                    Data = query,
                    Message = !string.IsNullOrEmpty(query.StandardReferenceId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(query.StandardReferenceId)
                });
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
                    return NotFound(AppConstant.NotFoundMsg);

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

        [HttpGet("GetNewStandardReferenceID", Name = "GetNewStandardReferenceID")]
        public ActionResult<Response<string>> GetNewStandardReferenceID([FromQuery] AppStandardReferenceFilter filter)
        {
            var data = string.Empty;
            var response = new Response<string>();

            try
            {
                if (string.IsNullOrEmpty(filter.ReferenceID))
                {
                    response = new Response<string>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ReferenceID"),
                        Succeeded = !string.IsNullOrEmpty(data)
                    };
                    return BadRequest(response);
                }

                var asr = new G.Appstandardreference();
                data = asr.LoadByPrimaryKey(filter.ReferenceID) ? asr.StandardReferenceID : string.Empty;

                response = new Response<string>
                {
                    Data = string.IsNullOrEmpty(data) ? filter.ReferenceID : string.Empty,
                    Message = !string.IsNullOrEmpty(data) ? string.Format(AppConstant.AlreadyExistMsg, "ReferenceID") : AppConstant.FoundMsg,
                    Succeeded = string.IsNullOrEmpty(data)
                };
                return !string.IsNullOrEmpty(data) ? BadRequest(response) : Ok(response);
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
    }
}