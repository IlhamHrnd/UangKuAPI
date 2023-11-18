using EbookAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                                '{updatedate}','{picture.LastUpdateByUser}');";
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
