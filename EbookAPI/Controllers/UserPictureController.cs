using EbookAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
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
                                WHERE up.IsDeleted = '{isDeleted}'
                                AND up.PersonID = '{filter.PersonID}'
                                ORDER BY up.PictureID DESC
                                OFFSET {(pageNumber - 1) * pageSize} ROWS
                                FETCH NEXT {pageSize} ROWS ONLY;";
                var pagedData = await _context.Picture.FromSqlRaw(query).ToListAsync();

                if (pagedData == null || pagedData.Count == 0 || !pagedData.Any())
                {
                    return NotFound($"Person ID Not Found");
                }

                var totalRecord = await _context.Users.CountAsync();
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
            try
            {
                if (picture == null)
                {
                    return BadRequest($"Picture Are Required");
                }
                DateTime dateTime = DateTime.Now;
                string createddate = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                string updatedate = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                int deleted = picture.IsDeleted == true ? 1 : 0;

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

        [HttpGet("GetNewPictureID", Name = "GetNewPictureID")]
        public IActionResult GetNewPictureID([FromQuery] string TransType)
        {
            try
            {
                if (string.IsNullOrEmpty(TransType))
                {
                    return BadRequest($"Transaction Type Is Required");
                }
                string transDate = DateTime.Now.ToString("yyMMdd");
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
