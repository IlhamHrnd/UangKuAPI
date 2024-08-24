using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using UangKuAPI.BusinessObjects.Entity;
using System.Data;

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
        public async Task<ActionResult<AppParameter>> GetAllAppParameter([FromQuery] AppParameterFilter filter)
        {
            try
            {
                var aQ = new AppparameterQuery("aQ");

                aQ.Select(aQ.ParameterID, aQ.ParameterName, aQ.ParameterValue, aQ.LastUpdateDateTime, 
                    aQ.LastUpdateByUserID, aQ.SRControl,
                    "<CASE WHEN aQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>")
                    .OrderBy(aQ.ParameterID.Ascending);
                DataTable dtRecord = aQ.LoadDataTable();

                aQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = aQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var pagedData = new List<AppParameter>();

                foreach (DataRow dr in dt.Rows)
                {
                    var a = new AppParameter
                    {
                        ParameterID = (string)dr["ParameterID"],
                        ParameterName = (string)dr["ParameterName"],
                        ParameterValue = (string)dr["ParameterValue"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserID = (string)dr["LastUpdateByUserID"],
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        SRControl = (string)dr["SRControl"]
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

                var response = new PageResponse<List<AppParameter>>(pagedData, filter.PageNumber, filter.PageSize)
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

        [HttpGet("GetAllParameterWithNoPageFilter", Name = "GetAllParameterWithNoPageFilter")]
        public async Task<ActionResult<AppParameter>> GetAllParameterWithNoPageFilter()
        {
            try
            {
                var aQ = new AppparameterQuery("aQ");

                aQ.Select(aQ.ParameterID, aQ.ParameterName, aQ.ParameterValue, aQ.LastUpdateDateTime,
                    aQ.LastUpdateByUserID, aQ.SRControl,
                    "<CASE WHEN aQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>")
                    .OrderBy(aQ.ParameterID.Ascending);
                var dt = aQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var response = new List<AppParameter>();

                foreach (DataRow dr in dt.Rows)
                {
                    var a = new AppParameter
                    {
                        ParameterID = (string)dr["ParameterID"],
                        ParameterName = (string)dr["ParameterName"],
                        ParameterValue = (string)dr["ParameterValue"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserID = (string)dr["LastUpdateByUserID"],
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        SRControl = (string)dr["SRControl"]
                    };
                    response.Add(a);
                }

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

                var aQ = new AppparameterQuery("aQ");

                aQ.Select(aQ.ParameterID, aQ.ParameterName, aQ.ParameterValue, aQ.LastUpdateDateTime,
                    aQ.LastUpdateByUserID, aQ.SRControl,
                    "<CASE WHEN aQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>")
                    .OrderBy(aQ.ParameterID.Ascending)
                    .Where(aQ.ParameterID == filter.ParameterID);
                var dt = aQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var response = new List<AppParameter>();

                foreach (DataRow dr in dt.Rows)
                {
                    var a = new AppParameter
                    {
                        ParameterID = (string)dr["ParameterID"],
                        ParameterName = (string)dr["ParameterName"],
                        ParameterValue = (string)dr["ParameterValue"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserID = (string)dr["LastUpdateByUserID"],
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        SRControl = (string)dr["SRControl"]
                    };
                    response.Add(a);
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
