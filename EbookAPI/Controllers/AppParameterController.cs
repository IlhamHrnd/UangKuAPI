using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Entity.Generated;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

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
        public ActionResult<PageResponse<AppParameter>> GetAllAppParameter([FromQuery] AppParameterFilter filter)
        {
            var pagedData = new List<AppParameter>();
            var response = new PageResponse<List<AppParameter>>(pagedData, 0, 0);

            try
            {
                var aQ = new AppparameterQuery("aQ");

                aQ.Select(aQ.ParameterID, aQ.ParameterName, aQ.ParameterValue, aQ.LastUpdateDateTime,
                    aQ.LastUpdateByUserID, aQ.SRControl, aQ.IsUsedBySystem)
                    .OrderBy(aQ.ParameterID.Ascending);
                DataTable dtRecord = aQ.LoadDataTable();

                aQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = aQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new PageResponse<List<AppParameter>>(pagedData, 0, 0)
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

                foreach (DataRow dr in dt.Rows)
                {
                    var a = new AppParameter
                    {
                        ParameterId = (string)dr["ParameterID"],
                        ParameterName = dr["ParameterName"] != DBNull.Value ? (string)dr["ParameterName"] : string.Empty,
                        ParameterValue = dr["ParameterValue"] != DBNull.Value ? (string)dr["ParameterValue"] : string.Empty,
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"],
                        Srcontrol = (string)dr["SRControl"],
                        IsUsedBySystem = (UInt64)dr["IsUsedBySystem"] == 1
                    };
                    pagedData.Add(a);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllAppParameter", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllAppParameter", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<AppParameter>>(pagedData, filter.PageNumber, filter.PageSize)
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
                var aQ = new AppparameterQuery("aQ");

                aQ.Select(aQ.ParameterID, aQ.ParameterName, aQ.ParameterValue, aQ.LastUpdateDateTime,
                    aQ.LastUpdateByUserID, aQ.SRControl, aQ.IsUsedBySystem)
                    .OrderBy(aQ.ParameterID.Ascending);
                var dt = aQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new PageResponse<List<AppParameter>>(pagedData, 0, 0)
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

                foreach (DataRow dr in dt.Rows)
                {
                    var a = new AppParameter
                    {
                        ParameterId = (string)dr["ParameterID"],
                        ParameterName = dr["ParameterName"] != DBNull.Value ? (string)dr["ParameterName"] : string.Empty,
                        ParameterValue = dr["ParameterValue"] != DBNull.Value ? (string)dr["ParameterValue"] : string.Empty,
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"],
                        Srcontrol = (string)dr["SRControl"],
                        IsUsedBySystem = (UInt64)dr["IsUsedBySystem"] == 1
                    };
                    pagedData.Add(a);
                }

                response = new Response<List<AppParameter>>
                {
                    Data = pagedData,
                    Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = pagedData.Count > 0
                };
                return Ok(response);
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

                var a = new Appparameter();

                if (!a.LoadByPrimaryKey(filter.ParameterID))
                {
                    response = new Response<AppParameter>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(a.ParameterID) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(a.ParameterID)
                    };
                    return NotFound(response);
                }

                data = new AppParameter
                {
                    ParameterId = a.ParameterID,
                    ParameterName = a.ParameterName,
                    ParameterValue = a.ParameterValue,
                    LastUpdateDateTime = a.LastUpdateDateTime ?? DateTime.Now,
                    LastUpdateByUserId = a.LastUpdateByUserID,
                    IsUsedBySystem = a.IsUsedBySystem == 1,
                    Srcontrol = a.SRControl
                };
                response = new Response<AppParameter>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data.ParameterId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(filter.ParameterID)
                };
                return Ok(response);
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
                    return NotFound(string.Format(AppConstant.NotFoundMsg, ap.ParameterId));

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
