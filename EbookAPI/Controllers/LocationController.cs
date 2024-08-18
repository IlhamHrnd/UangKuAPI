using UangKuAPI.Context;
using Microsoft.AspNetCore.Mvc;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Entity;
using System.Data;
using BusinessObjects;
using Models = UangKuAPI.BusinessObjects.Model;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "LocationAPI")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllProvince", Name = "GetAllProvince")]
        public async Task<ActionResult<List<Models.Province>>> GetAllProvince()
        {
            try
            {
                var pQ = new ProvincesQuery("pQ");

                pQ.Select(pQ.ProvID, pQ.ProvName, pQ.LocationID, pQ.Status)
                    .OrderBy(pQ.ProvID.Ascending);
                DataTable dt = pQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return BadRequest($"Data Not Found");
                }

                var response = new List<Models.Province>();

                foreach (DataRow dr in dt.Rows)
                {
                    var prov = new Models.Province
                    {
                        ProvID = (int)dr["ProvID"],
                        ProvName = (string)dr["ProvName"],
                        LocationID = (int)dr["LocationID"],
                        Status = (int)dr["Status"]
                    };
                    response.Add(prov);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetAllCities", Name = "GetAllCities")]
        public async Task<ActionResult<List<Models.Cities>>> GetAllCities([FromQuery] LocationFillter filter)
        {
            try
            {
                if (filter.ProvID == 0)
                {
                    return BadRequest("ProvID Is Required");
                }

                var cQ = new CitiesQuery("cQ");

                cQ.Select(cQ.CityID, cQ.CityName, cQ.ProvID)
                    .Where(cQ.ProvID == filter.ProvID)
                    .OrderBy(cQ.CityID.Ascending);
                DataTable dt = cQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return BadRequest($"Data Not Found");
                }

                var response = new List<Models.Cities>();

                foreach (DataRow dr in dt.Rows)
                {
                    var city = new Models.Cities
                    {
                        CityID = (int)dr["CityID"],
                        CityName = (string)dr["CityName"],
                        ProvID = (int)dr["ProvID"]
                    };
                    response.Add(city);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllDistrict", Name = "GetAllDistrict")]
        public async Task<ActionResult<List<Models.District>>> GetAllDistrict([FromQuery] LocationFillter filter)
        {
            try
            {
                if (filter.CityID == 0)
                {
                    return BadRequest("CityID Is Required");
                }

                var disQ = new DistrictsQuery("disQ");

                disQ.Select(disQ.DisID, disQ.DisName, disQ.CityID)
                    .Where(disQ.CityID == filter.CityID)
                    .OrderBy(disQ.DisID.Ascending);
                DataTable dt = disQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return BadRequest($"Data Not Found");
                }

                var response = new List<Models.District>();

                foreach (DataRow dr in dt.Rows)
                {
                    var dis = new Models.District
                    {
                        DisID = (int)dr["DisID"],
                        DisName = (string)dr["DisName"],
                        CityID = (int)dr["CityID"]
                    };
                    response.Add(dis);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllSubDistrict", Name = "GetAllSubDistrict")]
        public async Task<ActionResult<List<Models.Subdistrict>>> GetAllSubDistrict([FromQuery] LocationFillter filter)
        {
            try
            {
                if (filter.DistrictID == 0)
                {
                    return NotFound("DistrictID Is Required");
                }

                var sdQ = new SubdistrictsQuery("sdQ");

                sdQ.Select(sdQ.SubdisID, sdQ.SubdisName, sdQ.DisID)
                    .Where(sdQ.DisID == filter.DistrictID)
                    .OrderBy(sdQ.SubdisID.Ascending);
                DataTable dt = sdQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return BadRequest($"Data Not Found");
                }

                var response = new List<Models.Subdistrict>();

                foreach (DataRow dr in dt.Rows)
                {
                    var sd = new Models.Subdistrict
                    {
                        SubdisID = (int)dr["SubdisID"],
                        SubdisName = (string)dr["SubdisName"],
                        DisID = (int)dr["DisID"]
                    };
                    response.Add(sd);
                }
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetPostalCode", Name = "GetPostalCode")]
        public async Task<ActionResult<Models.PostalCodes>> GetPostalCode([FromQuery] LocationFillter filter)
        {
            try
            {
                var requiredFields = new Dictionary<int, string>
                {
                    { filter.ProvID, "ProvID Is Required" },
                    { filter.CityID, "CityID Is Required" },
                    { filter.DistrictID, "DistrictID Is Required" },
                    { filter.SubdisID, "SubdisID Is Required" }
                };

                foreach (var field in requiredFields)
                {
                    if (field.Key == 0)
                    {
                        return BadRequest(field.Value);
                    }
                }

                var pcQ = new PostalcodeQuery("pcQ");

                pcQ.Select(pcQ.PostalID, pcQ.SubdisID, pcQ.DisID, pcQ.CityID, pcQ.ProvID, pcQ.PostalCode)
                    .Where(pcQ.ProvID == filter.ProvID && pcQ.CityID == filter.CityID && pcQ.SubdisID == filter.SubdisID);
                DataTable dt = pcQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return BadRequest($"Data Not Found");
                }

                var response = new List<Models.PostalCodes>();

                foreach (DataRow dr in dt.Rows)
                {
                    var pc = new Models.PostalCodes
                    {
                        PostalID = dr["PostalID"] != DBNull.Value ? Convert.ToInt32(dr["PostalID"]) : 0,
                        SubdisID = dr["SubdisID"] != DBNull.Value ? Convert.ToInt32(dr["SubdisID"]) : 0,
                        DisID = dr["DisID"] != DBNull.Value ? Convert.ToInt32(dr["DisID"]) : 0,
                        CityID = dr["CityID"] != DBNull.Value ? Convert.ToInt32(dr["CityID"]) : 0,
                        ProvID = dr["ProvID"] != DBNull.Value ? Convert.ToInt32(dr["ProvID"]) : 0,
                        PostalCode = dr["PostalCode"] != DBNull.Value ? Convert.ToInt32(dr["PostalCode"]) : 0
                    };
                    response.Add(pc);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
