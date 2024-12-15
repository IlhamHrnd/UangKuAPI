using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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

                //Proses Pengecekan File Document Ada Atau Tidak
                if (document.DocumentData == null)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", document.FileName, "The Document You Uploaded Is Empty"));

                //Proses Mengambil List Document Type Yang Boleh Di Upload
                var listDocType = new List<string>();
                var asriQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("asriQ");
                var asriColl = new BusinessObjects.Entity.Generated.AppstandardreferenceitemCollection();
                asriQ.Where(asriQ.StandardReferenceID == "DocumentType", asriQ.IsActive == true, asriQ.IsUsedBySystem == true)
                    .OrderBy(asriQ.ItemName.Ascending);
                asriColl.Load(asriQ);
                if (asriColl.Count == 0)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Retrieve", "Document Type", AppConstant.NotFoundMsg));
                else
                    listDocType = asriColl.Select(coll => coll.ItemName).ToList();

                bool isAllowDocument = Converter.IsAllowDocumentType(listDocType, document.FileExtention);
                if (!isAllowDocument)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", document.FileName, $"The Document Extention Is Not Allow. Document Type : {document.FileExtention}"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran File Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);
                if (document.DocumentData.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", document.FileName, $"The Document You Uploaded Exceeds The Maximum Size Limit({size})"));

                //Proses Pengecekan Folder Sudah Ada Tau Belum
                var folderName = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("DocumentDirectory");
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);

                var folderUser = Path.Combine(folderName, document.PersonId);
                if (!Directory.Exists(folderUser))
                    Directory.CreateDirectory(folderUser);

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
                    DocumentId = document.DocumentId, PersonId = document.PersonId, FileName = document.FileName, FileExtention = document.FileExtention, Note = document.Note, DocumentDate = document.DocumentDate, 
                    FilePath = folderUser, IsDeleted = document.IsDeleted, LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserId = document.LastUpdateByUserId, CreatedDateTime = DateFormat.DateTimeNow(), 
                    CreatedByUserId = document.CreatedByUserId
                };
                _context.UserDocuments.Add(ud);
                int rows = await _context.SaveChangesAsync();

                if (rows > 0)
                {
                    await System.IO.File.WriteAllBytesAsync(filePath, document.DocumentData);
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

        [HttpPatch("PatchUserDocument", Name = "PatchUserDocument")]
        public async Task<IActionResult> PatchUserDocument([FromBody] UserDocumentUpload document)
        {
            try
            {
                if (document == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Document"));

                //Proses Pengecekan File Document Ada Atau Tidak
                if (document.DocumentData == null)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", document.FileName, "The Document You Uploaded Is Empty"));

                //Proses Mengambil List Document Type Yang Boleh Di Upload
                var listDocType = new List<string>();
                var asriQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("asriQ");
                var asriColl = new BusinessObjects.Entity.Generated.AppstandardreferenceitemCollection();
                asriQ.Where(asriQ.StandardReferenceID == "DocumentType", asriQ.IsActive == true, asriQ.IsUsedBySystem == true)
                    .OrderBy(asriQ.ItemName.Ascending);
                asriColl.Load(asriQ);
                if (asriColl.Count == 0)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Retrieve", "Document Type", AppConstant.NotFoundMsg));
                else
                    listDocType = asriColl.Select(coll => coll.ItemName).ToList();

                bool isAllowDocument = Converter.IsAllowDocumentType(listDocType, document.FileExtention);
                if (!isAllowDocument)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", document.FileName, $"The Document Extention Is Not Allow. Document Type : {document.FileExtention}"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran File Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);
                if (document.DocumentData.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", document.FileName, $"The Document You Uploaded Exceeds The Maximum Size Limit({size})"));

                //Proses Pengecekan Folder Sudah Ada Tau Belum
                var folderName = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("DocumentDirectory");
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);

                var folderUser = Path.Combine(folderName, document.PersonId);
                if (!Directory.Exists(folderUser))
                    Directory.CreateDirectory(folderUser);

                var data = await _context.UserDocuments
                    .FirstOrDefaultAsync(ud => ud.DocumentId == document.DocumentId);
                
                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.FileName = document.FileName;
                data.FileExtention = document.FileExtention;
                data.Note = document.Note;
                data.DocumentDate = document.DocumentDate;
                data.FilePath = folderUser;
                data.IsDeleted = document.IsDeleted;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUserId = document.LastUpdateByUserId;

                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                if (rows > 0)
                {
                    var filePath = Path.Combine(folderName, document.PersonId, document.FileName);
                    await System.IO.File.WriteAllBytesAsync(filePath, document.DocumentData);
                    return Ok(string.Format(AppConstant.UpdateSuccessMsg, document.DocumentId));
                }
                else
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Document", document.DocumentId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetUserDocument", Name = "GetUserDocument")]
        public ActionResult<PageResponse<UserDocument>> GetUserDocument([FromQuery] UserDocumentFilter filter)
        {
            var pagedData = new List<UserDocument>();
            var response = new PageResponse<List<UserDocument>>(pagedData, 0, 0);

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new PageResponse<List<UserDocument>>(pagedData, 0, 0)
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

                var udQ = new BusinessObjects.Entity.Generated.UserDocumentQuery("udQ");
                var udColl = new BusinessObjects.Entity.Generated.UserDocumentCollection();

                udQ.Where(udQ.PersonID == filter.PersonID);

                if (filter.IsDeleted.HasValue)
                    udQ.Where(udQ.IsDeleted == filter.IsDeleted.Value);

                //Proses Mengambil List Document Type Yang Boleh Di Upload
                var asriQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("asriQ");
                var asriColl = new BusinessObjects.Entity.Generated.AppstandardreferenceitemCollection();
                asriQ.Where(asriQ.StandardReferenceID == "DocumentType", asriQ.IsActive == true, asriQ.IsUsedBySystem == true)
                    .OrderBy(asriQ.ItemName.Ascending);
                asriColl.Load(asriQ);
                var listDocType = asriColl.Count > 0 ? asriColl.Select(coll => coll.ItemName).ToList() : new List<string>();

                if (listDocType.Count > 0)
                    udQ.Where(udQ.FileExtention.In(listDocType));
                udColl.Load(udQ);

                if (udColl.Count == 0)
                {
                    response = new PageResponse<List<UserDocument>>(pagedData, 0, 0)
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

                bool isAdmin = BusinessObjects.Entity.Custom.User.IsUserAdmin(filter.PersonID);

                foreach (var item in udColl)
                {
                    var ud = new UserDocument
                    {
                        DocumentId = item.DocumentID, PersonId = item.PersonID, FileName = item.FileName, FileExtention = item.FileExtention, 
                        Note = item.Note, DocumentDate = item.DocumentDate ?? new DateTime(), FilePath = isAdmin ? item.FilePath : string.Empty, IsDeleted = Converter.SbyteToUlong(item.IsDeleted) ?? 0, 
                        LastUpdateDateTime = isAdmin ? (item.LastUpdateDateTime ?? new DateTime()) : new DateTime(), LastUpdateByUserId = isAdmin ? item.LastUpdateByUserID : string.Empty, 
                        CreatedDateTime = isAdmin ? (item.CreatedDateTime ?? new DateTime()) : new DateTime(), CreatedByUserId = isAdmin ? item.CreatedByUserID : string.Empty
                    };
                    pagedData.Add(ud);
                }
                var totalRecord = udColl.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetUserDocument", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetUserDocument", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<UserDocument>>(pagedData, filter.PageNumber, filter.PageSize)
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
                response = new PageResponse<List<UserDocument>>(pagedData, 0, 0)
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

        [HttpGet("GetUserDocumentID", Name = "GetUserDocumentID")]
        public ActionResult<Response<UserDocumentUpload>> GetUserDocumentID([FromQuery] UserDocumentFilter filter)
        {
            var data = new UserDocumentUpload();
            var response = new Response<UserDocumentUpload>();

            try
            {
                if (string.IsNullOrEmpty(filter.DocumentID))
                {
                    response = new Response<UserDocumentUpload>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "DocumentID"),
                        Succeeded = !string.IsNullOrEmpty(data.DocumentId)
                    };
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new Response<UserDocumentUpload>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = !string.IsNullOrEmpty(data.PersonId)
                    };
                    return BadRequest(response);
                }

                var ud = new BusinessObjects.Entity.Generated.UserDocument();

                if (!ud.LoadByPrimaryKey(filter.DocumentID, filter.PersonID))
                {
                    response = new Response<UserDocumentUpload>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(ud.DocumentID) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(ud.DocumentID)
                    };
                    return NotFound(response);
                }

                //Proses Mencari Folder Dan File Ada Atau Tidak
                if (string.IsNullOrEmpty(ud.FilePath))
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Retrieve", ud.DocumentID, $"The File Path Is Empty"));

                if (!Directory.Exists(ud.FilePath))
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Find", ud.DocumentID, $"Folder Not Found"));

                var filePath = Path.Combine(ud.FilePath, ud.FileName);
                var fileInfo = _file.GetFileInfo(filePath);
                if (!fileInfo.Exists)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Find", ud.DocumentID, $"File Not Found"));

                var documentData = System.IO.File.ReadAllBytes(filePath);
                bool isAdmin = BusinessObjects.Entity.Custom.User.IsUserAdmin(filter.PersonID);

                data = new UserDocumentUpload
                {
                    DocumentId = ud.DocumentID, PersonId = ud.PersonID, FileName = ud.FileName, FileExtention = ud.FileExtention, Note = ud.Note,
                    DocumentDate = ud.DocumentDate ?? new DateTime(), FilePath = isAdmin ? ud.FilePath : string.Empty, IsDeleted = Converter.SbyteToUlong(ud.IsDeleted) ?? 0,
                    LastUpdateDateTime = isAdmin ? (ud.LastUpdateDateTime ?? new DateTime()) : new DateTime(), LastUpdateByUserId = ud.LastUpdateByUserID,
                    CreatedDateTime = isAdmin ? (ud.LastUpdateDateTime ?? new DateTime()) : new DateTime(), CreatedByUserId = isAdmin ? ud.LastUpdateByUserID : string.Empty,
                    DocumentData = documentData
                };

                response = new Response<UserDocumentUpload>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(ud.DocumentID) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(ud.DocumentID)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<UserDocumentUpload>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.DocumentId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.DocumentId)
                };
                return BadRequest(response);
            }
        }

        [HttpDelete("DeleteUserDocument", Name = "DeleteUserDocument")]
        public async Task<IActionResult> DeleteUserDocument([FromBody] UserDocument document)
        {
            try
            {
                if (document == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Document"));

                var data = await _context.UserDocuments
                    .FirstOrDefaultAsync(ud => ud.DocumentId == document.DocumentId && ud.PersonId == document.PersonId);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);
                
                data.IsDeleted = 1;
                data.LastUpdateByUserId = document.LastUpdateByUserId;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, document.DocumentId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Delete", "Document", document.DocumentId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}