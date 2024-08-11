using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
using static UangKuAPI.BusinessObjects.Helper.Helper;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using static UangKuAPI.BusinessObjects.Helper.Converter;

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
                var validFilter = new UserWishlistFilter(filter.PageNumber, filter.PageSize, filter.WishlistID, filter.PersonID);

                if (string.IsNullOrEmpty(validFilter.PersonID))
                {
                    return BadRequest($"PersonID Is Required");
                }

                var query = $@"SELECT uw.WishlistID, uw.PersonID, uw.SRProductCategory, uw.ProductName,
                                    uw.ProductQuantity, uw.ProductPrice, uw.ProductLink, uw.LastUpdateDateTime,
                                    uw.WishlistDate, uw.ProductPicture, uw.IsComplete
                                FROM UserWishlist AS uw
                                WHERE uw.PersonID = '{validFilter.PersonID}'
                                ORDER BY uw.WishlistID
                                OFFSET {(validFilter.PageNumber - 1) * validFilter.PageSize} ROWS
                                FETCH NEXT {validFilter.PageSize} ROWS ONLY;";
                var pagedData = await _context.UserWishlist.FromSqlRaw(query).ToListAsync();

                if (pagedData == null || pagedData.Count == 0 || !pagedData.Any())
                {
                    return NotFound("Person ID Not Found");
                }

                var totalRecord = await _context.UserWishlist.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / validFilter.PageSize);

                string? prevPageLink = validFilter.PageNumber > 1
                    ? Url.Link("GetAllUserWishlist", new { PageNumber = validFilter.PageNumber - 1, validFilter.PageSize })
                    : null;

                string? nextPageLink = validFilter.PageNumber < totalPages
                    ? Url.Link("GetAllUserWishlist", new { PageNumber = validFilter.PageNumber + 1, validFilter.PageSize })
                    : null;

                var response = new PageResponse<List<UserWishlist>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
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
            var validFilter = new UserWishlistFilter(filter.PageNumber, filter.PageSize, filter.WishlistID, filter.PersonID);

            if (string.IsNullOrEmpty(validFilter.WishlistID))
            {
                return BadRequest($"PersonID Is Required");
            }

            var query = $@"SELECT uw.WishlistID, uw.PersonID, uw.SRProductCategory, uw.ProductName,
                                    uw.ProductQuantity, uw.ProductPrice, uw.ProductLink, uw.LastUpdateDateTime,
                                    uw.WishlistDate, uw.ProductPicture, uw.IsComplete
                                FROM UserWishlist AS uw
                                WHERE uw.WishlistID = '{validFilter.WishlistID}';";
            var response = await _context.UserWishlist.FromSqlRaw(query).ToListAsync();

            if (response == null || response.Count == 0 || !response.Any())
            {
                return NotFound("WishlistID Not Found");
            }

            return Ok(response);
        }

        [HttpPost("PostUserWishlist", Name = "PostUserWishlist")]
        public async Task<IActionResult> PostUserWishlist([FromBody] PostUserWishlist wishlist)
        {
            var param = new ParameterHelper(_context);

            try
            {
                if (wishlist == null)
                {
                    return BadRequest($"Wishlist Are Required");
                }

                var dateTimeNow = DateFormat.DateTimeNow();
                var dateTimeNowDate = DateFormat.DateTimeNowDate(dateTimeNow.Year, dateTimeNow.Month, 7);

                string createddate = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);
                string updatedate = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);
                string wishlistdate = DateFormat.DateTimeIsNull(wishlist.WishlistDate);
                int complete = wishlist.IsComplete == true ? 1 : 0;

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = IntToLong(sizeResult);

                //Cek Jika Base64String Gambar Kosong Atau Tidak
                //Jika Kosong Maka Tidak Akan Disave
                string? picture = string.IsNullOrEmpty(wishlist.ProductPicture) || wishlist.PictureSize == 0 ? string.Empty : wishlist.ProductPicture;

                if (wishlist.PictureSize > longResult)
                {
                    return BadRequest($"The Image You Uploaded Exceeds The Maximum Size Limit");
                }

                var query = $@"INSERT INTO `UserWishlist`(`WishlistID`, `PersonID`, `SRProductCategory`, `ProductName`, 
                                `ProductQuantity`, `ProductPrice`, `ProductLink`, `CreatedByUserID`, 
                                `CreatedDateTime`, `LastUpdateByUserID`, `LastUpdateDateTime`, `WishlistDate`, 
                                `ProductPicture`, `IsComplete`) 
                            VALUES ('{wishlist.WishlistID}', '{wishlist.PersonID}', '{wishlist.SRProductCategory}', '{wishlist.ProductName}',
                                '{wishlist.ProductQuantity}','{wishlist.ProductPrice}','{wishlist.ProductLink}','{wishlist.CreatedByUserID}',
                                '{createddate}', '{wishlist.LastUpdateByUserID}', '{updatedate}', '{wishlistdate}',
                                '{picture}', '{complete}')";
                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

                if (rowsAffected > 0)
                {
                    return Ok($"Picture ID {wishlist.WishlistID} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For WishlistID {wishlist.WishlistID}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchUserWishlist", Name = "PatchUserWishlist")]
        public async Task<IActionResult> PatchUserWishlist([FromBody] PatchUserWishlist wishlist)
        {
            var param = new ParameterHelper(_context);

            try
            {
                if (string.IsNullOrEmpty(wishlist.WishlistID))
                {
                    return BadRequest($"WishlistID Is Required");
                }

                var dateTimeNow = DateFormat.DateTimeNow();
                var dateTimeNowDate = DateFormat.DateTimeNowDate(dateTimeNow.Year, dateTimeNow.Month, 7);

                string updatedate = DateFormat.DateTimeNow(Longyearpattern, DateFormat.DateTimeNow());
                string wishlistdate = DateFormat.DateTimeIsNull(wishlist.WishlistDate);
                int complete = wishlist.IsComplete == true ? 1 : 0;

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = IntToLong(sizeResult);

                //Cek Jika Base64String Gambar Kosong Atau Tidak
                //Jika Kosong Maka Tidak Akan Disave
                string? picture = string.IsNullOrEmpty(wishlist.ProductPicture) || wishlist.PictureSize == 0 ? string.Empty : wishlist.ProductPicture;

                if (wishlist.PictureSize > longResult)
                {
                    return BadRequest($"The Image You Uploaded Exceeds The Maximum Size Limit");
                }

                var query = $@"UPDATE `UserWishlist` 
                                SET `SRProductCategory` = '{wishlist.SRProductCategory}', `ProductName` = '{wishlist.ProductName}',
                                    `ProductQuantity` = '{wishlist.ProductQuantity}', `ProductPrice` = '{wishlist.ProductPrice}',
                                    `ProductLink` = '{wishlist.ProductLink}', `LastUpdateByUserID` = '{wishlist.LastUpdateByUserID}',
                                    `LastUpdateDateTime` = '{updatedate}', `WishlistDate` = '{wishlistdate}',
                                    `ProductPicture` = '{picture}', `IsComplete` = '{complete}' 
                                WHERE `WishlistID` = '{wishlist.WishlistID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"{wishlist.WishlistID} Update Successfully");
                }
                else
                {
                    return NotFound($"{wishlist.WishlistID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetUserWishlistPerCategory", Name = "GetUserWishlistPerCategory")]
        public async Task<IActionResult> GetUserWishlistPerCategory([FromQuery] UserWishlistPerCategoryFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"Person ID Is Required");
                }

                int isComplete;
                switch (filter.IsComplete)
                {
                    case true:
                        isComplete = 1;
                        break;

                    case false:
                        isComplete = 0;
                        break;

                    default:
                        isComplete = 0;
                        break;
                }

                var query = $@"SELECT COUNT(uw.SRProductCategory) AS 'CountProductCategory', asri.ItemName, asri.ItemIcon
	                            FROM UserWishlist AS uw
	                            INNER JOIN AppStandardReferenceItem AS asri
		                            ON asri.StandardReferenceID = 'Wishlist'
		                            AND asri.ItemID = uw.SRProductCategory
	                            WHERE uw.PersonID = '{filter.PersonID}'
		                            AND uw.IsComplete = {isComplete}
	                            GROUP BY uw.SRProductCategory";
                var result = await _context.UserWishlistPerCategories.FromSqlRaw(query).ToListAsync();

                if (result == null || result.Count == 0 || !result.Any())
                {
                    return NotFound($"Wishlist For {filter.PersonID} Not Found");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
