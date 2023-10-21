using EbookAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Model;

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
        public async Task<ActionResult<Province>> GetAllProvince()
        {
            try
            {
                var query = $@"SELECT p.ProvID, p.ProvName, p.LocationID, p.Status
                                FROM Provinces AS p
                                ORDER BY p.ProvID ASC;";
                var response = await _context.Provinces.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("User Not Found");
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
        public async Task<ActionResult<Cities>> GetAllCities([FromQuery] CitiesFilter filter)
        {
            try
            {
                if (filter.ProvID == 0)
                {
                    return BadRequest("ProvID Is Required");
                }
                var query = $@"SELECT c.CityID, c.CityName, c.ProvID
                                FROM Cities AS c
                                WHERE c.ProvID = '{filter.ProvID}'
                                ORDER BY c.CityID;";
                var response = await _context.Cities.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("Cities Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllDistrict", Name = "GetAllDistrict")]
        public async Task<ActionResult<District>> GetAllDistrict([FromQuery] DistrictFilter filter)
        {
            try
            {
                if (filter.CityID == 0)
                {
                    return BadRequest("CityID Is Required");
                }
                var query = $@"SELECT d.DisID, d.DisName, d.CityID
                                FROM Districts AS d
                                WHERE d.CityID = '{filter.CityID}'
                                ORDER BY d.DisID ASC;";
                var response = await _context.Districts.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("District Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllSubDistrict", Name = "GetAllSubDistrict")]
        public async Task<ActionResult<Subdistrict>> GetAllSubDistrict([FromQuery] SubDistrictFilter filter)
        {
            try
            {
                if (filter.DistrictID == 0)
                {
                    return NotFound("DistrictID Is Required");
                }
                var query = $@"SELECT s.SubdisID, s.SubdisName, s.DisID
                                FROM Subdistricts AS s
                                WHERE s.DisID = '{filter.DistrictID}'
                                ORDER BY s.SubdisID ASC;";
                var response = await _context.Subdistricts.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("Sub District Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetPostalCode", Name = "GetPostalCode")]
        public async Task<ActionResult<PostalCodes>> GetPostalCode([FromQuery] PostalCodeFilter filter)
        {
            try
            {
                if (filter.ProvID == 0)
                {
                    return BadRequest("ProvID Is Required");
                }
                if (filter.CityID == 0)
                {
                    return BadRequest("CityID Is Required");
                }
                if (filter.DisID == 0)
                {
                    return BadRequest("DisID Is Required");
                }
                if (filter.SubdisID == 0)
                {
                    return BadRequest("SubdisIS Is Required");
                }
                var query = $@"SELECT pc.PostalID, pc.SubdisID, pc.DisID, pc.CityID, pc.ProvID, pc.PostalCode
                                FROM PostalCode AS pc
                                WHERE pc.ProvID = '{filter.ProvID}' AND pc.CityID = '{filter.CityID}'
		                            AND pc.SubdisID = '{filter.SubdisID}';";
                //Filter Untuk District / Kecamatan Di Komen Dulu Karna Data DisID Di Tabel PostalCode Tidak Sesuai Dengan Yang Ada Di Tabel District
                //AND pc.DisID = '{filter.DisID}'
                var response = await _context.PostalCodes.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("PostalCode Not Found");
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
