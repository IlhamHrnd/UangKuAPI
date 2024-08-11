using UangKuAPI.Context;
using EbookAPI.Encryptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;

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
                        item.FirstName = EncryptorNullChecker.DecryptIfNotNull(item.FirstName);
                        item.MiddleName = EncryptorNullChecker.DecryptIfNotNull(item.MiddleName);
                        item.LastName = EncryptorNullChecker.DecryptIfNotNull(item.LastName);
                        item.PlaceOfBirth = EncryptorNullChecker.DecryptIfNotNull(item.PlaceOfBirth);
                        item.Address = EncryptorNullChecker.DecryptIfNotNull(item.Address);
                        item.Province = EncryptorNullChecker.DecryptIfNotNull(item.Province);
                        item.City = EncryptorNullChecker.DecryptIfNotNull(item.City);
                        item.District = EncryptorNullChecker.DecryptIfNotNull(item.District);
                        item.Subdistrict = EncryptorNullChecker.DecryptIfNotNull(item.Subdistrict);
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

                profile.FirstName = EncryptorNullChecker.EncryptIfNotNull(profile.FirstName);
                profile.MiddleName = EncryptorNullChecker.EncryptIfNotNull(profile.MiddleName);
                profile.LastName = EncryptorNullChecker.EncryptIfNotNull(profile.LastName);
                profile.PlaceOfBirth = EncryptorNullChecker.EncryptIfNotNull(profile.PlaceOfBirth);
                profile.Address = EncryptorNullChecker.EncryptIfNotNull(profile.Address);
                profile.Province = EncryptorNullChecker.EncryptIfNotNull(profile.Province);
                profile.City = EncryptorNullChecker.EncryptIfNotNull(profile.City);
                profile.District = EncryptorNullChecker.EncryptIfNotNull(profile.District);
                profile.Subdistrict = EncryptorNullChecker.EncryptIfNotNull(profile.Subdistrict);
                string updatedate = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, DateTime.Now);
                string date = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, (DateTime)profile.BirthDate);

                var query = $@"INSERT INTO `Profile`(`PersonID`, `FirstName`, `MiddleName`, `LastName`, `BirthDate`, `PlaceOfBirth`, 
                                `Photo`, `Address`, `Province`, `City`, `Subdistrict`, `District`, `PostalCode`, `LastUpdateDateTime`, `LastUpdateByUser`)
                                VALUES('{profile.PersonID}', '{profile.FirstName}', '{profile.MiddleName}', '{profile.LastName}', '{date}', 
                                '{profile.PlaceOfBirth}', '{profile.Photo}', '{profile.Address}', '{profile.Province}', 
                                '{profile.City}', '{profile.Subdistrict}', '{profile.District}', '{profile.PostalCode}', '{updatedate}', '{profile.LastUpdateByUser}');";
                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

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
                    .Where(p => p.PersonID == profile.PersonID)
                    .ToListAsync();

                if (!person.Any())
                {
                    return BadRequest($"{profile.PersonID} Not Found");
                }

                profile.FirstName = EncryptorNullChecker.EncryptIfNotNull(profile.FirstName);
                profile.MiddleName = EncryptorNullChecker.EncryptIfNotNull(profile.MiddleName);
                profile.LastName = EncryptorNullChecker.EncryptIfNotNull(profile.LastName);
                profile.PlaceOfBirth = EncryptorNullChecker.EncryptIfNotNull(profile.PlaceOfBirth);
                profile.Address = EncryptorNullChecker.EncryptIfNotNull(profile.Address);
                profile.Province = EncryptorNullChecker.EncryptIfNotNull(profile.Province);
                profile.City = EncryptorNullChecker.EncryptIfNotNull(profile.City);
                profile.District = EncryptorNullChecker.EncryptIfNotNull(profile.District);
                profile.Subdistrict = EncryptorNullChecker.EncryptIfNotNull(profile.Subdistrict);
                string updatedate = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, DateFormat.DateTimeNow());
                string date = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, (DateTime)profile.BirthDate);

                var query = $@"UPDATE `Profile` SET `FirstName` = '{profile.FirstName}', `MiddleName` = '{profile.MiddleName}',
                                `LastName` = '{profile.LastName}', `BirthDate` = '{date}', `PlaceOfBirth` = '{profile.PlaceOfBirth}',
                                `Photo` = '{profile.Photo}', `Address` = '{profile.Address}', `Province` = '{profile.Province}',
                                `City` = '{profile.City}', `Subdistrict` = '{profile.Subdistrict}', `District` = '{profile.District}',
                                `PostalCode` = '{profile.PostalCode}', `LastUpdateDateTime` = '{updatedate}', `LastUpdateByUser` = '{profile.LastUpdateByUser}'
                                WHERE `PersonID` = '{profile.PersonID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                return response > 0 
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
