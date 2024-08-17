using UangKuAPI.Context;
using EbookAPI.Encryptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using static UangKuAPI.BusinessObjects.Helper.Converter;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "ProfileAPI")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetPersonID", Name = "GetPersonID")]
        public async Task<ActionResult<Profile>> GetPersonID([FromQuery] ProfileFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"PersonID Is Required");
                }

                var response = await _context.Profile
                    .Select(p => new Profile
                    {
                        PersonID = p.PersonID, FirstName = p.FirstName, MiddleName = p.MiddleName, LastName = p.LastName,
                        PlaceOfBirth = p.PlaceOfBirth, Photo = p.Photo, Address = p.Address, Province = p.Province,
                        City = p.City, Subdistrict = p.Subdistrict, District = p.District, PostalCode = p.PostalCode,
                        LastUpdateDateTime = p.LastUpdateDateTime, LastUpdateByUser = p.LastUpdateByUser, BirthDate = p.BirthDate
                    })
                    .Where(p => p.PersonID == filter.PersonID)
                    .ToListAsync();

                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("PersonID Not Found");
                }
                foreach (var item in response)
                {
                    if (!string.IsNullOrEmpty(item.PersonID))
                    {
                        item.FirstName = Encryptor.DataEncrypt(item.FirstName);
                        item.MiddleName = Encryptor.DataEncrypt(item.MiddleName);
                        item.LastName = Encryptor.DataEncrypt(item.LastName);
                        item.PlaceOfBirth = Encryptor.DataEncrypt(item.PlaceOfBirth);
                        item.Address = Encryptor.DataEncrypt(item.Address);
                        item.Province = Encryptor.DataEncrypt(item.Province);
                        item.City = Encryptor.DataEncrypt(item.City);
                        item.District = Encryptor.DataEncrypt(item.District);
                        item.Subdistrict = Encryptor.DataEncrypt(item.Subdistrict);
                    }
                }

                return response == null || response.Count == 0 || !response.Any()
                    ? (ActionResult<Profile>)NotFound("Province Not Found")
                    : (ActionResult<Profile>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("PostProfile", Name = "PostProfile")]
        public async Task<IActionResult> PostProfile([FromBody] Profile2 profile)
        {
            try
            {
                if (profile == null)
                {
                    return BadRequest($"Profile Are Required");
                }

                var person = await _context.Profile
                    .Where(p => p.PersonID == profile.PersonID)
                    .ToListAsync();

                if (person.Any())
                {
                    return BadRequest($"{profile.PersonID} Already Exist");
                }

                var prof = new Profile
                {
                    PersonID = profile.PersonID, FirstName = Encryptor.DataEncrypt(profile.FirstName), MiddleName = Encryptor.DataEncrypt(profile.MiddleName),
                    LastName = Encryptor.DataEncrypt(profile.LastName), BirthDate = profile.BirthDate, PlaceOfBirth = Encryptor.DataEncrypt(profile.PlaceOfBirth),
                    Photo = StringToByte(profile.Photo), Address = Encryptor.DataEncrypt(profile.Address), Province = Encryptor.DataEncrypt(profile.Province),
                    City = Encryptor.DataEncrypt(profile.City), Subdistrict = Encryptor.DataEncrypt(profile.Subdistrict), District = Encryptor.DataEncrypt(profile.District),
                    PostalCode = profile.PostalCode, LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUser = profile.LastUpdateByUser
                };
                _context.Add(prof);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"Person ID {profile.PersonID} Created Successfully")
                    : BadRequest($"Failed To Insert Data For Person ID {profile.PersonID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchProfile", Name = "PatchProfile")]
        public async Task<IActionResult> PatchProfile([FromBody] Profile2 profile)
        {
            try
            {
                if (profile == null)
                {
                    return BadRequest($"Profile Is Required");
                }

                var person = await _context.Profile
                    .FirstOrDefaultAsync(p => p.PersonID == profile.PersonID);

                if (person == null)
                {
                    return BadRequest($"{profile.PersonID} Not Found");
                }

                person.FirstName = Encryptor.DataEncrypt(profile.FirstName);
                person.MiddleName = Encryptor.DataEncrypt(profile.MiddleName);
                person.LastName = Encryptor.DataEncrypt(profile.LastName);
                person.BirthDate = profile.BirthDate;
                person.PlaceOfBirth = Encryptor.DataEncrypt(profile.PlaceOfBirth);
                person.Photo = StringToByte(profile.Photo);
                person.Address = Encryptor.DataEncrypt(profile.Address);
                person.Province = Encryptor.DataEncrypt(profile.Province);
                person.City = Encryptor.DataEncrypt(profile.City);
                person.District = Encryptor.DataEncrypt(profile.District);
                person.Subdistrict = Encryptor.DataEncrypt(profile.Subdistrict);
                person.PostalCode = profile.PostalCode;
                person.LastUpdateDateTime = DateFormat.DateTimeNow();
                person.LastUpdateByUser = profile.LastUpdateByUser;
                _context.Update(person);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{profile.PersonID} Update Successfully")
                    : NotFound($"{profile.PersonID} Not Found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
