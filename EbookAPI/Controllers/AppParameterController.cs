using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Response;
using UangKuAPI.EntityFramework.Models;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "AppParameterAPI")]
    [ApiController]
    public class AppParameterController : ControllerBase
    {
        private readonly BaseFramework _context;

        public AppParameterController(BaseFramework context)
        {
            _context = context;
        }

        [HttpGet("GetAllAppParameter", Name = "GetAllAppParameter")]
        public ActionResult<PageResponse<AppParameter>> GetAllAppParameter([FromQuery] AppParameterFilter filter)
        {
            var pagedData = new List<AppParameter>();
            var response = new PageResponse<List<AppParameter>>(pagedData, 0, 0);

            try
            {
                var record = (from ap in _context.AppParameters
                              select ap).ToList();
                if (record.Count == 0)
                    return NotFound(response = new PageResponse<List<AppParameter>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = pagedData.Count > 0
                    });

                var data = (from ap in _context.AppParameters
                            orderby ap.ParameterId ascending
                            select ap)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToList();

                var totalRecord = record.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllAppParameter", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllAppParameter", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                return Ok(response = new PageResponse<List<AppParameter>>(data, filter.PageNumber, filter.PageSize)
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
                response = new PageResponse<List<AppParameter>>(pagedData, 0, 0)
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

        [HttpGet("GetAllParameterWithNoPageFilter", Name = "GetAllParameterWithNoPageFilter")]
        public ActionResult<Response<List<AppParameter>>> GetAllParameterWithNoPageFilter()
        {
            var pagedData = new List<AppParameter>();
            var response = new Response<List<AppParameter>>();

            try
            {
                var query = (from ap in _context.AppParameters
                             orderby ap.ParameterId ascending
                             select ap).ToList();

                if (query.Count == 0)
                    return NotFound(response = new Response<List<AppParameter>>
                    {
                        Data = pagedData,
                        Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = pagedData.Count > 0
                    });

                return Ok(response = new Response<List<AppParameter>>
                {
                    Data = query,
                    Message = query.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = query.Count > 0
                });
            }
            catch (Exception e)
            {
                response = new Response<List<AppParameter>>
                {
                    Data = pagedData,
                    Message = $"{(pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = pagedData.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetParameterID", Name = "GetParameterID")]
        public ActionResult<Response<AppParameter>> GetParameterID([FromQuery] AppParameterFilter filter)
        {
            var data = new AppParameter();
            var response = new Response<AppParameter>();

            try
            {
                if (string.IsNullOrEmpty(filter.ParameterID))
                {
                    response = new Response<AppParameter>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ParameterID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ParameterID)
                    };
                    return BadRequest(response);
                }

                var query = (from ap in _context.AppParameters
                             where ap.ParameterId == filter.ParameterID
                             select ap).FirstOrDefault();

                if (string.IsNullOrEmpty(query?.ParameterId))
                    return NotFound(response = new Response<AppParameter>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(query?.ParameterId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(query?.ParameterId)
                    });

                return Ok(response = new Response<AppParameter>
                {
                    Data = query,
                    Message = !string.IsNullOrEmpty(query.ParameterId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(query.ParameterId)
                });
            }
            catch (Exception e)
            {
                response = new Response<AppParameter>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.ParameterId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.ParameterId)
                };
                return BadRequest(response);
            }
        }

        [HttpPost("PostAppParameter", Name = "PostAppParameter")]
        public async Task<IActionResult> PostAppParameter([FromBody] AppParameter ap)
        {
            try
            {
                if (ap == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "AppParameter"));

                var data = await _context.AppParameters
                    .FirstOrDefaultAsync(p => p.ParameterId == ap.ParameterId);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, ap.ParameterId));

                var p = new AppParameter
                {
                    ParameterId = ap.ParameterId, ParameterName = ap.ParameterName, ParameterValue = ap.ParameterValue,
                    LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserId = ap.LastUpdateByUserId, IsUsedBySystem = ap.IsUsedBySystem,
                    Srcontrol = ap.Srcontrol
                };
                _context.AppParameters.Add(p);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Parameter", ap.ParameterId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "ParameterID", ap.ParameterId));
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
                if (string.IsNullOrEmpty(ap.ParameterId))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "ParameterID"));

                var data = await _context.AppParameters
                    .FirstOrDefaultAsync(p => p.ParameterId == ap.ParameterId);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.ParameterName = ap.ParameterName;
                data.ParameterValue = ap.ParameterValue;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUserId = ap.LastUpdateByUserId;
                data.IsUsedBySystem = ap.IsUsedBySystem;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, ap.ParameterId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "ParameterID", ap.ParameterId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
