using Microsoft.AspNetCore.Mvc;
using System.Data;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Response;
using UangKuAPI.EntityFramework.Models;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "LocationAPI")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly BaseFramework _context;

        public LocationController(BaseFramework context)
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
                var query = (from p in _context.Provinces
                             orderby p.ProvName ascending
                             select p).ToList();

                if (query.Count == 0)
                    return NotFound(response = new Response<List<Province>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    });

                return Ok(response = new Response<List<Province>>
                {
                    Data = query,
                    Message = query.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = query.Count > 0
                });
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
                if (filter.ProvID == 0)
                {
                    response = new Response<List<City>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ProvincesID"),
                        Succeeded = filter.ProvID != 0
                    };
                    return BadRequest(response);
                }

                var query = (from c in _context.Cities
                             where c.ProvId == filter.ProvID
                             orderby c.CityName ascending
                             select c).ToList();

                if (query.Count == 0)
                    return NotFound(response = new Response<List<City>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    });

                return Ok(response = new Response<List<City>>
                {
                    Data = query,
                    Message = query.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = query.Count > 0
                });
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
                if (filter.CityID == 0)
                {
                    response = new Response<List<District>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "CityID"),
                        Succeeded = filter.CityID != 0
                    };
                    return BadRequest(response);
                }

                var query = (from d in _context.Districts
                             where d.CityId == filter.CityID
                             orderby d.DisName ascending
                             select d).ToList();

                if (query.Count == 0)
                    return NotFound(response = new Response<List<District>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    });

                return Ok(response = new Response<List<District>>
                {
                    Data = query,
                    Message = query.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = query.Count > 0
                });
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
                if (filter.DistrictID == 0)
                {
                    response = new Response<List<Subdistrict>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Subdistrict ID"),
                        Succeeded = filter.DistrictID != 0
                    };
                    return BadRequest(response);
                }

                var query = (from sd in _context.Subdistricts
                             where sd.DisId == filter.DistrictID
                             orderby sd.SubdisName ascending
                             select sd).ToList();

                if (query.Count == 0)
                    return NotFound(response = new Response<List<Subdistrict>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    });

                return Ok(response = new Response<List<Subdistrict>>
                {
                    Data = query,
                    Message = query.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = query.Count > 0
                });
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
                if (filter.ProvID == 0)
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Provinces ID"),
                        Succeeded = filter.ProvID != 0
                    };
                    return BadRequest(response);
                }

                if (filter.CityID == 0)
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "City ID"),
                        Succeeded = filter.CityID != 0
                    };
                    return BadRequest(response);
                }

                if (filter.DistrictID == 0)
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "District ID"),
                        Succeeded = filter.DistrictID != 0
                    };
                    return BadRequest(response);
                }

                if (filter.SubDisID == 0)
                {
                    response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Subdistrict ID"),
                        Succeeded = filter.SubDisID != 0
                    };
                    return BadRequest(response);
                }

                var query = (from pc in _context.PostalCodes
                             where pc.ProvId == filter.ProvID && pc.CityId == filter.CityID && pc.SubdisId == filter.SubDisID
                             select pc).FirstOrDefault();

                if (query == null)
                    return NotFound(response = new Response<PostalCode>
                    {
                        Data = data,
                        Message = data?.PostalId != 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data?.PostalId != 0
                    });

                return Ok(response = new Response<PostalCode>
                {
                    Data = query,
                    Message = query?.PostalId != 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = query?.PostalId != 0
                });
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