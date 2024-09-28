using System.Data;
using Microsoft.AspNetCore.Mvc;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Entity.Generated;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

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
        public ActionResult<Response<List<Province>>> GetAllProvince([FromQuery] LocationFilter filter)
        {
            var data = new List<Province>();
            var response = new Response<List<Province>>();

            try
            {
                var pQ = new ProvincesQuery("pQ");

                pQ.Select(pQ.ProvID, pQ.ProvName, pQ.LocationID, pQ.Status)
                    .OrderBy(pQ.ProvName.Ascending);
                var dt = pQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<List<Province>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var prov = new Province
                    {
                        ProvId = (int)dr["ProvID"],
                        ProvName = dr["ProvName"] != DBNull.Value ? (string)dr["ProvName"] : string.Empty,
                        LocationId = dr["LocationID"] != DBNull.Value ? (int)dr["LocationID"] : 0,
                        Status = dr["Status"] != DBNull.Value ? (int)dr["Status"] : 0
                    };
                    data.Add(prov);
                }

                response = new Response<List<Province>>
                {
                    Data = data,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<List<Province>>
                {
                    Data = data,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetAllCities", Name = "GetAllCities")]
        public ActionResult<Response<List<City>>> GetAllCities([FromQuery] LocationFilter filter)
        {
            var data = new List<City>();
            var response = new Response<List<City>>();

            try
            {
                if (string.IsNullOrEmpty(filter.ProvID))
                {
                    response = new Response<List<City>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ProvincesID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ProvID)
                    };
                    return BadRequest(response);
                }

                var cQ = new CitiesQuery("cQ");

                cQ.Select(cQ.CityID, cQ.CityName, cQ.ProvID)
                    .Where(cQ.ProvID == filter.ProvID)
                    .OrderBy(cQ.CityName.Ascending);
                var dt = cQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<List<City>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var city = new City
                    {
                        CityId = (int)dr["CityID"],
                        CityName = dr["CityName"] != DBNull.Value ? (string)dr["CityName"] : string.Empty,
                        ProvId = dr["ProvID"] != DBNull.Value ? (int)dr["ProvID"] : 0
                    };
                    data.Add(city);
                }

                response = new Response<List<City>>
                {
                    Data = data,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<List<City>>
                {
                    Data = data,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetAllDistrict", Name = "GetAllDistrict")]
        public ActionResult<Response<List<District>>> GetAllDistrict([FromQuery] LocationFilter filter)
        {
            var data = new List<District>();
            var response = new Response<List<District>>();

            try
            {
                if (string.IsNullOrEmpty(filter.CityID))
                {
                    response = new Response<List<District>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "CityID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ProvID)
                    };
                    return BadRequest(response);
                }

                var dQ = new DistrictsQuery("dQ");

                dQ.Select(dQ.DisID, dQ.DisName, dQ.CityID)
                    .Where(dQ.CityID == filter.CityID)
                    .OrderBy(dQ.DisName.Ascending);
                var dt = dQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<List<District>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var dis = new District
                    {
                        DisId = (int)dr["DisID"],
                        DisName = dr["DisName"] != DBNull.Value ? (string)dr["DisName"] : string.Empty,
                        CityId = dr["CityID"] != DBNull.Value ? (int)dr["CityID"] : 0
                    };
                    data.Add(dis);
                }

                response = new Response<List<District>>
                {
                    Data = data,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<List<District>>
                {
                    Data = data,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetAllSubDistrict", Name = "GetAllSubDistrict")]
        public ActionResult<Response<List<Subdistrict>>> GetAllSubDistrict([FromQuery] LocationFilter filter)
        {
            var data = new List<Subdistrict>();
            var response = new Response<List<Subdistrict>>();

            try
            {
                if (string.IsNullOrEmpty(filter.DistrictID))
                {
                    response = new Response<List<Subdistrict>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Subdistrict ID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ProvID)
                    };
                    return BadRequest(response);
                }

                var sQ = new SubdistrictsQuery("sQ");

                sQ.Select(sQ.SubdisID, sQ.SubdisName, sQ.DisID)
                    .Where(sQ.DisID == filter.DistrictID)
                    .OrderBy(sQ.SubdisName.Ascending);
                var dt = sQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<List<Subdistrict>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var sub = new Subdistrict
                    {
                        SubdisId = (int)dr["SubdisID"],
                        SubdisName = dr["SubdisName"] != DBNull.Value ? (string)dr["SubdisName"] : string.Empty,
                        DisId = dr["DisID"] != DBNull.Value ? (int)dr["DisID"] : 0
                    };
                    data.Add(sub);
                }

                response = new Response<List<Subdistrict>>
                {
                    Data = data,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<List<Subdistrict>>
                {
                    Data = data,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetPostalCode", Name = "GetPostalCode")]
        public ActionResult<Response<Subdistrict>> GetPostalCode([FromQuery] LocationFilter filter)
        {
            var data = new PostalCode();
            var response = new Response<PostalCode>();

            try
            {
                if (string.IsNullOrEmpty(filter.ProvID))
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Provinces ID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ProvID)
                    };
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(filter.CityID))
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "City ID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ProvID)
                    };
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(filter.DistrictID))
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "District ID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ProvID)
                    };
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(filter.SubDisID))
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Subdistrict ID"),
                        Succeeded = !string.IsNullOrEmpty(filter.ProvID)
                    };
                    return BadRequest(response);
                }

                var pc = new Postalcode();

                if (!pc.LoadByPrimaryKey(filter.ProvID, filter.CityID, filter.DistrictID, filter.SubDisID))
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = pc.PostalCode != 0 && pc.PostalCode != null ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = pc.PostalCode != 0 && pc.PostalCode != null
                    };
                    return NotFound(response);
                }

                data = new PostalCode
                {
                    PostalId = pc.PostalID ?? 0,
                    SubdisId = pc.SubdisID,
                    DisId = pc.DisID,
                    CityId = pc.CityID,
                    ProvId = pc.ProvID,
                    PostalCode1 = pc.PostalCode
                };

                response = new Response<PostalCode>
                {
                    Data = data,
                    Message = pc.PostalCode != 0 && pc.PostalCode != null ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = pc.PostalCode != 0 && pc.PostalCode != null
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<PostalCode>
                {
                    Data = data,
                    Message = $"{(data.PostalCode1 != 0 && data.PostalCode1 != null ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.PostalCode1 != 0 && data.PostalCode1 != null
                };
                return BadRequest(response);
            }
        }
    }
}