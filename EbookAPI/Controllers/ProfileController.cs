using UangKuAPI.Context;
using EbookAPI.Encryptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;
using static UangKuAPI.BusinessObjects.Helper.Converter;
using Models = UangKuAPI.BusinessObjects.Model;
using System.Data;
using UangKuAPI.BusinessObjects.Entity;
using Microsoft.Extensions.Options;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "ProfileAPI")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Parameter _param;

        public ProfileController(AppDbContext context, IOptions<Parameter> options)
        {
            _context = context;
            _param = options.Value;
        }

        [HttpGet("GetPersonID", Name = "GetPersonID")]
        public ActionResult<List<Models.Profile>> GetPersonID([FromQuery] ProfileFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"PersonID Is Required");
                }

                var pQ = new ProfileQuery("pQ");

                pQ.Select(pQ.PersonID, pQ.FirstName, pQ.MiddleName, pQ.LastName, pQ.PlaceOfBirth, pQ.Photo,
                    pQ.Address, pQ.Province, pQ.City, pQ.Subdistrict, pQ.District, pQ.PostalCode, pQ.LastUpdateDateTime,
                    pQ.LastUpdateByUser, pQ.BirthDate)
                    .Where(pQ.PersonID == filter.PersonID);
                DataTable dt = pQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return BadRequest($"Data Not Found");
                }

                var response = new List<Models.Profile>();

                foreach (DataRow dr in dt.Rows)
                {
                    var profile = new Models.Profile
                    {
                        PersonID = (string)dr["PersonID"],
                        FirstName = Encryptor.DataDecrypt((string)dr["FirstName"], _param.Key01, _param.Key02, _param.Key03),
                        MiddleName = Encryptor.DataDecrypt((string)dr["MiddleName"], _param.Key01, _param.Key02, _param.Key03),
                        LastName = Encryptor.DataDecrypt((string)dr["LastName"], _param.Key01, _param.Key02, _param.Key03),
                        PlaceOfBirth = Encryptor.DataDecrypt((string)dr["PlaceOfBirth"], _param.Key01, _param.Key02, _param.Key03),
                        Photo = (byte[])dr["Photo"],
                        Address = Encryptor.DataDecrypt((string)dr["Address"], _param.Key01, _param.Key02, _param.Key03),
                        Province = Encryptor.DataDecrypt((string)dr["Province"], _param.Key01, _param.Key02, _param.Key03),
                        City = Encryptor.DataDecrypt((string)dr["City"], _param.Key01, _param.Key02, _param.Key03),
                        District = Encryptor.DataDecrypt((string)dr["District"], _param.Key01, _param.Key02, _param.Key03),
                        Subdistrict = Encryptor.DataDecrypt((string)dr["Subdistrict"], _param.Key01, _param.Key02, _param.Key03),
                        PostalCode = (int)dr["PostalCode"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUser = (string)dr["LastUpdateByUser"],
                        BirthDate = (DateTime)dr["BirthDate"]
                    };
                    response.Add(profile);
                }

                return Ok(response);
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

                var prof = new Models.Profile
                {
                    PersonID = profile.PersonID, FirstName = Encryptor.DataEncrypt(profile.FirstName, _param.Key01, _param.Key02, _param.Key03), MiddleName = Encryptor.DataEncrypt(profile.MiddleName, _param.Key01, _param.Key02, _param.Key03),
                    LastName = Encryptor.DataEncrypt(profile.LastName, _param.Key01, _param.Key02, _param.Key03), BirthDate = profile.BirthDate, PlaceOfBirth = Encryptor.DataEncrypt(profile.PlaceOfBirth, _param.Key01, _param.Key02, _param.Key03),
                    Photo = StringToByte(profile.Photo), Address = Encryptor.DataEncrypt(profile.Address, _param.Key01, _param.Key02, _param.Key03), Province = Encryptor.DataEncrypt(profile.Province, _param.Key01, _param.Key02, _param.Key03),
                    City = Encryptor.DataEncrypt(profile.City, _param.Key01, _param.Key02, _param.Key03), Subdistrict = Encryptor.DataEncrypt(profile.Subdistrict, _param.Key01, _param.Key02, _param.Key03), District = Encryptor.DataEncrypt(profile.District, _param.Key01, _param.Key02, _param.Key03),
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

                person.FirstName = Encryptor.DataEncrypt(profile.FirstName, _param.Key01, _param.Key02, _param.Key03);
                person.MiddleName = Encryptor.DataEncrypt(profile.MiddleName, _param.Key01, _param.Key02, _param.Key03);
                person.LastName = Encryptor.DataEncrypt(profile.LastName, _param.Key01, _param.Key02, _param.Key03);
                person.BirthDate = profile.BirthDate;
                person.PlaceOfBirth = Encryptor.DataEncrypt(profile.PlaceOfBirth, _param.Key01, _param.Key02, _param.Key03);
                person.Photo = StringToByte(profile.Photo);
                person.Address = Encryptor.DataEncrypt(profile.Address, _param.Key01, _param.Key02, _param.Key03);
                person.Province = Encryptor.DataEncrypt(profile.Province, _param.Key01, _param.Key02, _param.Key03);
                person.City = Encryptor.DataEncrypt(profile.City, _param.Key01, _param.Key02, _param.Key03);
                person.District = Encryptor.DataEncrypt(profile.District, _param.Key01, _param.Key02, _param.Key03);
                person.Subdistrict = Encryptor.DataEncrypt(profile.Subdistrict, _param.Key01, _param.Key02, _param.Key03);
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
