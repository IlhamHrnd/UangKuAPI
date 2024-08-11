using UangKuAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.BusinessObjects.Filter;

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
        public async Task<ActionResult<List<Province>>> GetAllProvince()
        {
            try
            {
                var response = await _context.Provinces
                    .Select(p => new Province
                    {
                        ProvID = p.ProvID, ProvName = p.ProvName, LocationID = p.LocationID, Status = p.Status
                    })
                    .OrderBy(p => p.ProvID)
                    .ToListAsync();

                return response == null || response.Count == 0 || !response.Any()
                    ? (ActionResult<List<Province>>)NotFound("Province Not Found")
                    : (ActionResult<List<Province>>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetAllCities", Name = "GetAllCities")]
        public async Task<ActionResult<List<Cities>>> GetAllCities([FromQuery] LocationFillter filter)
        {
            try
            {
                if (filter.ProvID == 0)
                {
                    return BadRequest("ProvID Is Required");
                }

                var response = await _context.Cities
                    .Select(c => new Cities
                    {
                        CityID = c.CityID, CityName = c.CityName, ProvID = c.ProvID
                    })
                    .Where(c => c.ProvID == filter.ProvID)
                    .OrderBy(c => c.CityID)
                    .ToListAsync();

                return response == null || response.Count == 0 || !response.Any()
                    ? (ActionResult<List<Cities>>)NotFound("Cities Not Found")
                    : (ActionResult<List<Cities>>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllDistrict", Name = "GetAllDistrict")]
        public async Task<ActionResult<List<District>>> GetAllDistrict([FromQuery] LocationFillter filter)
        {
            try
            {
                if (filter.CityID == 0)
                {
                    return BadRequest("CityID Is Required");
                }

                var response = await _context.Districts
                    .Select(d => new District
                    {
                        DisID = d.DisID, DisName = d.DisName, CityID = d.CityID
                    })
                    .Where(d => d.CityID == filter.CityID)
                    .OrderBy(d => d.DisID)
                    .ToListAsync();

                return response == null || response.Count == 0 || !response.Any()
                    ? (ActionResult<List<District>>)NotFound("District Not Found")
                    : (ActionResult<List<District>>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllSubDistrict", Name = "GetAllSubDistrict")]
        public async Task<ActionResult<List<Subdistrict>>> GetAllSubDistrict([FromQuery] LocationFillter filter)
        {
            try
            {
                if (filter.DistrictID == 0)
                {
                    return NotFound("DistrictID Is Required");
                }

                var response = await _context.Subdistricts
                    .Select(s => new Subdistrict
                    {
                        SubdisID = s.SubdisID, SubdisName = s.SubdisName, DisID = s.DisID
                    })
                    .Where(s => s.DisID == filter.DistrictID)
                    .OrderBy(s => s.SubdisID)
                    .ToListAsync();

                return response == null || response.Count == 0 || !response.Any()
                   ? (ActionResult<List<Subdistrict>>)NotFound("Subdistrict Not Found")
                   : (ActionResult<List<Subdistrict>>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetPostalCode", Name = "GetPostalCode")]
        public async Task<ActionResult<List<PostalCodes>>> GetPostalCode([FromQuery] LocationFillter filter)
        {
            try
            {
                var requiredFields = new Dictionary<int, string>
                {
                    { filter.ProvID, "ProvID Is Required" },
                    { filter.CityID, "CityID Is Required" },
                    { filter.DisID, "DisID Is Required" },
                    { filter.SubdisID, "SubdisID Is Required" }
                };

                foreach (var field in requiredFields)
                {
                    if (field.Key == 0)
                    {
                        return BadRequest(field.Value);
                    }
                }

                var response = await _context.PostalCodes
                    .Select(p => new PostalCodes
                    {
                        PostalID = p.ProvID, SubdisID = p.SubdisID, DisID = p.DisID, CityID = p.CityID, 
                        ProvID = p.ProvID, PostalCode = p.PostalCode
                    })
                    .Where(p => p.ProvID == filter.ProvID && p.CityID == filter.CityID && p.SubdisID == filter.SubdisID)
                    .ToListAsync();

                return response == null || response.Count == 0 || !response.Any()
                   ? (ActionResult<List<PostalCodes>>)NotFound("PostalCodes Not Found")
                   : (ActionResult<List<PostalCodes>>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
