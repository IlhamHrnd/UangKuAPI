using UangKuAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;

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
        public async Task<ActionResult<List<AppStandardReferenceItem>>> GetStandardID([FromQuery] AppStandardReferenceItemFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.StandardReferenceID))
                {
                    return BadRequest("StandardReferenceID Is Required");
                }

                var query = _context.AppStandardReferenceItems.AsQueryable();

                query = query.Select(asri => new AppStandardReferenceItem
                {
                    StandardReferenceID = asri.StandardReferenceID, ItemID = asri.ItemID, Note = asri.Note,
                    IsUsedBySystem = asri.IsUsedBySystem, IsActive = asri.IsActive, LastUpdateDateTime = asri.LastUpdateDateTime,
                    LastUpdateByUserID = asri.LastUpdateByUserID, ItemIcon = asri.ItemIcon
                })
                    .Where(asri => asri.StandardReferenceID == filter.StandardReferenceID);

                if (filter.isActive.HasValue)
                {
                    query = query.Where(asri => asri.IsActive == filter.isActive.Value);
                }

                if (filter.isUse.HasValue)
                {
                    query = query.Where(asri => asri.IsUsedBySystem == filter.isUse.Value);
                }

                var response = await query.ToListAsync();
                return response == null || response.Count == 0 
                    ? (ActionResult<List<AppStandardReferenceItem>>)NotFound("AppStandardReferenceItem Not Found") 
                    : (ActionResult<List<AppStandardReferenceItem>>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("CreateAppStandardReferenceItem", Name = "CreateAppStandardReferenceItem")]
        public async Task<IActionResult> CreateAppStandardReferenceItem([FromBody] AppStandardReferenceItem2 asri)
        {
            var param = new ParameterHelper(_context);

            try
            {
                string date = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, DateTime.Now);

                if (string.IsNullOrEmpty(asri.StandardReferenceID) || string.IsNullOrEmpty(asri.ItemID))
                {
                    return BadRequest("ReferenceID And ItemID Is Required");
                }

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = Converter.IntToLong(sizeResult);

                //Cek Jika Base64String Gambar Kosong Atau Tidak
                //Jika Kosong Maka Tidak Akan Disave
                string? icon = string.IsNullOrEmpty(asri.ItemIcon) || asri.IconSize == 0 ? string.Empty : asri.ItemIcon;

                if (asri.IconSize > longResult)
                {
                    return BadRequest($"The Icon You Uploaded Exceeds The Maximum Size Limit");
                }

                int use;
                switch (asri.IsUsedBySystem)
                {
                    case null:
                        use = 0;
                        break;

                    case true:
                        use = 1;
                        break;

                    default:
                        use = 0;
                        break;

                }

                int active;
                switch (asri.IsActive)
                {
                    case null:
                        active = 0;
                        break;

                    case true:
                        active = 1;
                        break;

                    default:
                        active = 0;
                        break;
                }

                var query = $@"INSERT INTO `AppStandardReferenceItem`(`StandardReferenceID`, `ItemID`, `ItemName`, `Note`, `IsUsedBySystem`,
                                `IsActive`, `LastUpdateDateTime`, `LastUpdateByUserID`, `ItemIcon`)
                                VALUES ('{asri.StandardReferenceID}','{asri.ItemID}','{asri.ItemName}','{asri.Note}','{use}','{active}',
                                '{date}','{asri.LastUpdateByUserID}', '{asri.ItemIcon}')";

                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);
                return rowsAffected > 0
                    ? Ok($"Standard ReferenceID {asri.StandardReferenceID} Created Successfully")
                    : BadRequest($"Failed To Insert Data For Standard ReferenceID {asri.StandardReferenceID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReferenceItem", Name = "UpdateAppStandardReferenceItem")]
        public async Task<IActionResult> UpdateAppStandardReferenceItem([FromQuery] AppStandardReferenceItem2 asri)
        {
            var param = new ParameterHelper(_context);

            try
            {
                string date = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, DateTime.Now);

                if (string.IsNullOrEmpty(asri.StandardReferenceID) || string.IsNullOrEmpty(asri.ItemName))
                {
                    return BadRequest("ReferenceID And ItemID Is Required");
                }

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = Converter.IntToLong(sizeResult);

                //Cek Jika Base64String Gambar Kosong Atau Tidak
                //Jika Kosong Maka Tidak Akan Disave
                string? icon = string.IsNullOrEmpty(asri.ItemIcon) || asri.IconSize == 0 ? string.Empty : asri.ItemIcon;

                if (asri.IconSize > longResult)
                {
                    return BadRequest($"The Icon You Uploaded Exceeds The Maximum Size Limit");
                }

                string updatedate = DateFormat.DateTimeNow(DateStringFormat.Longyearpattern, DateFormat.DateTimeNow());

                int use;
                switch (asri.IsUsedBySystem)
                {
                    case null:
                        use = 0;
                        break;

                    case true:
                        use = 1;
                        break;

                    default:
                        use = 0;
                        break;
                }

                int active;
                switch (asri.IsActive)
                {
                    case null:
                        active = 0;
                        break;

                    case true:
                        active = 1;
                        break;

                    default:
                        active = 0;
                        break;
                }

                var query = $@"UPDATE `AppStandardReferenceItem`
                                SET `ItemName` = '{asri.ItemName}',
                                `Note` = '{asri.Note}',
                                `IsUsedBySystem` = '{use}',
                                `IsActive` = '{active}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUserID` = '{asri.LastUpdateByUserID}',
                                `ItemIcon` = '{asri.ItemIcon}'
                                WHERE `StandardReferenceID` = '{asri.StandardReferenceID}' AND `ItemID` = '{asri.ItemID}'";

                var response = await _context.Database.ExecuteSqlRawAsync(query);
                return response > 0 
                    ? Ok($"{asri.ItemID} Update Successfully") 
                    : NotFound($"{asri.ItemID} Not Found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
