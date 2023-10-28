using EbookAPI.Context;
using EbookAPI.Encryptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Model;

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
        public async Task<ActionResult<GetProfile>> GetPersonID([FromQuery] ProfileFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"PersonID Is Required");
                }
                var query = $@"SELECT p.PersonID, p.FirstName, p.MiddleName, p.LastName, p.BirthDate,
                                p.PlaceOfBirth, p.Photo, p.Address, p.Province, p.City, p.Subdistrict,
                                p.District, p.PostalCode, p.LastUpdateDateTime, p.LastUpdateByUser
                                FROM Profile AS p
                                WHERE p.PersonID = '{filter.PersonID}';";
                var response = await _context.Profile.FromSqlRaw(query).ToListAsync();
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

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("PostProfile", Name = "PostProfile")]
        public async Task<IActionResult> PostProfile([FromBody] PostProfile profile)
        {
            try
            {
                if (profile == null)
                {
                    return BadRequest($"Profile Are Required");
                }
                else
                {
                    profile.FirstName = EncryptorNullChecker.EncryptIfNotNull(profile.FirstName);
                    profile.MiddleName = EncryptorNullChecker.EncryptIfNotNull(profile.MiddleName);
                    profile.LastName = EncryptorNullChecker.EncryptIfNotNull(profile.LastName);
                    profile.PlaceOfBirth = EncryptorNullChecker.EncryptIfNotNull(profile.PlaceOfBirth);
                    profile.Address = EncryptorNullChecker.EncryptIfNotNull(profile.Address);
                    profile.Province = EncryptorNullChecker.EncryptIfNotNull(profile.Province);
                    profile.City = EncryptorNullChecker.EncryptIfNotNull(profile.City);
                    profile.District = EncryptorNullChecker.EncryptIfNotNull(profile.District);
                    profile.Subdistrict = EncryptorNullChecker.EncryptIfNotNull(profile.Subdistrict);
                }
                DateTime dateTime = DateTime.Now;
                string updatedate = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                string date = $"{profile.BirthDate: yyyy-MM-dd HH:mm:ss}";

                var query = $@"INSERT INTO `Profile`(`PersonID`, `FirstName`, `MiddleName`, `LastName`, `BirthDate`, `PlaceOfBirth`, 
                                `Photo`, `Address`, `Province`, `City`, `Subdistrict`, `District`, `PostalCode`, `LastUpdateDateTime`, `LastUpdateByUser`)
                                VALUES('{profile.PersonID}', '{profile.FirstName}', '{profile.MiddleName}', '{profile.LastName}', '{date}', 
                                '{profile.PlaceOfBirth}', '{profile.Photo}', '{profile.Address}', '{profile.Province}', 
                                '{profile.City}', '{profile.Subdistrict}', '{profile.District}', '{profile.PostalCode}', '{updatedate}', '{profile.LastUpdateByUser}');";
                await _context.Database.ExecuteSqlRawAsync(query);

                return Ok($"Person ID {profile.PersonID} Created Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchProfile", Name = "PatchProfile")]
        public async Task<IActionResult> PatchProfile([FromBody] PostProfile profile)
        {
            try
            {
                if (profile == null)
                {
                    return BadRequest($"Profile Is Required");
                }
                else
                {
                    profile.FirstName = EncryptorNullChecker.EncryptIfNotNull(profile.FirstName);
                    profile.MiddleName = EncryptorNullChecker.EncryptIfNotNull(profile.MiddleName);
                    profile.LastName = EncryptorNullChecker.EncryptIfNotNull(profile.LastName);
                    profile.PlaceOfBirth = EncryptorNullChecker.EncryptIfNotNull(profile.PlaceOfBirth);
                    profile.Address = EncryptorNullChecker.EncryptIfNotNull(profile.Address);
                    profile.Province = EncryptorNullChecker.EncryptIfNotNull(profile.Province);
                    profile.City = EncryptorNullChecker.EncryptIfNotNull(profile.City);
                    profile.District = EncryptorNullChecker.EncryptIfNotNull(profile.District);
                    profile.Subdistrict = EncryptorNullChecker.EncryptIfNotNull(profile.Subdistrict);
                }
                DateTime dateTime = DateTime.Now;
                string updatedate = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                string date = $"{profile.BirthDate: yyyy-MM-dd HH:mm:ss}";

                var query = $@"UPDATE `Profile`
                                SET `FirstName` = '{profile.FirstName}',
                                `MiddleName` = '{profile.MiddleName}',
                                `LastName` = '{profile.LastName}',
                                `BirthDate` = '{date}',
                                `PlaceOfBirth` = '{profile.PlaceOfBirth}',
                                `Photo` = '{profile.Photo}',
                                `Address` = '{profile.Address}',
                                `Province` = '{profile.Province}',
                                `City` = '{profile.City}',
                                `Subdistrict` = '{profile.Subdistrict}',
                                `District` = '{profile.District}',
                                `PostalCode` = '{profile.PostalCode}',
                                `LastUpdateDateTime` = '{updatedate}',
                                `LastUpdateByUser` = '{profile.LastUpdateByUser}'
                                WHERE `PersonID` = '{profile.PersonID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"{profile.PersonID} Update Successfully");
                }
                else
                {
                    return NotFound($"{profile.PersonID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
