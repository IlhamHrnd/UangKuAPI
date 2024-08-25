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
using static UangKuAPI.BusinessObjects.Helper.AppConstant;

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
        public ActionResult<PageResponse<UserPicture>> GetUserPicture([FromQuery] UserPictureFilter filter)
        {
            try
            {
                var upQ = new UserpictureQuery("upQ");
                upQ.Select(upQ.PictureID, upQ.Picture, upQ.PictureName, upQ.PictureFormat, upQ.PersonID,
                    upQ.CreatedByUserID, upQ.CreatedDateTime, upQ.LastUpdateDateTime, upQ.LastUpdateByUserID,
                    "<CASE WHEN upQ.IsDeleted = 1 THEN 'true' ELSE 'false' END AS 'IsDeleted'>")
                    .OrderBy(upQ.PersonID.Descending)
                    .Where(upQ.PersonID == filter.PersonID && upQ.IsDeleted == filter.IsDeleted);
                DataTable dtRecord = upQ.LoadDataTable();

                upQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = upQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var pagedData = new List<UserPicture>();

                foreach (DataRow dr in dt.Rows)
                {
                    var up = new UserPicture
                    {
                        PictureID = (string)dr["PictureID"],
                        Picture = (byte[]?)dr["Picture"],
                        PictureName = (string)dr["PictureName"],
                        PictureFormat = (string)dr["PictureFormat"],
                        PersonID = (string)dr["PersonID"],
                        IsDeleted = bool.Parse((string)dr["IsDeleted"]),
                        CreatedByUserID = (string)dr["CreatedByUserID"],
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserID = (string)dr["LastUpdateByUserID"]
                    };
                    pagedData.Add(up);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetUserPicture", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetUserPicture", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<UserPicture>>(pagedData, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = pagedData.Count > 0 ? FoundMsg : NotFoundMsg,
                    Succeeded = pagedData.Count > 0
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
        public async Task<IActionResult> PostUserPicture([FromBody] UserPicture2 picture)
        {
            var param = new ParameterHelper(_context);

            try
            {
                if (picture == null)
                {
                    return BadRequest($"Picture Are Required");
                }
                //Proses Mencari Data MaxPicture Yang Menyimpan Jumlah Maksimal Gambar Yang Bisa Di Upload User
                var maxPicture = await param.GetParameterValue(AppParameterValue.MaxPicture);
                int picResult = ParameterHelper.TryParseInt(maxPicture);

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = await param.GetParameterValue(AppParameterValue.MaxSize);
                int sizeResult = ParameterHelper.TryParseInt(maxSize);
                long longResult = IntToLong(sizeResult);

                //Menghitung Jumlah Gambar Yang Sudah Di Upload User
                int pictureCount = await _context.Picture
                    .CountAsync(up => up.PersonID == picture.PersonID && up.IsDeleted == false);

                //Mencari Nama Gambar Yang Sama Yang Sudah Di Upload User
                //Untuk Menghemat Space Server
                int nameCount = await _context.Picture
                    .CountAsync(up => up.PersonID == picture.PersonID && up.PictureName == picture.PictureName);

                var checkPictureID = await _context.Picture
                    .Where(up => up.PictureID == picture.PictureID)
                    .ToListAsync();

                if (checkPictureID.Any())
                {
                    return BadRequest($"{picture.PictureID} Already Exist");
                }

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

                var pic = new UserPicture
                {
                    PictureID = picture.PictureID, Picture = StringToByte(picture.Picture), PictureName = picture.PictureName,
                    PictureFormat = picture.PictureFormat, PersonID = picture.PersonID, IsDeleted = picture.IsDeleted,
                    CreatedByUserID = picture.CreatedByUserID, CreatedDateTime = DateFormat.DateTimeNow(), LastUpdateDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserID = picture.CreatedByUserID
                };
                _context.Picture.Add(pic);

                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Ok($"User {picture.PictureID} Created Successfully")
                    : BadRequest($"Failed To Insert Data For PictureID {picture.PictureID}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteUserPicture", Name = "DeleteUserPicture")]
        public async Task<IActionResult> DeleteUserPicture([FromQuery] UserPictureFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PictureID))
                {
                    return BadRequest($"PictureID Is Required");
                }

                var pic = await _context.Picture
                    .FirstOrDefaultAsync(p => p.PictureID == filter.PictureID && p.IsDeleted == false);

                if (pic == null)
                {
                    return BadRequest($"{pic.PictureID} Not Found");
                }
                pic.IsDeleted = filter.IsDeleted;
                _context.Update(pic);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{filter.PictureID} Delete Successfully")
                    : BadRequest($"Failed To Delete Data For PictureID {filter.PictureID}");
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
                string transDate = DateFormat.DateTimeNow(Shortyearpattern, DateTime.Now);
                int number = 1;
                string formattedNumber = NumberingFormat(number, "D3");
                string transNo = $"PIC/{TransType}/{transDate}-{formattedNumber}";

                while (_context.Picture.Any(p => p.PictureID == transNo))
                {
                    number++;
                    formattedNumber = NumberingFormat(number, "D3");
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
