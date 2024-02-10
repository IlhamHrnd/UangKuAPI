using EbookAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
using UangKuAPI.Model;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserProfileAPI")]
    [ApiController]
    public class UserPictureController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserPictureController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserPicture", Name = "GetUserPicture")]
        public async Task<ActionResult<GetUserPicture>> GetUserPicture([FromQuery] PictureFilter filter)
        {
            try
            {
                var validFilter = new PictureFilter(filter.PageNumber, filter.PageSize, filter.PersonID, filter.IsDeleted);
                var pageNumber = validFilter.PageNumber;
                var pageSize = validFilter.PageSize;
                int isDeleted = filter.IsDeleted ? 1 : 0;
                var query = $@"SELECT up.PictureID, up.Picture, up.PictureName, up.PictureFormat, 
                                up.PersonID, up.IsDeleted, up.CreatedByUserID, up.CreatedDateTime, 
                                up.LastUpdateDateTime, up.LastUpdateByUserID
                                FROM UserPicture AS up
                                WHERE up.PersonID = '{filter.PersonID}'
                                AND up.IsDeleted = '{isDeleted}'
                                ORDER BY up.PictureID DESC
                                OFFSET {(pageNumber - 1) * pageSize} ROWS
                                FETCH NEXT {pageSize} ROWS ONLY;";
                var pagedData = await _context.Picture.FromSqlRaw(query).ToListAsync();
                var totalRecord = await _context.Picture
                    .Where(p => p.IsDeleted == filter.IsDeleted && p.PersonID == filter.PersonID)
                    .CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / validFilter.PageSize);

                string? prevPageLink = validFilter.PageNumber > 1
                    ? Url.Link("GetUserPicture", new { PageNumber = validFilter.PageNumber - 1, validFilter.PageSize })
                    : null;

                string? nextPageLink = validFilter.PageNumber < totalPages
                    ? Url.Link("GetUserPicture", new { PageNumber = validFilter.PageNumber + 1, validFilter.PageSize })
                    : null;

                var response = new PageResponse<List<UserPicture>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink
                };

                if (pagedData == null || pagedData.Count == 0 || !pagedData.Any())
                {
                    return NotFound($"PersonID {filter.PersonID} Not Found");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpPost("PostUserPicture", Name = "PostUserPicture")]
        public async Task<IActionResult> PostUserPicture([FromBody] PostUserPicture picture)
        {
            var param = new ParameterHelper(_context);

            try
            {
                if (picture == null)
                {
                    return BadRequest($"Picture Are Required");
                }
                string createddate = DateFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss, DateTime.Now);
                string updatedate = DateFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss, DateTime.Now);
                int deleted = picture.IsDeleted == true ? 1 : 0;

                //Proses Mencari Data MaxPicture Yang Menyimpan Jumlah Maksimal Gambar Yang Bisa Di Upload User
                var maxPicture = await param.GetParameterValue(AppParameterValue.MaxPicture);
                int picResult = ParameterHelper.TryParseInt(maxPicture);

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = ImageHelper.MaxSizeInt(sizeResult);

                //Menghitung Jumlah Gambar Yang Sudah Di Upload User
                int pictureCount = await _context.Picture
                    .CountAsync(up => up.PersonID == picture.PersonID && up.IsDeleted == false);

                //Mencari Nama Gambar Yang Sama Yang Sudah Di Upload User
                //Untuk Menghemat Space Server
                int nameCount = await _context.Picture
                    .CountAsync(up => up.PersonID == picture.PersonID && up.PictureName == picture.PictureName);

                if (pictureCount > picResult)
                {
                    return BadRequest($"The PersonID For {picture.PersonID} Maximum Limit Has Been Reached");
                }

                if (nameCount > 0)
                {
                    return BadRequest($"Duplicate Picture For {picture.PictureName} Already Exist");
                }

                if (picture.PictureSize > longResult)
                {
                    return BadRequest($"The Image You Uploaded {picture.PictureName} Exceeds The Maximum Size Limit");
                }

                var query = $@"INSERT INTO `UserPicture`(`PictureID`, `Picture`, `PictureName`, `PictureFormat`, 
                                `PersonID`, `IsDeleted`, `CreatedByUserID`, `CreatedDateTime`, 
                                `LastUpdateDateTime`, `LastUpdateByUserID`) 
                                VALUES ('{picture.PictureID}', '{picture.Picture}', '{picture.PictureName}', '{picture.PictureFormat}',
                                '{picture.PersonID}', '{deleted}', '{picture.CreatedByUserID}', '{createddate}',
                                '{updatedate}','{picture.LastUpdateByUserID}');";
                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

                if (rowsAffected > 0)
                {
                    return Ok($"Picture ID {picture.PictureID} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For Picture ID {picture.PictureID}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteUserPicture", Name = "DeleteUserPicture")]
        public async Task<IActionResult> DeleteUserPicture([FromQuery] string pictureID, [FromBody] DeletedPictureFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(pictureID) || filter == null)
                {
                    return BadRequest($"All Data Are Required");
                }
                string date = DateFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss, DateTime.Now);
                int deleted = filter.IsDeleted == true ? 1 : 0;

                var query = $@"UPDATE `UserPicture`
                                SET `IsDeleted` = '{deleted}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUserID` = '{filter.LastUpdateUserID}'
                                WHERE `PictureID` = '{pictureID}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"{pictureID} Delete Successfully");
                }
                else
                {
                    return NotFound($"{pictureID} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetNewPictureID", Name = "GetNewPictureID")]
        public IActionResult GetNewPictureID([FromQuery] string TransType)
        {
            try
            {
                if (string.IsNullOrEmpty(TransType))
                {
                    return BadRequest($"Transaction Type Is Required");
                }
                string transDate = DateFormat.DateTimeNow(DateStringFormat.Yymmdd, DateTime.Now);
                int number = 1;
                string formattedNumber = number.ToString("D3");
                string transNo = $"PIC/{TransType}/{transDate}-{formattedNumber}";

                while (_context.Picture.Any(p => p.PictureID == transNo))
                {
                    number++;
                    formattedNumber = number.ToString("D3");
                    transNo = $"PIC/{TransType}/{transDate}-{formattedNumber}";
                }

                number++;

                return Ok(transNo);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
