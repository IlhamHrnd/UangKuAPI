using UangKuAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;
using static UangKuAPI.BusinessObjects.Helper.Helper;
using static UangKuAPI.BusinessObjects.Helper.Converter;

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
                if (string.IsNullOrEmpty(asri.StandardReferenceID) || string.IsNullOrEmpty(asri.ItemID))
                {
                    return BadRequest("ReferenceID And ItemID Is Required");
                }

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = IntToLong(sizeResult);

                //Cek Jika Base64String Gambar Kosong Atau Tidak
                //Jika Kosong Maka Tidak Akan Disave
                string? icon = string.IsNullOrEmpty(asri.ItemIcon) || asri.IconSize == 0 ? string.Empty : asri.ItemIcon;

                if (asri.IconSize > longResult)
                {
                    return BadRequest($"The Icon You Uploaded Exceeds The Maximum Size Limit");
                }

                //Cek ReferenceID Dan ItemID Sudah Ada
                var refer = await _context.AppStandardReferenceItems
                    .Where(sr => sr.StandardReferenceID == asri.StandardReferenceID && sr.ItemID == asri.ItemID)
                    .ToListAsync();

                if (refer.Any())
                {
                    return BadRequest($"{asri.StandardReferenceID}-{asri.ItemID} Already Exist");
                }

                var asr = new AppStandardReferenceItem
                {
                    StandardReferenceID = asri.StandardReferenceID, ItemID = asri.ItemID, ItemName = asri.ItemName, Note = asri.Note,
                    IsUsedBySystem = asri.IsUsedBySystem, IsActive = asri.IsActive, LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserID = asri.LastUpdateByUserID,
                    ItemIcon = StringToByte(asri.ItemIcon)
                };
                _context.Add(asr);

                int rowsAffected = await _context.SaveChangesAsync();
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
        public async Task<IActionResult> UpdateAppStandardReferenceItem([FromBody] AppStandardReferenceItem2 asri)
        {
            var param = new ParameterHelper(_context);

            try
            {
                if (string.IsNullOrEmpty(asri.StandardReferenceID) || string.IsNullOrEmpty(asri.ItemName))
                {
                    return BadRequest("ReferenceID And ItemID Is Required");
                }

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = IntToLong(sizeResult);

                //Cek Jika Base64String Gambar Kosong Atau Tidak
                //Jika Kosong Maka Tidak Akan Disave
                string? icon = string.IsNullOrEmpty(asri.ItemIcon) || asri.IconSize == 0 ? string.Empty : asri.ItemIcon;

                if (asri.IconSize > longResult)
                {
                    return BadRequest($"The Icon You Uploaded Exceeds The Maximum Size Limit");
                }

                //Cek ReferenceID Dan ItemID Sudah Ada
                var refer = await _context.AppStandardReferenceItems
                    .FirstOrDefaultAsync(sr => sr.StandardReferenceID == asri.StandardReferenceID && sr.ItemID == asri.ItemID);

                if (refer == null)
                {
                    return BadRequest($"{asri.StandardReferenceID}-{asri.ItemID} Not Found");
                }

                refer.ItemName = asri.ItemName;
                refer.Note = asri.Note;
                refer.IsUsedBySystem = asri.IsUsedBySystem;
                refer.IsActive = asri.IsActive;
                refer.LastUpdateDateTime = DateFormat.DateTimeNow();
                refer.LastUpdateByUserID = asri.LastUpdateByUserID;
                refer.ItemIcon = StringToByte(asri.ItemIcon);
                _context.Update(refer);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{asri.StandardReferenceID} Updated Successfully")
                    : BadRequest($"Failed To Update Data For ReferenceID {asri.StandardReferenceID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
