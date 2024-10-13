using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Entity.Generated;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserPictureAPI")]
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
            var pagedData = new List<UserPicture>();
            var response = new PageResponse<List<UserPicture>>(pagedData, 0, 0);

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new PageResponse<List<UserPicture>>(pagedData, 0, 0)
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

                var upQ = new BusinessObjects.Entity.Generated.UserpictureQuery("upQ");

                upQ.Select(upQ.PictureID, upQ.Picture, upQ.PictureName, upQ.PictureFormat, upQ.PersonID, upQ.IsDeleted,
                    upQ.CreatedByUserID, upQ.CreatedDateTime, upQ.LastUpdateByUserID, upQ.LastUpdateDateTime)
                    .Where(upQ.PersonID == filter.PersonID)
                    .OrderBy(upQ.PictureID.Descending);

                if (filter.IsDelete.HasValue)
                    upQ.Where(upQ.IsDeleted == filter.IsDelete);
                DataTable dtRecord = upQ.LoadDataTable();

                upQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = upQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new PageResponse<List<UserPicture>>(pagedData, 0, 0)
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
                    var up = new UserPicture
                    {
                        PictureId = (string)dr["PictureID"],
                        Picture = (byte[])dr["Picture"],
                        PictureName = dr["PictureName"] != DBNull.Value ? (string)dr["PictureName"] : string.Empty,
                        PictureFormat = dr["PictureFormat"] != DBNull.Value ? (string)dr["PictureFormat"] : string.Empty,
                        IsDeleted = (Int32)dr["IsDeleted"] == 1,
                        CreatedByUserId = (string)dr["CreatedByUserID"],
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"],
                        PersonId = (string)dr["PersonID"]
                    };
                    pagedData.Add(up);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetUserPicture", new { filter.PersonID, PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetUserPicture", new { filter.PersonID, PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<UserPicture>>(pagedData, filter.PageNumber, filter.PageSize)
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
                response = new PageResponse<List<UserPicture>>(pagedData, 0, 0)
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

        [HttpPost("PostUserPicture", Name = "PostUserPicture")]
        public async Task<IActionResult> PostUserPicture([FromBody] UserPicture picture)
        {
            try
            {
                if (picture == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Picture"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                if (picture.Picture != null && picture.Picture.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", picture.PictureId, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                //Proses Mencari Data MaxPicture Yang Menyimpan Jumlah Maksimal Gambar Yang Bisa Di Upload User
                var maxPicture = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxPicture");
                var limit = Converter.StringToInt(maxPicture, 0);
                int pictureCount = await _context.UserPictures
                    .CountAsync(up => up.PersonId == picture.PersonId && up.IsDeleted == false);

                if (pictureCount > limit)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", picture.PersonId, $"The PersonID For {picture.PersonId} Already Reach Maximum Limit ({limit})"));

                //Mencari Nama Gambar Yang Sama Yang Sudah Di Upload User
                int nameCount = await _context.UserPictures
                    .CountAsync(up => up.PersonId == picture.PersonId && up.PictureName == picture.PictureName);

                if (nameCount > 0)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", $"{picture.PersonId}-{picture.PictureName}", $"Duplicate Picture For {picture.PictureName} Already Exist"));

                var up = new UserPicture
                {
                    PictureId = picture.PictureId, Picture = picture.Picture, PictureName = picture.PictureName, PictureFormat = picture.PictureFormat, PersonId = picture.PersonId,
                    IsDeleted = picture.IsDeleted, CreatedByUserId = picture.CreatedByUserId, CreatedDateTime = DateFormat.DateTimeNow(), LastUpdateByUserId = picture.LastUpdateByUserId,
                    LastUpdateDateTime = DateFormat.DateTimeNow()
                };
                _context.UserPictures.Add(up);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Picture", picture.PictureId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Picture", picture.PictureId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteUserPicture", Name = "DeleteUserPicture")]
        public async Task<IActionResult> DeleteUserPicture([FromBody] UserPicture picture)
        {
            try
            {
                if (picture == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Picture"));

                var data = await _context.UserPictures
                    .FirstOrDefaultAsync(up => up.PictureId == picture.PictureId);
                
                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.IsDeleted = true;
                data.LastUpdateByUserId = picture.LastUpdateByUserId;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, picture.PictureId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Delete", "Picture", picture.PictureId));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetNewPictureID", Name = "GetNewPictureID")]
        public ActionResult<Response<string>> GetNewPictureID([FromQuery] TransTypeFilter filter)
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
                    data = $"PIC/{filter.TransType}/{transDate}-{formattedNumber}";
                    number++;
                } while (_context.UserPictures.Any(up => up.PictureId == data));

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