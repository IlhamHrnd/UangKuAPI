using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using static UangKuAPI.BusinessObjects.Helper.Helper;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using static UangKuAPI.BusinessObjects.Helper.Converter;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Entity;
using System.Data;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserWishlistAPI")]
    [ApiController]
    public class UserWishlistController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserWishlistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetNewUserWishlistID", Name = "GetNewUserWishlistID")]
        public IActionResult GetNewUserWishlistID([FromQuery] string TransType)
        {
            try
            {
                if (string.IsNullOrEmpty(TransType))
                {
                    return BadRequest($"Transaction Type Is Required");
                }
                string wishDate = DateFormat.DateTimeNow(Shortyearpattern, DateTime.Now);
                int number = 1;
                string formattedNumber = NumberingFormat(number, "D3");
                string wishlistID = $"USR/{TransType}/{wishDate}-{formattedNumber}";

                while (_context.UserWishlist.Any(w => w.WishlistID == wishlistID))
                {
                    number++;
                    formattedNumber = NumberingFormat(number, "D3");
                    wishlistID = $"USR/{TransType}/{wishDate}-{formattedNumber}";
                }

                number++;

                return Ok(wishlistID);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllUserWishlist", Name = "GetAllUserWishlist")]
        public async Task<ActionResult<UserWishlist>> GetAllUserWishlist([FromQuery] UserWishlistFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"PersonID Is Required");
                }

                var uwQ = new UserwishlistQuery("uwQ");
                var catQ = new AppstandardreferenceitemQuery("catQ");

                uwQ.Select(uwQ.WishlistID, uwQ.PersonID, uwQ.ProductName, uwQ.ProductQuantity,
                    uwQ.ProductPrice, uwQ.ProductLink, uwQ.LastUpdateDateTime, uwQ.WishlistDate, uwQ.ProductPicture,
                    catQ.ItemName.As("SRProductCategory"),
                    "<CASE WHEN uwQ.IsComplete = 1 THEN 'true' ELSE 'false' END AS 'IsComplete'>")
                    .InnerJoin(catQ).On(catQ.StandardReferenceID == "Wishlist" && catQ.ItemID == uwQ.SRProductCategory)
                    .Where(uwQ.PersonID == filter.PersonID)
                    .OrderBy(uwQ.WishlistID.Ascending);
                DataTable dtRecord = uwQ.LoadDataTable();

                uwQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = uwQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var pagedData = new List<UserWishlist>();

                foreach (DataRow dr in dt.Rows)
                {
                    var wish = new UserWishlist
                    {
                        WishlistID = (string)dr["WishlistID"],
                        PersonID = (string)dr["PersonID"],
                        SRProductCategory = (string)dr["SRProductCategory"],
                        ProductName = (string)dr["ProductName"],
                        ProductQuantity = (int)dr["ProductQuantity"],
                        ProductPrice = (decimal)dr["ProductPrice"],
                        ProductLink = (string)dr["ProductLink"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        WishlistDate = (DateTime)dr["WishlistDate"],
                        IsComplete = bool.Parse((string)dr["IsComplete"])
                    };
                    pagedData.Add(wish);
                }

                var totalRecord = await _context.UserWishlist.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllUserWishlist", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllUserWishlist", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<UserWishlist>>(pagedData, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetUserWishlistID", Name = "GetUserWishlistID")]
        public async Task<ActionResult<UserWishlist>> GetUserWishlistID([FromQuery] UserWishlistFilter filter)
        {
            if (string.IsNullOrEmpty(filter.WishlistID))
            {
                return BadRequest($"WishlistID Is Required");
            }

            var uwQ = new UserwishlistQuery("uwQ");
            var catQ = new AppstandardreferenceitemQuery("catQ");

            uwQ.Select(uwQ.WishlistID, uwQ.PersonID, uwQ.ProductName, uwQ.ProductQuantity,
                uwQ.ProductPrice, uwQ.ProductLink, uwQ.LastUpdateDateTime, uwQ.WishlistDate, uwQ.ProductPicture,
                catQ.ItemName.As("SRProductCategory"),
                "<CASE WHEN uwQ.IsComplete = 1 THEN 'true' ELSE 'false' END AS 'IsComplete'>")
                .InnerJoin(catQ).On(catQ.StandardReferenceID == "Wishlist" && catQ.ItemID == uwQ.SRProductCategory)
                .Where(uwQ.WishlistID == filter.WishlistID)
                .OrderBy(uwQ.WishlistID.Ascending);
            var dt = uwQ.LoadDataTable();

            if (dt.Rows.Count == 0)
            {
                return NotFound($"{filter.WishlistID} Not Found");
            }

            var wishlist = new List<UserWishlist>();

            foreach (DataRow dr in dt.Rows)
            {
                var wish = new UserWishlist
                {
                    WishlistID = (string)dr["WishlistID"],
                    PersonID = (string)dr["PersonID"],
                    SRProductCategory = (string)dr["SRProductCategory"],
                    ProductName = (string)dr["ProductName"],
                    ProductQuantity = (int)dr["ProductQuantity"],
                    ProductPrice = (decimal)dr["ProductPrice"],
                    ProductLink = (string)dr["ProductLink"],
                    LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                    WishlistDate = (DateTime)dr["WishlistDate"],
                    IsComplete = bool.Parse((string)dr["IsComplete"])
                };
                wishlist.Add(wish);
            }

            return Ok(wishlist);
        }

        [HttpPost("PostUserWishlist", Name = "PostUserWishlist")]
        public async Task<IActionResult> PostUserWishlist([FromBody] UserWishlist2 wishlist)
        {
            var param = new ParameterHelper(_context);

            try
            {
                if (wishlist == null)
                {
                    return BadRequest($"Wishlist Are Required");
                }

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = IntToLong(sizeResult);

                if (wishlist.PictureSize > longResult)
                {
                    return BadRequest($"The Image You Uploaded Exceeds The Maximum Size Limit");
                }

                var checkWishlist = await _context.UserWishlist
                    .Where(uw => uw.WishlistID == wishlist.WishlistID)
                    .ToListAsync();

                if (checkWishlist.Any())
                {
                    return BadRequest($"{wishlist.WishlistID} Already Exist");
                }

                var newWishlist = new UserWishlist
                {
                    WishlistID = wishlist.WishlistID, PersonID = wishlist.PersonID, SRProductCategory = wishlist.SRProductCategory,
                    ProductName = wishlist.ProductName, ProductQuantity = wishlist.ProductQuantity, ProductPrice = wishlist.ProductPrice,
                    ProductLink = wishlist.ProductLink, CreatedByUserID = wishlist.CreatedByUserID, CreatedDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserID = wishlist.LastUpdateByUserID, LastUpdateDateTime = DateFormat.DateTimeNow(), WishlistDate = wishlist.WishlistDate,
                    ProductPicture = StringToByte(wishlist.ProductPicture), IsComplete = wishlist.IsComplete
                };
                _context.UserWishlist.Add(newWishlist);

                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Ok($"User {wishlist.WishlistID} Created Successfully")
                    : BadRequest($"Failed To Insert Data For Wishlist {wishlist.WishlistID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchUserWishlist", Name = "PatchUserWishlist")]
        public async Task<IActionResult> PatchUserWishlist([FromBody] UserWishlist2 wishlist)
        {
            var param = new ParameterHelper(_context);

            try
            {
                if (string.IsNullOrEmpty(wishlist.WishlistID))
                {
                    return BadRequest($"WishlistID Is Required");
                }

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = IntToLong(sizeResult);

                if (wishlist.PictureSize > longResult)
                {
                    return BadRequest($"The Image You Uploaded Exceeds The Maximum Size Limit");
                }

                var wish = await _context.UserWishlist
                    .FirstOrDefaultAsync(uw => uw.WishlistID == wishlist.WishlistID);

                if (wish == null)
                {
                    return BadRequest($"{wishlist.WishlistID} Not Found");
                }

                wish.SRProductCategory = wishlist.SRProductCategory;
                wish.ProductName = wishlist.ProductName;
                wish.ProductQuantity = wishlist.ProductQuantity;
                wish.ProductPrice = wishlist.ProductPrice;
                wish.ProductLink = wishlist.ProductLink;
                wish.LastUpdateByUserID = wishlist.LastUpdateByUserID;
                wish.LastUpdateDateTime = DateFormat.DateTimeNow();
                wish.WishlistDate = wishlist.WishlistDate;
                wish.ProductPicture = StringToByte(wishlist.ProductPicture);
                wish.IsComplete = wishlist.IsComplete;
                _context.Update(wish);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{wishlist.WishlistID} Updated Successfully")
                    : BadRequest($"Failed To Update Data For WishlistID {wishlist.WishlistID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetUserWishlistPerCategory", Name = "GetUserWishlistPerCategory")]
        public async Task<ActionResult<UserWishlistPerCategory>> GetUserWishlistPerCategory([FromQuery] UserWishlistFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"Person ID Is Required");
                }

                var uwQ = new UserwishlistQuery("uwQ");
                var catQ = new AppstandardreferenceitemQuery("catQ");

                uwQ.Select(uwQ.SRProductCategory.Count().As("CountProductCategory"), catQ.ItemName, catQ.ItemIcon)
                    .InnerJoin(catQ).On(catQ.StandardReferenceID == "Wishlist" && catQ.ItemID == uwQ.SRProductCategory)
                    .Where(uwQ.PersonID == filter.PersonID && uwQ.IsComplete == filter.IsComplete)
                    .GroupBy(uwQ.SRProductCategory);
                var dt = uwQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return BadRequest($"{filter.PersonID} Not Found");
                }

                var wishlist = new List<UserWishlistPerCategory>();

                foreach (DataRow dr in dt.Rows)
                {
                    var wish = new UserWishlistPerCategory
                    {
                        CountProductCategory = (int?)(Int64)dr["CountProductCategory"],
                        ItemName = (string)dr["ItemName"],
                        ItemIcon = (byte[])dr["ItemIcon"]
                    };
                    wishlist.Add(wish);
                }

                return Ok(wishlist);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
