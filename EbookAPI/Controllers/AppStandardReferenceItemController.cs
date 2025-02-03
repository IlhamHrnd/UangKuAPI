using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Entity.Generated;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

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

        [HttpGet("GetAllReferenceItemID", Name = "GetAllReferenceItemID")]
        public ActionResult<PageResponse<AppStandardReferenceItem>> GetAllReferenceItemID([FromQuery] AppStandardRerefenceItemFilter filter)
        {
            var data = new List<AppStandardReferenceItem>();
            var response = new PageResponse<List<AppStandardReferenceItem>>(data, 0, 0);

            try
            {
                if (string.IsNullOrEmpty(filter.StandardReferenceID))
                {
                    response = new PageResponse<List<AppStandardReferenceItem>>(data, 0, 0)
                    {
                        TotalPages = data.Count,
                        TotalRecords = data.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = string.Format(AppConstant.RequiredMsg, "Standard ReferenceID"),
                        Succeeded = data.Count > 0
                    };
                    return BadRequest(response);
                }

                var asriQ = new AppstandardreferenceitemQuery("asriQ");

                asriQ.Select(asriQ.StandardReferenceID, asriQ.ItemID, asriQ.Note, asriQ.LastUpdateDateTime, asriQ.LastUpdateByUserID,
                    asriQ.ItemIcon, asriQ.ItemName,
                    "<CASE WHEN asriQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>",
                    "<CASE WHEN asriQ.IsActive = 1 THEN 'true' ELSE 'false' END AS 'IsActive'>")
                    .Where(asriQ.StandardReferenceID == filter.StandardReferenceID)
                    .OrderBy(asriQ.ItemName.Ascending);

                if (filter.IsActive.HasValue)
                    asriQ.Where(asriQ.IsActive == filter.IsActive);

                if (filter.IsUsedBySystem.HasValue)
                    asriQ.Where(asriQ.IsUsedBySystem == filter.IsUsedBySystem);
                var dt = asriQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new PageResponse<List<AppStandardReferenceItem>>(data, 0, 0)
                    {
                        TotalPages = data.Count,
                        TotalRecords = data.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var asri = new AppStandardReferenceItem
                    {
                        StandardReferenceId = (string)dr["StandardReferenceID"],
                        ItemId = (string)dr["ItemID"],
                        ItemName = dr["ItemName"] != DBNull.Value ? (string)dr["ItemName"] : string.Empty,
                        Note = (string)dr["Note"],
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        IsActive = bool.Parse((string)dr["IsActive"]),
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"],
                        ItemIcon = dr["ItemIcon"] != DBNull.Value ? (byte[])dr["ItemIcon"] : null
                    };
                    data.Add(asri);
                }

                response = new PageResponse<List<AppStandardReferenceItem>>(data, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = data.Count,
                    TotalRecords = data.Count,
                    PrevPageLink = string.Empty,
                    NextPageLink = string.Empty,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new PageResponse<List<AppStandardReferenceItem>>(data, 0, 0)
                {
                    TotalPages = data.Count,
                    TotalRecords = data.Count,
                    PrevPageLink = string.Empty,
                    NextPageLink = string.Empty,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpPost("CreateAppStandardReferenceItem", Name = "CreateAppStandardReferenceItem")]
        public async Task<IActionResult> CreateAppStandardReferenceItem([FromBody] AppStandardReferenceItem asri)
        {
            try
            {
                if (asri == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "AppStandardReferenceItem"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                //Proces Pengecekan Ukuran Icon Jika Ada
                if (asri.ItemIcon != null && asri.ItemIcon.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", asri.ItemId, $"The Icon You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.AppStandardReferenceItems
                    .FirstOrDefaultAsync(a => a.StandardReferenceId == asri.StandardReferenceId && a.ItemId == asri.ItemId);
                
                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, asri.ItemId));
                
                var a = new AppStandardReferenceItem
                {
                    StandardReferenceId = asri.StandardReferenceId, ItemId = asri.ItemId, ItemName = asri.ItemName,
                    Note = asri.Note, IsUsedBySystem = asri.IsUsedBySystem, IsActive = asri.IsActive,
                    LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserId = asri.LastUpdateByUserId, ItemIcon = asri.ItemIcon
                };
                _context.AppStandardReferenceItems.Add(a);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Standard Reference", asri.StandardReferenceId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Standard Reference Item", asri.StandardReferenceId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateAppStandardReferenceItem", Name = "UpdateAppStandardReferenceItem")]
        public async Task<IActionResult> UpdateAppStandardReferenceItem([FromBody] AppStandardReferenceItem asri)
        {
            try
            {
                if (asri == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "AppStandardReferenceItem"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                //Proces Pengecekan Ukuran Icon Jika Ada
                if (asri.ItemIcon != null && asri.ItemIcon.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Update", asri.ItemId, $"The Icon You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.AppStandardReferenceItems
                    .FirstOrDefaultAsync(a => a.StandardReferenceId == asri.StandardReferenceId && a.ItemId == asri.ItemId);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.ItemName = asri.ItemName;
                data.Note = asri.Note;
                data.IsUsedBySystem = asri.IsUsedBySystem;
                data.IsActive = asri.IsActive;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUserId = asri.LastUpdateByUserId;
                data.ItemIcon = asri.ItemIcon;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, asri.StandardReferenceId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Standard ReferenceID", asri.StandardReferenceId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetNewItemID", Name = "GetNewItemID")]
        public ActionResult<Response<string>> GetNewItemID([FromQuery] AppStandardRerefenceItemFilter filter)
        {
            var data = string.Empty;
            var response = new Response<string>();

            try
            {
                if (string.IsNullOrEmpty(filter.StandardReferenceID))
                {
                    response = new Response<string>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Standard ReferenceID"),
                        Succeeded = !string.IsNullOrEmpty(data)
                    };
                    return BadRequest(response);
                }

                int number = 1;
                string formattedNumber;

                do
                {
                    formattedNumber = Converter.NumberingFormat(number, "D3");
                    data = $"{filter.StandardReferenceID}-{formattedNumber}";
                    number++;
                } while (_context.AppStandardReferenceItems.Any(asri => asri.ItemId == data));

                response = new Response<string>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(data)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<string>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data)
                };
                return BadRequest(response);
            }
        }
    }
}
