using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserWishlistAPI")]
    [ApiController]
    public class UserWishlistController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Parameter _param;
        public UserWishlistController(AppDbContext context, IOptions<Parameter> param)
        {
            _context = context;
            _param = param.Value;
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

                var uwQ = new BusinessObjects.Entity.Generated.UserwishlistQuery("uwQ");
                var catQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("catQ");

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
                    uwQ.CreatedByUserID, catQ.ItemName.As("SRProductCategory"))
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = uwQ.LoadDataTable();

                foreach (DataRow dr in dt.Rows)
                {
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
                        ProductPicture = dr["ProductPicture"] != DBNull.Value ? (byte[]?)dr["ProductPicture"] : null,
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

                var uw = new BusinessObjects.Entity.Generated.Userwishlist();
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

                var CategoryName = !string.IsNullOrEmpty(uw.SRProductCategory) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("Wishlist", uw.SRProductCategory) : string.Empty;
                var wishlistDate = uw.WishlistDate.HasValue ? uw.WishlistDate.Value : DateFormat.DateTimeNow();

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
                    ProductPicture = uw.ProductPicture,
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
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                if (wishlist.ProductPicture != null && wishlist.ProductPicture.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", wishlist.WishlistId, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.UserWishlists
                    .FirstOrDefaultAsync(uw => uw.WishlistId == wishlist.WishlistId);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, wishlist.WishlistId));

                var uw = new UserWishlist
                {
                    WishlistId = wishlist.WishlistId, PersonId = wishlist.PersonId, SrproductCategory = wishlist.SrproductCategory,
                    ProductName = wishlist.ProductName, ProductQuantity = wishlist.ProductQuantity, ProductPrice = wishlist.ProductPrice,
                    ProductLink = wishlist.ProductLink, CreatedByUserId = wishlist.CreatedByUserId, CreatedDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserId = wishlist.LastUpdateByUserId, LastUpdateDateTime = DateFormat.DateTimeNow(), WishlistDate = wishlist.WishlistDate,
                    ProductPicture = wishlist.ProductPicture, IsComplete = wishlist.IsComplete
                };
                _context.Add(uw);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Wishlist", wishlist.WishlistId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Wishlist", wishlist.WishlistId));
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
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                if (wishlist.ProductPicture != null && wishlist.ProductPicture.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", wishlist.WishlistId, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                var data = await _context.UserWishlists
                    .FirstOrDefaultAsync(uw => uw.WishlistId == wishlist.WishlistId);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.SrproductCategory = wishlist.SrproductCategory;
                data.ProductName = wishlist.ProductName;
                data.ProductQuantity = wishlist.ProductQuantity;
                data.ProductPrice = wishlist.ProductPrice;
                data.ProductLink = wishlist.ProductLink;
                data.LastUpdateByUserId = wishlist.LastUpdateByUserId;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.WishlistDate = wishlist.WishlistDate;
                data.ProductPicture = wishlist.ProductPicture;
                data.IsComplete = wishlist.IsComplete;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, wishlist.WishlistId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Wishlist", wishlist.WishlistId));
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

                var uwQ = new BusinessObjects.Entity.Generated.UserwishlistQuery("uwQ");
                var catQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("catQ");

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