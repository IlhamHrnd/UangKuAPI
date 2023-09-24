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
    }
}
