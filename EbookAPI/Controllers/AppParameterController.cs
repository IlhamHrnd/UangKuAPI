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
                var pageNumber = filter.PageNumber;
                var pageSize = filter.PageSize;
                var pagedData = await _context.Parameter
                    .Select(p => new AppParameter
                    {
                        ParameterID = p.ParameterID, ParameterName = p.ParameterName,
                        ParameterValue = p.ParameterValue, LastUpdateDateTime = p.LastUpdateDateTime,
                        LastUpdateByUserID = p.LastUpdateByUserID, IsUsedBySystem = p.IsUsedBySystem,
                        SRControl = p.SRControl
                    })
                    .OrderBy(p => p.ParameterID)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var totalRecord = await _context.Parameter.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllAppParameter", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllAppParameter", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var isDataFound = _context.Parameter.Any();
                var response = new PageResponse<List<AppParameter>>(pagedData, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = isDataFound ? "Data Found" : "Data Not Found",
                    Succeeded = isDataFound
                };

                return isDataFound 
                    ? Ok(response) 
                    : NotFound(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllParameterWithNoPageFilter", Name = "GetAllParameterWithNoPageFilter")]
        public async Task<ActionResult<List<AppParameter>>> GetAllParameterWithNoPageFilter()
        {
            try
            {
                var response = await _context.Parameter
                    .Select(p => new AppParameter
                    {
                        ParameterID = p.ParameterID, ParameterName = p.ParameterName, ParameterValue = p.ParameterValue,
                        LastUpdateDateTime = p.LastUpdateDateTime, LastUpdateByUserID = p.LastUpdateByUserID, IsUsedBySystem = p.IsUsedBySystem,
                        SRControl = p.SRControl
                    })
                    .OrderBy(p => p.ParameterID)
                    .ToListAsync();

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetParameterID", Name = "GetParameterID")]
        public async Task<ActionResult<AppParameter>> GetParameterID([FromQuery] AppParameterFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.ParameterID))
                {
                    return BadRequest($"ParameterID Is Required");
                }

                var response = await _context.Parameter
                    .Select(p => new AppParameter
                    {
                        ParameterID = p.ParameterID, ParameterName = p.ParameterName, ParameterValue = p.ParameterValue,
                        LastUpdateDateTime = p.LastUpdateDateTime, LastUpdateByUserID = p.LastUpdateByUserID, IsUsedBySystem = p.IsUsedBySystem,
                        SRControl = p.SRControl
                    })
                    .Where(p => p.ParameterID == filter.ParameterID)
                    .ToListAsync();

                return response == null || response.Count == 0 || !response.Any() 
                    ? (ActionResult<AppParameter>)NotFound("App Parameter Not Found") 
                    : (ActionResult<AppParameter>)Ok(response);
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

                var checkParam = await _context.Parameter
                    .FirstOrDefaultAsync(p => p.ParameterID == ap.ParameterID);

                if (checkParam != null)
                {
                    return BadRequest($"{ap.ParameterID} Already Exist");
                }
                
                var param = new AppParameter
                {
                    ParameterID = ap.ParameterID, ParameterName = ap.ParameterName, ParameterValue = ap.ParameterValue,
                    LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserID = ap.LastUpdateByUserID, IsUsedBySystem = ap.IsUsedBySystem,
                    SRControl = ap.SRControl
                };
                _context.Parameter.Add(param);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Ok($"Parameter {ap.ParameterID} Created Successfully")
                    : BadRequest($"Failed To Insert Data For ParameterID {ap.ParameterID}");
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
                string date = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);
                int use = ap.IsUsedBySystem == true ? 1 : 0;

                if (string.IsNullOrEmpty(ap.ParameterID))
                {
                    return BadRequest($"ParameterID Is Required");
                }

                var param = await _context.Parameter
                    .FirstOrDefaultAsync(p => p.ParameterID == ap.ParameterID);

                if (param == null)
                {
                    return NotFound($"{ap.ParameterID} Not Found");
                }

                param.ParameterName = ap.ParameterName;
                param.ParameterValue = ap.ParameterValue;
                param.LastUpdateDateTime = DateFormat.DateTimeNow();
                param.LastUpdateByUserID = ap.LastUpdateByUserID;
                param.IsUsedBySystem = ap.IsUsedBySystem;
                param.SRControl = ap.SRControl;
                _context.Update(param);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{ap.ParameterID} Updated Successfully")
                    : BadRequest($"Failed To Update Data For ParameterID {ap.ParameterID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
