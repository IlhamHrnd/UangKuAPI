using EbookAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Model;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "AppStandardReferenceItemAPI")]
    [ApiController]
    public class AppStandardReferenceItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppStandardReferenceItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetStandardID", Name = "GetStandardID")]
        public async Task<ActionResult<List<AppStandardReferenceItem>>> GetStandardID([FromQuery] AppStandardReferenceItemFilter filter, bool isActive = false, bool isUse = false)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.StandardReferenceID))
                {
                    return BadRequest("StandardReferenceID Is Required");
                }
                string isActiveCondition = isActive ? "AND asri.IsActive = 1" : "";
                string isUseCondition = isUse ? "AND asri.IsUsedBySystem = 1" : "";

                var query = $@"SELECT asri.StandardReferenceID, asri.ItemID, asri.ItemName,
                        asri.Note, asri.IsUsedBySystem, asri.IsActive,
                        asri.LastUpdateDateTime, asri.LastUpdateByUserID
                        FROM AppStandardReferenceItem AS asri
                        WHERE asri.StandardReferenceID = '{filter.StandardReferenceID}' {isActiveCondition} {isUseCondition};";

                var response = await _context.AppStandardReferenceItems.FromSqlRaw(query).ToListAsync();

                if (response == null || response.Count == 0)
                {
                    return NotFound("AppStandardReferenceItem Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("CreateAppStandardReferenceItem", Name = "CreateAppStandardReferenceItem")]
        public async Task<IActionResult> CreateAppStandardReferenceItem([FromBody] AppStandardReferenceItem asri)
        {
            try
            {
                if (asri == null)
                {
                    return BadRequest("AppStandardReferenceItem Are Required");
                }

                DateTime dateTime = DateTime.Now;
                string date = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                bool isdefault = true;

                var query = $@"INSERT INTO `AppStandardReferenceItem`(`StandardReferenceID`, `ItemID`, `ItemName`, `Note`, `IsUsedBySystem`,
                                `IsActive`, `LastUpdateDateTime`, `LastUpdateByUserID`)
                                VALUES ('{asri.StandardReferenceID}','{asri.ItemID}','{asri.ItemName}','{asri.Note}','{isdefault}','{isdefault}',
                                '{date}','{asri.LastUpdateByUserID}')";

                await _context.Database.ExecuteSqlRawAsync(query);

                return Ok($"Standard Reference Item {asri.ItemName} Created Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReferenceItem", Name = "UpdateAppStandardReferenceItem")]
        public async Task<IActionResult> UpdateAppStandardReferenceItem([FromQuery] string referenceID, string itemID, string itemName, string note, bool isActive, bool isUse, string user)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                string date = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                int use = 0 ;
                int active = 0 ;

                if (string.IsNullOrEmpty(referenceID) || string.IsNullOrEmpty(itemID))
                {
                    return BadRequest("ReferenceID And ItemID Is Required");
                }
                
                use = isUse ? 1 : 0 ;
                active = isActive ? 1 : 0 ;
                
                var query = $@"UPDATE `AppStandardReferenceItem`
                                SET `ItemName` = '{itemName}',
                                `Note` = '{note}',
                                `IsUsedBySystem` = '{use}',
                                `IsActive` = '{active}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUserID` = '{user}'
                                WHERE `StandardReferenceID` = '{referenceID}' AND `ItemID` = '{itemID}'";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"{itemID} Update Successfully");
                }
                else
                {
                    return NotFound($"{itemID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
