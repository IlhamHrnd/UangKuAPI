using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Data;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Response;
using UangKuAPI.EntityFramework.Models;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserWishlistAPI")]
    [ApiController]
    public class UserWishlistController : ControllerBase
    {
        private readonly BaseFramework _context;
        private readonly Parameter _param;
        private readonly IFileProvider _file;
        public UserWishlistController(BaseFramework context, IOptions<Parameter> param, IWebHostEnvironment env)
        {
            _context = context;
            _param = param.Value;
            _file = env.ContentRootFileProvider;
        }

        [HttpGet("GetNewUserWishlistID", Name = "GetNewUserWishlistID")]
        public ActionResult<Response<string>> GetNewUserWishlistID([FromQuery] UserWishlistFilter filter)
        {
            var data = string.Empty;
            var response = new Response<string>();

            try
            {
                if (string.IsNullOrEmpty(filter.TransType))
                {
                    response = new Response<string>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Transaction Type"),
                        Succeeded = !string.IsNullOrEmpty(data)
                    };
                    return BadRequest(response);
                }

                int number = 1;
                string transDate = DateFormat.DateTimeNow(DateFormat.Shortyearpattern, DateFormat.DateTimeNow());
                string formattedNumber;

                do
                {
                    formattedNumber = Converter.NumberingFormat(number, "D3");
                    data = $"USR/{filter.TransType}/{transDate}-{formattedNumber}";
                    number++;
                } while (_context.UserWishlists.Any(uw => uw.WishlistId == data));

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

        [HttpGet("GetAllUserWishlist", Name = "GetAllUserWishlist")]
        public ActionResult<PageResponse<UserWishlist>> GetAllUserWishlist([FromQuery] UserWishlistFilter filter)
        {
            var pagedData = new List<UserWishlist>();
            var response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0);

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = pagedData.Count > 0
                    };
                    return BadRequest(response);
                }

                var uwQ = new G.UserwishlistQuery("uwQ");
                var catQ = new G.AppstandardreferenceitemQuery("catQ");

                uwQ.Select(uwQ.WishlistID)
                    .InnerJoin(catQ).On(catQ.StandardReferenceID == "Wishlist" && catQ.ItemID == uwQ.SRProductCategory)
                    .Where(uwQ.PersonID == filter.PersonID)
                    .OrderBy(uwQ.WishlistID.Ascending);

                if (filter.IsComplete.HasValue)
                    uwQ.Where(uwQ.IsComplete == filter.IsComplete.Value);
                DataTable dtRecord = uwQ.LoadDataTable();
                
                if (dtRecord.Rows.Count == 0)
                {
                    response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = pagedData.Count > 0
                    };
                    return NotFound(response);
                }

                uwQ.Select(uwQ.PersonID, uwQ.ProductName, uwQ.ProductQuantity, uwQ.ProductPrice, uwQ.ProductLink, uwQ.IsComplete,
                    uwQ.LastUpdateDateTime, uwQ.LastUpdateByUserID, uwQ.WishlistDate, uwQ.ProductPicture, uwQ.CreatedDateTime, 
                    uwQ.CreatedByUserID, catQ.ItemName.As("SRProductCategory"), uwQ.PhotoExtention)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = uwQ.LoadDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    var photoData = Array.Empty<byte>();
                    if (dr["ProductPicture"] is not byte[] photo || photo.Length == 0)
                    {
                        var folderName = EntitySpaces.Custom.AppParameter.GetAppParameterValue("WishlistDirectory");
                        var wishlistId = dr["WishlistID"] as string ?? string.Empty;
                        var filePath = Path.Combine(folderName, (string)dr["PersonID"], $"{wishlistId.Replace("/", "")}{dr["PhotoExtention"]}");
                        var fileInfo = _file.GetFileInfo(filePath);
                        if (fileInfo.Exists)
                            photoData = System.IO.File.ReadAllBytes(filePath);
                    }
                    else
                        photoData = (byte[])dr["Photo"];

                    DateOnly lastUpdateDate = DateOnly.FromDateTime((DateTime)dr["LastUpdateDateTime"]);

                    var uw = new UserWishlist
                    {
                        WishlistId = (string)dr["WishlistID"],
                        PersonId = (string)dr["PersonID"],
                        SrproductCategory = (string)dr["SRProductCategory"],
                        ProductName = dr["ProductName"] != DBNull.Value ? (string)dr["ProductName"] : string.Empty,
                        ProductQuantity = dr["ProductQuantity"] != DBNull.Value ? (int)dr["ProductQuantity"] : 0,
                        ProductPrice = dr["ProductPrice"] != DBNull.Value ? (decimal)dr["ProductPrice"] : 0,
                        ProductLink = dr["ProductLink"] != DBNull.Value ? (string)dr["ProductLink"] : string.Empty,
                        ProductPicture = photoData,
                        CreatedByUserId = (string)dr["CreatedByUserID"],
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        WishlistDate = dr["WishlistDate"] != DBNull.Value ? Converter.DateTimeToDateOnly((DateTime)dr["WishlistDate"]) : null,
                        IsComplete = (int)dr["IsComplete"]
                    };
                    pagedData.Add(uw);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllUserWishlist", new { filter.PersonID, PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllUserWishlist", new { filter.PersonID, PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<UserWishlist>>(pagedData, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = pagedData.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0)
                {
                    TotalPages = pagedData.Count,
                    TotalRecords = pagedData.Count,
                    PrevPageLink = string.Empty,
                    NextPageLink = string.Empty,
                    Message = $"{(pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = pagedData.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetUserWishlistID", Name = "GetUserWishlistID")]
        public ActionResult<Response<UserWishlist>> GetUserWishlistID([FromQuery] UserWishlistFilter filter)
        {
            var data = new UserWishlist();
            var response = new Response<UserWishlist>();

            try 
            {
                if (string.IsNullOrEmpty(filter.WishlistID))
                {
                    response = new Response<UserWishlist>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "WishlistID"),
                        Succeeded = !string.IsNullOrEmpty(filter.WishlistID)
                    };
                    return BadRequest(response);
                }

                var uw = new G.Userwishlist();
                if (!uw.LoadByPrimaryKey(filter.WishlistID))
                {
                    response = new Response<UserWishlist>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(data.WishlistId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(data.WishlistId)
                    };
                    return NotFound(response);
                }

                var photoData = Array.Empty<byte>();
                if (uw.ProductPicture == null || uw.ProductPicture.Length == 0)
                {
                    var folderName = EntitySpaces.Custom.AppParameter.GetAppParameterValue("WishlistDirectory");
                    var filePath = Path.Combine(folderName, uw.PersonID, $"{uw.WishlistID.Replace("/", "")}{uw.PhotoExtention}");
                    var fileInfo = _file.GetFileInfo(filePath);
                    if (fileInfo.Exists)
                        photoData = System.IO.File.ReadAllBytes(filePath);
                }
                else
                    photoData = uw.ProductPicture;

                var CategoryName = !string.IsNullOrEmpty(uw.SRProductCategory) ? EntitySpaces.Custom.AppStandardReferenceItem.GetItemName("Wishlist", uw.SRProductCategory) : string.Empty;
                var wishlistDate = uw.WishlistDate ?? DateFormat.DateTimeNow();

                data = new UserWishlist
                {
                    WishlistId = uw.WishlistID,
                    PersonId = uw.PersonID,
                    SrproductCategory = CategoryName,
                    ProductName = uw.ProductName,
                    ProductQuantity = uw.ProductQuantity,
                    ProductPrice = uw.ProductPrice,
                    ProductLink = uw.ProductLink,
                    CreatedByUserId = uw.CreatedByUserID,
                    CreatedDateTime = uw.CreatedDateTime ?? DateFormat.DateTimeNow(),
                    LastUpdateByUserId = uw.LastUpdateByUserID,
                    LastUpdateDateTime = uw.LastUpdateDateTime ?? DateFormat.DateTimeNow(),
                    WishlistDate = Converter.DateTimeToDateOnly(wishlistDate),
                    ProductPicture = photoData,
                    IsComplete = uw.IsComplete ?? 0
                };

                response = new Response<UserWishlist>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data.WishlistId) ? AppConstant.NotFoundMsg : AppConstant.FoundMsg,
                    Succeeded = !string.IsNullOrEmpty(data.WishlistId)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<UserWishlist>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.WishlistId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.WishlistId)
                };
                return BadRequest(response);
            }
        }

        [HttpPost("PostUserWishlist", Name = "PostUserWishlist")]
        public async Task<IActionResult> PostUserWishlist([FromBody] UserWishlist wishlist)
        {
            try
            {
                if (wishlist == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Wishlist"));

                if (string.IsNullOrEmpty(wishlist.WishlistId))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "WishlistID"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = EntitySpaces.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                if (wishlist.ProductPicture != null && wishlist.ProductPicture.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", wishlist.WishlistId, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.UserWishlists
                    .FirstOrDefaultAsync(uw => uw.WishlistId == wishlist.WishlistId);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, wishlist.WishlistId));

                string filePath = string.Empty;
                if (wishlist.ProductPicture != null && wishlist.ProductPicture.Length > 0)
                {
                    //Proses Pengecekan Folder Suda Ada Atau Belum
                    var folderName = EntitySpaces.Custom.AppParameter.GetAppParameterValue("WishlistDirectory");
                    if (!Directory.Exists(folderName))
                        Directory.CreateDirectory(folderName);

                    var folderUser = Path.Combine(folderName, wishlist.PersonId);
                    if (!Directory.Exists(folderUser))
                        Directory.CreateDirectory(folderUser);

                    filePath = Path.Combine(folderName, wishlist.PersonId, $"{wishlist.WishlistId.Replace("/", "")}{(!string.IsNullOrEmpty(wishlist.PhotoExtention) ? wishlist.PhotoExtention : ".png")}");
                    var fileInfo = _file.GetFileInfo(filePath);
                    if (fileInfo.Exists)
                        return BadRequest(string.Format(AppConstant.AlreadyExistMsg, wishlist.WishlistId));
                }

                var uw = new UserWishlist
                {
                    WishlistId = wishlist.WishlistId, PersonId = wishlist.PersonId, SrproductCategory = wishlist.SrproductCategory,
                    ProductName = wishlist.ProductName, ProductQuantity = wishlist.ProductQuantity, ProductPrice = wishlist.ProductPrice,
                    ProductLink = wishlist.ProductLink, CreatedByUserId = wishlist.CreatedByUserId, CreatedDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserId = wishlist.LastUpdateByUserId, LastUpdateDateTime = DateFormat.DateTimeNow(), WishlistDate = wishlist.WishlistDate,
                    ProductPicture = Array.Empty<byte>(), IsComplete = wishlist.IsComplete, PhotoExtention = !string.IsNullOrEmpty(wishlist.PhotoExtention) ? wishlist.PhotoExtention : ".png"
                };
                _context.Add(uw);
                int rows = await _context.SaveChangesAsync();

                if (rows > 0)
                {
                    if (!string.IsNullOrEmpty(filePath))
                        await System.IO.File.WriteAllBytesAsync(filePath, wishlist.ProductPicture ?? Array.Empty<byte>());
                    return Ok(string.Format(AppConstant.CreatedSuccessMsg, "Wishlist", wishlist.WishlistId));
                }
                else
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Wishlist", wishlist.WishlistId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchUserWishlist", Name = "PatchUserWishlist")]
        public async Task<IActionResult> PatchUserWishlist([FromBody] UserWishlist wishlist)
        {
            try
            {
                if (wishlist == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Wishlist"));

                if (string.IsNullOrEmpty(wishlist.WishlistId))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "WishlistID"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = EntitySpaces.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                if (wishlist.ProductPicture != null && wishlist.ProductPicture.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", wishlist.WishlistId, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.UserWishlists
                    .FirstOrDefaultAsync(uw => uw.WishlistId == wishlist.WishlistId);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                string filePath = string.Empty;
                if (wishlist.ProductPicture != null && wishlist.ProductPicture.Length > 0)
                {
                    //Proses Pengecekan Folder Suda Ada Atau Belum
                    var folderName = EntitySpaces.Custom.AppParameter.GetAppParameterValue("WishlistDirectory");
                    if (!Directory.Exists(folderName))
                        Directory.CreateDirectory(folderName);

                    var folderUser = Path.Combine(folderName, wishlist.PersonId);
                    if (!Directory.Exists(folderUser))
                        Directory.CreateDirectory(folderUser);

                    filePath = Path.Combine(folderName, wishlist.PersonId, $"{wishlist.WishlistId.Replace("/", "")}{(!string.IsNullOrEmpty(wishlist.PhotoExtention) ? wishlist.PhotoExtention : ".png")}");
                }

                data.SrproductCategory = wishlist.SrproductCategory;
                data.ProductName = wishlist.ProductName;
                data.ProductQuantity = wishlist.ProductQuantity;
                data.ProductPrice = wishlist.ProductPrice;
                data.ProductLink = wishlist.ProductLink;
                data.LastUpdateByUserId = wishlist.LastUpdateByUserId;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.WishlistDate = wishlist.WishlistDate;
                data.ProductPicture = Array.Empty<byte>();
                data.IsComplete = wishlist.IsComplete;
                data.PhotoExtention = !string.IsNullOrEmpty(wishlist.PhotoExtention) ? wishlist.PhotoExtention : ".png";
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                if (rows > 0)
                {
                    if (!string.IsNullOrEmpty(filePath))
                        await System.IO.File.WriteAllBytesAsync(filePath, wishlist.ProductPicture ?? Array.Empty<byte>());
                    return Ok(string.Format(AppConstant.UpdateSuccessMsg, wishlist.WishlistId));
                }
                else
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Wishlist", wishlist.WishlistId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetUserWishlistPerCategory", Name = "GetUserWishlistPerCategory")]
        public ActionResult<PageResponse<UserWishlist>> GetUserWishlistPerCategory([FromQuery] UserWishlistFilter filter)
        {
            var pagedData = new List<UserWishlist>();
            var response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0);

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = pagedData.Count > 0
                    };
                    return BadRequest(response);
                }

                var uwQ = new G.UserwishlistQuery("uwQ");
                var catQ = new G.AppstandardreferenceitemQuery("catQ");

                uwQ.Select(uwQ.SRProductCategory.Count().As("CountProductCategory"), catQ.ItemName, catQ.ItemIcon)
                    .InnerJoin(catQ).On(catQ.StandardReferenceID == "Wishlist" && catQ.ItemID == uwQ.SRProductCategory)
                    .Where(uwQ.PersonID == filter.PersonID)
                    .GroupBy(uwQ.SRProductCategory);

                if (filter.IsComplete.HasValue)
                    uwQ.Where(uwQ.IsComplete == filter.IsComplete.Value);

                var dt = uwQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0)
                    {
                        TotalPages = pagedData.Count,
                        TotalRecords = pagedData.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = pagedData.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var uw = new UserWishlist
                    {
                        ProductQuantity = (int?)(Int64)dr["CountProductCategory"],
                        ProductName = (string)dr["ItemName"],
                        ProductPicture = (byte[])dr["ItemIcon"],
                        LastUpdateByUserId = _param.User,
                        LastUpdateDateTime = DateFormat.DateTimeNow(),
                        CreatedByUserId = _param.User,
                        CreatedDateTime = DateFormat.DateTimeNow()
                    };
                    pagedData.Add(uw);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new PageResponse<List<UserWishlist>>(pagedData, 0, 0)
                {
                    TotalPages = pagedData.Count,
                    TotalRecords = pagedData.Count,
                    PrevPageLink = string.Empty,
                    NextPageLink = string.Empty,
                    Message = $"{(pagedData.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = pagedData.Count > 0
                };
                return BadRequest(response);
            }
        }
    }
}