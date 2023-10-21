using EbookAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
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
        public async Task<ActionResult<Profile>> GetPersonID([FromQuery] ProfileFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"PersonID Is Required");
                }
                var query = $@"SELECT p.PersonID, p.FirstName, p.MiddleName, p.LastName, p.BirthDate,
                                p.PlaceOfBirth, p.Photo, p.Address, p.Province, p.City, p.Subdistrict,
                                p.District, p.PostalCode
                                FROM Profile AS p
                                WHERE p.PersonID = '{filter.PersonID}';";
                var response = await _context.Profile.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("PersonID Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("PostProfile", Name = "PostProfile")]
        public async Task<IActionResult> PostProfile([FromBody] Profile profile)
        {
            try
            {
                if (profile == null)
                {
                    return BadRequest($"Profile Are Required");
                }
                string date = $"{profile.BirthDate: yyyy-MM-dd HH:mm:ss}";

                var query = $@"INSERT INTO `Profile`(`PersonID`, `FirstName`, `MiddleName`, `LastName`, `BirthDate`, `PlaceOfBirth`, 
                                `Photo`, `Address`, `Province`, `City`, `Subdistrict`, `District`, `PostalCode`)
                                VALUES('{profile.PersonID}', '{profile.FirstName}', '{profile.MiddleName}', '{profile.LastName}', '{date}', 
                                '{profile.PlaceOfBirth}', '{profile.Photo}', '{profile.Address}', '{profile.Province}', 
                                '{profile.City}', '{profile.Subdistrict}', '{profile.District}', '{profile.PostalCode}');";
                await _context.Database.ExecuteSqlRawAsync(query);

                return Ok($"Person ID {profile.PersonID} Created Successfully");
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
                {
                    return BadRequest($"Profile Is Required");
                }
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
                                `PostalCode` = '{profile.PostalCode}'
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
