using EbookAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
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
        public async Task<ActionResult<List<AppStandardReferenceItem>>> GetStandardID([FromQuery] AppStandardReferenceItemFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.StandardReferenceID))
                {
                    return BadRequest("StandardReferenceID Is Required");
                }

                string isActiveCondition;
                string isUseCondition;

                switch (filter.isActive)
                {
                    case true:
                        isActiveCondition = "AND asri.IsActive = '1'";
                        break;

                    default:
                        isActiveCondition = string.Empty;
                        break;
                }

                switch (filter.isUse)
                {
                    case true:
                        isUseCondition = "AND asri.IsUsedBySystem = '1'";
                        break;

                    default:
                        isUseCondition = string.Empty;
                        break;
                }

                var query = $@"SELECT asri.StandardReferenceID, asri.ItemID, asri.ItemName,
                                asri.Note, asri.IsUsedBySystem, asri.IsActive,
                                asri.LastUpdateDateTime, asri.LastUpdateByUserID, asri.ItemIcon
                            FROM AppStandardReferenceItem AS asri
                            WHERE asri.StandardReferenceID = '{filter.StandardReferenceID}'
                                {isActiveCondition} {isUseCondition};";

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
        public async Task<IActionResult> CreateAppStandardReferenceItem([FromBody] PostAppStandardReferenceItem asri)
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

                if (rowsAffected > 0)
                {
                    return Ok($"Standard ReferenceID {asri.StandardReferenceID} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For Standard ReferenceID {asri.StandardReferenceID}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReferenceItem", Name = "UpdateAppStandardReferenceItem")]
        public async Task<IActionResult> UpdateAppStandardReferenceItem([FromQuery] PostAppStandardReferenceItem asri)
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

                if (response > 0)
                {
                    return Ok($"{asri.ItemID} Update Successfully");
                }
                else
                {
                    return NotFound($"{asri.ItemID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
