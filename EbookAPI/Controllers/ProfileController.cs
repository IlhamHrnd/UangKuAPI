using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Interface;
using UangKuAPI.BusinessObjects.Response;
using UangKuAPI.EntityFramework.Models;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "ProfileAPI")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly BaseFramework _context;
        private readonly Parameter _param;
        private readonly IAppParameter _appParameter;
        public ProfileController(BaseFramework context, IOptions<Parameter> param, IAppParameter appParameter)
        {
            _context = context;
            _param = param.Value;
            _appParameter = appParameter;
        }

        [HttpGet("GetPersonID", Name = "GetPersonID")]
        public ActionResult<Response<Profile>> GetPersonID([FromQuery] ProfileFilter filter)
        {
            var data = new Profile();
            var response = new Response<Profile>();

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new Response<Profile>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = !string.IsNullOrEmpty(filter.PersonID)
                    };
                    return BadRequest(response);
                }

                var query = (from p in _context.Profiles
                             where p.PersonId == filter.PersonID
                             select p).FirstOrDefault();

                if (string.IsNullOrEmpty(query?.PersonId))
                    return NotFound(response = new Response<Profile>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(data.PersonId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(data.PersonId)
                    });

                data = new Profile
                {
                    PersonId = query.PersonId,
                    FirstName = Encryptor.DataDecrypt(query.FirstName, _param.Key01 ?? string.Empty),
                    MiddleName = Encryptor.DataDecrypt(query.MiddleName, _param.Key01 ?? string.Empty),
                    LastName = Encryptor.DataDecrypt(query.LastName, _param.Key01 ?? string.Empty),
                    BirthDate = query.BirthDate,
                    PlaceOfBirth = Encryptor.DataDecrypt(query.PlaceOfBirth, _param.Key01 ?? string.Empty),
                    Photo = query.Photo,
                    Address = Encryptor.DataDecrypt(query.Address, _param.Key01 ?? string.Empty),
                    Province = Encryptor.DataDecrypt(query.Province, _param.Key01 ?? string.Empty),
                    City = Encryptor.DataDecrypt(query.City, _param.Key01 ?? string.Empty),
                    District = Encryptor.DataDecrypt(query.District, _param.Key01 ?? string.Empty),
                    Subdistrict = Encryptor.DataDecrypt(query.Subdistrict, _param.Key01 ?? string.Empty),
                    PostalCode = query.PostalCode,
                    LastUpdateDateTime = query.LastUpdateDateTime,
                    LastUpdateByUser = query.LastUpdateByUser
                };

                return Ok(response = new Response<Profile>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data.PersonId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(data.PersonId)
                });
            }
            catch (Exception e)
            {
                response = new Response<Profile>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.PersonId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.PersonId)
                };
                return BadRequest(response);
            }
        }

        [HttpPost("PostProfile", Name = "PostProfile")]
        public async Task<IActionResult> PostProfile([FromBody] Profile profile)
        {
            try
            {
                if (profile == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Profile"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var size = _appParameter.ParameterInteger("MaxFileSize");
                var result = Converter.IntToLong(size);

                if (profile.Photo != null && profile.Photo.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", profile.PersonId, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.Profiles
                    .FirstOrDefaultAsync(p => p.PersonId == profile.PersonId);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, profile.PersonId));

                var p = new Profile
                {
                    PersonId = profile.PersonId, FirstName = Encryptor.DataEncrypt(profile.FirstName, _param.Key01), MiddleName = Encryptor.DataEncrypt(profile.MiddleName, _param.Key01), LastName = Encryptor.DataEncrypt(profile.LastName, _param.Key01),
                    BirthDate = profile.BirthDate, PlaceOfBirth = Encryptor.DataEncrypt(profile.PlaceOfBirth, _param.Key01), Photo = profile.Photo, Address = Encryptor.DataEncrypt(profile.Address, _param.Key01), Province = Encryptor.DataEncrypt(profile.Province, _param.Key01),
                    City = Encryptor.DataEncrypt(profile.City, _param.Key01), District = Encryptor.DataEncrypt(profile.District, _param.Key01), Subdistrict = Encryptor.DataEncrypt(profile.Subdistrict, _param.Key01), PostalCode = profile.PostalCode,
                    LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUser = _param.User
                };
                _context.Profiles.Add(p);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Profile", profile.PersonId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Profile", profile.PersonId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchProfile", Name = "PatchProfile")]
        public async Task<IActionResult> PatchProfile([FromBody] Profile profile)
        {
            try
            {
                if (profile == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Profile"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var size = _appParameter.ParameterInteger("MaxFileSize");
                var result = Converter.IntToLong(size);

                if (profile.Photo != null && profile.Photo.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", profile.PersonId, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.Profiles
                    .FirstOrDefaultAsync(p => p.PersonId == profile.PersonId);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.FirstName = Encryptor.DataEncrypt(profile.FirstName, _param.Key01);
                data.MiddleName = Encryptor.DataEncrypt(profile.MiddleName, _param.Key01);
                data.LastName = Encryptor.DataEncrypt(profile.LastName, _param.Key01);
                data.BirthDate = profile.BirthDate;
                data.PlaceOfBirth = Encryptor.DataEncrypt(profile.PlaceOfBirth, _param.Key01);
                data.Photo = profile.Photo;
                data.Address = Encryptor.DataEncrypt(profile.Address, _param.Key01);
                data.Province = Encryptor.DataEncrypt(profile.Province, _param.Key01);
                data.City = Encryptor.DataEncrypt(profile.City, _param.Key01);
                data.District = Encryptor.DataEncrypt(profile.District, _param.Key01);
                data.Subdistrict = Encryptor.DataEncrypt(profile.Subdistrict, _param.Key01);
                data.PostalCode = profile.PostalCode;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUser = profile.LastUpdateByUser;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, profile.PersonId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Person", profile.PersonId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}