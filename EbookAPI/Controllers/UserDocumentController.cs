using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Renci.SshNet.Messages;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserDocumentAPI")]
    [ApiController]
    public class UserDocumentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _file;
        public UserDocumentController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _file = env.ContentRootFileProvider;
        }

        [HttpGet("GetNewDocumentID", Name = "GetNewDocumentID")]
        public ActionResult<Response<string>> GetNewDocumentID([FromQuery] UserDocumentFilter filter)
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
                        Message = string.Format(AppConstant.RequiredMsg, "Trans Type"),
                        Succeeded = !string.IsNullOrEmpty(data)
                    };
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new Response<string>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
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
                    data = $"{filter.PersonID}/{filter.TransType}/{transDate}-{formattedNumber}";
                } while (_context.UserDocuments.Any(ud => ud.DocumentId == data));

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

        [HttpPost("PostUserDocument", Name = "PostUserDocument")]
        public async Task<IActionResult> PostUserDocument([FromBody] UserDocumentUpload document)
        {
            try
            {
                if (document == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Document"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran File Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);
                if (document.DocumentData != null && document.DocumentData.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", document.FileName, $"The Document You Uploaded Exceeds The Maximum Size Limit({size})"));

                //Proses Pengecekan Folder Sudah Ada Tau Belum
                var folderName = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("DocumentDirectory");
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);

                var folderUser = Path.Combine(folderName, document.PersonId);
                if (!Directory.Exists(folderUser))
                    Directory.CreateDirectory(folderUser);

                //Proses Pengecekan Blank.pdf Jika DocumentData Ternyata Null
                var blankName = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("BlankPDF");
                var blankPath = Path.Combine("File", blankName);
                var blankInfo = _file.GetFileInfo(blankPath);
                var blankExtention = Path.GetExtension(blankPath);
                var blankByte = Array.Empty<byte>();
                if (!blankInfo.Exists)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Retrieve", blankName, AppConstant.NotFoundMsg));
                else
                    blankByte = System.IO.File.ReadAllBytes(blankPath);

                var filePath = Path.Combine(folderName, document.PersonId, document.FileName);
                var fileInfo = _file.GetFileInfo(filePath);
                if (fileInfo.Exists)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, document.FileName));

                var data = await _context.UserDocuments
                    .FirstOrDefaultAsync(ud => ud.DocumentId == document.DocumentId);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, document.DocumentId));

                var ud = new UserDocument
                {
                    DocumentId = document.DocumentId, PersonId = document.PersonId, FileName = document.DocumentData != null && document.DocumentData.Length > 0 ? document.FileName : blankName, 
                    FileExtention = document.DocumentData != null && document.DocumentData.Length > 0 ? document.FileExtention : blankExtention, Note = document.Note, DocumentDate = document.DocumentDate, 
                    FilePath = folderUser, IsDeleted = document.IsDeleted, LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserId = document.LastUpdateByUserId, CreatedDateTime = DateFormat.DateTimeNow(), 
                    CreatedByUserId = document.CreatedByUserId
                };
                _context.UserDocuments.Add(ud);
                int rows = await _context.SaveChangesAsync();

                if (rows > 0)
                {
                    System.IO.File.WriteAllBytesAsync(filePath, document.DocumentData != null && document.DocumentData.Length > 0 ? document.DocumentData : blankByte);
                    return Ok(string.Format(AppConstant.CreatedSuccessMsg, "Document", document.FileName));
                }
                else
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Document", document.DocumentId));
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
