using System.Data;
using iText.Kernel.Pdf;
using iText.Layout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "TransactionAPI")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Parameter _param;
        private readonly IFileProvider _file;
        public TransactionController(AppDbContext context, IOptions<Parameter> param, IWebHostEnvironment env)
        {
            _context = context;
            _param = param.Value;
            _file = env.ContentRootFileProvider;
        }

        [HttpPost("PostTransaction", Name = "PostTransaction")]
        public async Task<IActionResult> PostTransaction([FromBody] Transaction trans)
        {
            try
            {
                if (trans == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Transaction"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                if (trans.Photo != null && trans.Photo.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", trans.TransNo, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                if (string.IsNullOrEmpty(trans.PersonId))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "PersonID"));

                var data = await _context.Transactions
                    .FirstOrDefaultAsync(t => t.TransNo == trans.TransNo);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, trans.TransNo));

                var t = new Transaction
                {
                    TransNo = trans.TransNo, Srtransaction = trans.Srtransaction, SrtransItem = trans.SrtransItem, Amount = trans.Amount, Description = trans.Description, Photo = trans.Photo,
                    CreatedDateTime = DateFormat.DateTimeNow(), CreatedByUserId = trans.CreatedByUserId, LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUserId = trans.LastUpdateByUserId,
                    TransType = trans.TransType, TransDate = trans.TransDate, PersonId = trans.PersonId
                };
                _context.Transactions.Add(t);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "Transaction", trans.TransNo))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "Transaction", trans.TransNo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchTransaction", Name = "PatchTransaction")]
        public async Task<IActionResult> PatchTransaction([FromBody] Transaction trans)
        {
            try
            {
                if (trans == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Transaction"));

                //Proses Mencari Data MaxSize Yang Menyimpan Jumlah Maksimal Ukuran Gambar Yang Bisa Di Upload User
                var maxSize = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("MaxFileSize");
                var size = Converter.StringToInt(maxSize, 0);
                var result = Converter.IntToLong(size);

                if (trans.Photo != null && trans.Photo.Length > result)
                    return BadRequest(string.Format(AppConstant.FailedMsg, "Insert", trans.TransNo, $"The Image You Uploaded Exceeds The Maximum Size Limit({size})"));

                if (string.IsNullOrEmpty(trans.PersonId))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "PersonID"));

                var data = await _context.Transactions
                    .FirstOrDefaultAsync(t => t.TransNo == trans.TransNo);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.Srtransaction = trans.Srtransaction;
                data.SrtransItem = trans.SrtransItem;
                data.Amount = trans.Amount;
                data.Description = trans.Description;
                data.Photo = trans.Photo;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUserId = trans.LastUpdateByUserId;
                data.TransType = trans.TransType;
                data.TransDate = trans.TransDate;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, trans.TransNo))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Transaction", trans.TransNo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetNewTransactionNo", Name = "GetNewTransactionNo")]
        public ActionResult<Response<string>> GetNewTransactionNo([FromQuery] TransTypeFilter filter)
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
                    data = $"TRA/{filter.TransType}/{transDate}-{formattedNumber}";
                    number++;
                } while (_context.Transactions.Any(t => t.TransNo == data));


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

        [HttpGet("GetAllTransaction", Name = "GetAllTransaction")]
        public ActionResult<PageResponse<Transaction>> GetAllTransaction([FromQuery] TransactionFilter filter)
        {
            var pagedData = new List<Transaction>();
            var response = new PageResponse<List<Transaction>>(pagedData, 0, 0);
            
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new PageResponse<List<Transaction>>(pagedData, 0, 0)
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

                var tQ = new BusinessObjects.Entity.Generated.TransactionQuery("tQ");
                var transQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("transQ");
                var itemQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("itemQ");

                tQ.Select(tQ.TransNo, tQ.Amount, tQ.Description, tQ.Photo, tQ.TransType, tQ.PersonID,
                    tQ.TransDate, transQ.ItemName.As("SRTransaction"), itemQ.ItemName.As("SRTransItem"),
                    tQ.CreatedDateTime, tQ.CreatedByUserID, tQ.LastUpdateDateTime, tQ.LastUpdateByUserID)
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .InnerJoin(itemQ).On(itemQ.StandardReferenceID == "Expenditure" && itemQ.ItemID == tQ.SRTransItem)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate);

                var isAscending = filter.IsAscending ?? false;
                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    switch (filter.OrderBy)
                    {
                        case "OrderByTransaction-001":
                            tQ.OrderBy(isAscending ? tQ.TransNo.Ascending : tQ.TransNo.Descending);
                            break;

                        case "OrderByTransaction-002":
                            tQ.OrderBy(isAscending ? tQ.TransType.Ascending : tQ.TransType.Descending);
                            break;

                        case "OrderByTransaction-003":
                            tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                            break;

                        default:
                            tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                            break;
                    }
                }
                else
                {
                    tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                }
                DataTable dtRecord = tQ.LoadDataTable();

                tQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new PageResponse<List<Transaction>>(pagedData, 0, 0)
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
                    var t = new Transaction
                    {
                        TransNo = (string)dr["TransNo"],
                        PersonId = (string)dr["PersonID"],
                        Srtransaction = (string)dr["SRTransaction"],
                        SrtransItem = (string)dr["SRTransItem"],
                        Amount = dr["Amount"] != DBNull.Value ? (decimal)dr["Amount"] : 0,
                        Description = dr["Description"] != DBNull.Value ? (string)dr["Description"] : string.Empty,
                        Photo = dr["Photo"] != DBNull.Value ? (byte[])dr["Photo"] : null,
                        TransType = dr["TransType"] != DBNull.Value ? (string)dr["TransType"] : string.Empty,
                        TransDate = dr["TransDate"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)dr["TransDate"]) : null,
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        CreatedByUserId = (string)dr["CreatedByUserID"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"]
                    };
                    pagedData.Add(t);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllTransaction", new { filter.PersonID, PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllTransaction", new { filter.PersonID, PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<Transaction>>(pagedData, filter.PageNumber, filter.PageSize)
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
                response = new PageResponse<List<Transaction>>(pagedData, 0, 0)
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

        [HttpGet("GetAllPDFTransaction", Name = "GetAllPDFTransaction")]
        public ActionResult<Response<List<Transaction>>> GetAllPDFTransaction([FromQuery] TransactionFilter filter)
        {
            var data = new List<Transaction>();
            var response = new Response<List<Transaction>>();

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new PageResponse<List<Transaction>>(data, 0, 0)
                    {
                        TotalPages = data.Count,
                        TotalRecords = data.Count,
                        PrevPageLink = string.Empty,
                        NextPageLink = string.Empty,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = data.Count > 0
                    };
                    return BadRequest(response);
                }

                var tQ = new BusinessObjects.Entity.Generated.TransactionQuery("tQ");
                var transQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("transQ");
                var itemQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("itemQ");

                tQ.Select(tQ.TransNo, tQ.Amount, tQ.Description, tQ.Photo, tQ.TransType, tQ.PersonID,
                    tQ.TransDate, transQ.ItemName.As("SRTransaction"), itemQ.ItemName.As("SRTransItem"),
                    tQ.CreatedDateTime, tQ.CreatedByUserID, tQ.LastUpdateDateTime, tQ.LastUpdateByUserID)
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .InnerJoin(itemQ).On(itemQ.StandardReferenceID == "Expenditure" && itemQ.ItemID == tQ.SRTransItem)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate);

                var isAscending = filter.IsAscending ?? false;
                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    switch (filter.OrderBy)
                    {
                        case "OrderByTransaction-001":
                            tQ.OrderBy(isAscending ? tQ.TransNo.Ascending : tQ.TransNo.Descending);
                            break;

                        case "OrderByTransaction-002":
                            tQ.OrderBy(isAscending ? tQ.TransType.Ascending : tQ.TransType.Descending);
                            break;

                        case "OrderByTransaction-003":
                            tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                            break;

                        default:
                            tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                            break;
                    }
                }
                else
                {
                    tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                }
                var dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<List<Transaction>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var t = new Transaction
                    {
                        TransNo = (string)dr["TransNo"],
                        PersonId = (string)dr["PersonID"],
                        Srtransaction = (string)dr["SRTransaction"],
                        SrtransItem = (string)dr["SRTransItem"],
                        Amount = dr["Amount"] != DBNull.Value ? (decimal)dr["Amount"] : 0,
                        Description = dr["Description"] != DBNull.Value ? (string)dr["Description"] : string.Empty,
                        Photo = dr["Photo"] != DBNull.Value ? (byte[])dr["Photo"] : null,
                        TransType = dr["TransType"] != DBNull.Value ? (string)dr["TransType"] : string.Empty,
                        TransDate = dr["TransDate"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)dr["TransDate"]) : null,
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        CreatedByUserId = (string)dr["CreatedByUserID"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"]
                    };
                    data.Add(t);
                }

                response = new Response<List<Transaction>>
                {
                    Data = data,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<List<Transaction>>
                {
                    Data = data,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetTransactionNo", Name = "GetTransactionNo")]
        public ActionResult<Response<Transaction>> GetTransactionNo([FromQuery] TransactionFilter filter)
        {
            var data = new Transaction();
            var response = new Response<Transaction>();

            try
            {
                if (string.IsNullOrEmpty(filter.TransNo))
                {
                    response = new Response<Transaction>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "TransactionNo"),
                        Succeeded = !string.IsNullOrEmpty(data.TransNo)
                    };
                    return BadRequest(response);
                }

                var t = new BusinessObjects.Entity.Generated.Transaction();

                if (!t.LoadByPrimaryKey(filter.TransNo))
                {
                    response = new Response<Transaction>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(t.TransNo) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(t.TransNo)
                    };
                    return NotFound(response);
                }

                data = new Transaction
                {
                    TransNo = t.TransNo, PersonId = t.PersonID, Srtransaction = t.SRTransaction, SrtransItem = t.SRTransItem,
                    Amount = t.Amount, Description = t.Description, Photo = t.Photo, TransType = t.TransType, TransDate = t.TransDate.HasValue ? DateOnly.FromDateTime(t.TransDate.Value) : null,
                    CreatedDateTime = t.CreatedDateTime ?? new DateTime(), CreatedByUserId = t.CreatedByUserID, LastUpdateDateTime = t.LastUpdateDateTime ?? new DateTime(), LastUpdateByUserId = t.LastUpdateByUserID

                };

                response = new Response<Transaction>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(t.TransNo) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(t.TransNo)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<Transaction>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.TransNo) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.TransNo)
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetSumTransaction", Name = "GetSumTransaction")]
        public ActionResult<Response<List<Transaction>>> GetSumTransaction([FromQuery] TransactionFilter filter)
        {
            var data = new List<Transaction>();
            var response = new Response<List<Transaction>>();

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new Response<List<Transaction>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = data.Count > 0
                    };
                    return BadRequest(response);
                }

                var tQ = new BusinessObjects.Entity.Generated.TransactionQuery("tQ");
                var transQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("transQ");

                tQ.Select(tQ.Amount.Sum(), transQ.ItemName.As("SRTransaction"), tQ.PersonID, tQ.TransType)
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .GroupBy(tQ.SRTransaction)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate);
                var dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<List<Transaction>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return BadRequest(response);
                }

                decimal income = 0;
                decimal expenditure = 0;
                var t = new Transaction();

                foreach (DataRow dr in dt.Rows)
                {
                    t = new Transaction
                    {
                        Srtransaction = (string)dr["SRTransaction"],
                        Amount = (decimal)dr["Amount"],
                        PersonId = (string)dr["PersonID"],
                        TransType = (string)dr["TransType"],
                        TransDate = DateOnly.FromDateTime(DateFormat.DateTimeNow()),
                        CreatedDateTime = DateFormat.DateTimeNow(),
                        CreatedByUserId = _param.User,
                        LastUpdateDateTime = DateFormat.DateTimeNow(),
                        LastUpdateByUserId = _param.User
                    };
                    data.Add(t);

                    switch (dr["SRTransaction"])
                    {
                        case "Income":
                            income = dr["Amount"] != DBNull.Value ? (decimal)dr["Amount"] : 0;
                            break;

                        case "Expenditure":
                            expenditure = dr["Amount"] != DBNull.Value ? (decimal)dr["Amount"] : 0;
                            break;
                    }
                }
                decimal summary = income - expenditure;

                if (summary > 0)
                {
                    t = new Transaction
                    {
                        Srtransaction = "Summary",
                        Amount = summary,
                        PersonId = filter.PersonID,
                        TransType = "SU",
                        TransDate = DateOnly.FromDateTime(DateFormat.DateTimeNow()),
                        CreatedDateTime = DateFormat.DateTimeNow(),
                        CreatedByUserId = _param.User,
                        LastUpdateDateTime = DateFormat.DateTimeNow(),
                        LastUpdateByUserId = _param.User
                    };
                    data.Add(t);
                }

                response = new Response<List<Transaction>>
                {
                    Data = data,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<List<Transaction>>
                {
                    Data = data,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GenerateReportTransaction", Name = "GenerateReportTransaction")]
        public ActionResult<Response<byte[]>> GenerateReportTransaction([FromQuery] TransactionFilter filter)
        {
            var data = Array.Empty<byte>();
            var response = new Response<byte[]>();

            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    response = new Response<byte[]>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "PersonID"),
                        Succeeded = data.Length > 0
                    };
                    return BadRequest(response);
                }

                var fileName = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("BlankPDF");
                var filePath = Path.Combine("File", fileName);
                var fileInfo = _file.GetFileInfo(filePath);

                if (!fileInfo.Exists)
                {
                    response = new Response<byte[]>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.FailedMsg, "Retrieve", fileName, AppConstant.NotFoundMsg),
                        Succeeded = data.Length > 0
                    };
                    return NotFound(response);
                }

                #region List Transaksi Yang Akan Diambil
                var transaction = new List<Transaction>();
                var tQ = new BusinessObjects.Entity.Generated.TransactionQuery("tQ");
                var transQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("transQ");
                var itemQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("itemQ");

                tQ.Select(tQ.TransNo, tQ.Amount, tQ.Description, tQ.Photo, tQ.TransType, tQ.PersonID,
                    tQ.TransDate, transQ.ItemName.As("SRTransaction"), itemQ.ItemName.As("SRTransItem"),
                    tQ.CreatedDateTime, tQ.CreatedByUserID, tQ.LastUpdateDateTime, tQ.LastUpdateByUserID)
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .InnerJoin(itemQ).On(itemQ.ItemID == tQ.SRTransItem)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate && itemQ.StandardReferenceID.In("Expenditure", "Income"));

                var isAscending = filter.IsAscending ?? false;
                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    switch (filter.OrderBy)
                    {
                        case "OrderByTransaction-001":
                            tQ.OrderBy(isAscending ? tQ.TransNo.Ascending : tQ.TransNo.Descending);
                            break;

                        case "OrderByTransaction-002":
                            tQ.OrderBy(isAscending ? tQ.TransType.Ascending : tQ.TransType.Descending);
                            break;

                        case "OrderByTransaction-003":
                            tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                            break;

                        default:
                            tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                            break;
                    }
                }
                else
                {
                    tQ.OrderBy(isAscending ? tQ.TransDate.Ascending : tQ.TransDate.Descending);
                }
                var dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<byte[]>
                    {
                        Data = data,
                        Message = dt.Rows.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = dt.Rows.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var trans = new Transaction
                    {
                        TransNo = (string)dr["TransNo"],
                        PersonId = (string)dr["PersonID"],
                        Srtransaction = (string)dr["SRTransaction"],
                        SrtransItem = (string)dr["SRTransItem"],
                        Amount = dr["Amount"] != DBNull.Value ? (decimal)dr["Amount"] : 0,
                        Description = dr["Description"] != DBNull.Value ? (string)dr["Description"] : string.Empty,
                        Photo = dr["Photo"] != DBNull.Value ? (byte[])dr["Photo"] : null,
                        TransType = dr["TransType"] != DBNull.Value ? (string)dr["TransType"] : string.Empty,
                        TransDate = dr["TransDate"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)dr["TransDate"]) : null,
                        CreatedDateTime = (DateTime)dr["CreatedDateTime"],
                        CreatedByUserId = (string)dr["CreatedByUserID"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"]
                    };
                    transaction.Add(trans);
                }
                #endregion

                #region List Jumlah Total Pemasukan, Pengeluaran, Total Data Yang Diambil
                var sum = new List<Transaction>();
                tQ = new BusinessObjects.Entity.Generated.TransactionQuery("tQ");
                transQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("transQ");

                tQ.Select(tQ.Amount.Sum(), transQ.ItemName.As("SRTransaction"), tQ.PersonID, tQ.TransType)
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .GroupBy(tQ.SRTransaction)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate);
                var dts = tQ.LoadDataTable();

                if (dts.Rows.Count == 0)
                {
                    response = new Response<byte[]>
                    {
                        Data = data,
                        Message = dts.Rows.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = dts.Rows.Count > 0
                    };
                    return BadRequest(response);
                }

                decimal income = 0;
                decimal expenditure = 0;
                var sumtrans = new Transaction();

                foreach (DataRow dr in dts.Rows)
                {
                    sumtrans = new Transaction
                    {
                        Srtransaction = (string)dr["SRTransaction"],
                        Amount = (decimal)dr["Amount"],
                        PersonId = (string)dr["PersonID"],
                        TransType = (string)dr["TransType"],
                        TransDate = DateOnly.FromDateTime(DateFormat.DateTimeNow()),
                        CreatedDateTime = DateFormat.DateTimeNow(),
                        CreatedByUserId = _param.User,
                        LastUpdateDateTime = DateFormat.DateTimeNow(),
                        LastUpdateByUserId = _param.User
                    };
                    sum.Add(sumtrans);

                    switch (dr["SRTransaction"])
                    {
                        case "Income":
                            income = dr["Amount"] != DBNull.Value ? (decimal)dr["Amount"] : 0;
                            break;

                        case "Expenditure":
                            expenditure = dr["Amount"] != DBNull.Value ? (decimal)dr["Amount"] : 0;
                            break;
                    }
                }
                decimal summary = income - expenditure;

                if (summary > 0)
                {
                    sumtrans = new Transaction
                    {
                        Srtransaction = "Summary",
                        Amount = summary,
                        PersonId = filter.PersonID,
                        TransType = "SU",
                        TransDate = DateOnly.FromDateTime(DateFormat.DateTimeNow()),
                        CreatedDateTime = DateFormat.DateTimeNow(),
                        CreatedByUserId = _param.User,
                        LastUpdateDateTime = DateFormat.DateTimeNow(),
                        LastUpdateByUserId = _param.User
                    };
                    sum.Add(sumtrans);
                }
                #endregion

                #region Proses Generate Reporting
                var size = GeneratePDFFile.SetPageSize(filePath);

                //Generate PDF
                PdfWriter writer = new PdfWriter(filePath);
                PdfDocument pdfdoc = new PdfDocument(writer);
                Document doc = new Document(pdfdoc, size, false);

                //Header
                string title = $"{filter.PersonID} Transaction Report";
                var header = GeneratePDFFile.SetParagraph(title, 20, iText.Layout.Properties.TextAlignment.CENTER);
                doc.Add(header);

                //Subheader
                var firstItem = transaction.FirstOrDefault();
                var lastItem = transaction.LastOrDefault();
                var firstFormat = Converter.DateOnlyToDateTime(firstItem.TransDate, TimeOnly.MinValue);
                var lastFormat = Converter.DateOnlyToDateTime(lastItem.TransDate, TimeOnly.MinValue);
                var firstDate = DateFormat.DateTimeNow(DateFormat.Date, firstFormat);
                var lastDate = DateFormat.DateTimeNow(DateFormat.Date, lastFormat);

                var subtitle = $"Periode {firstDate} - {lastDate}";
                var subheader = GeneratePDFFile.SetParagraph(subtitle, 15, iText.Layout.Properties.TextAlignment.CENTER);
                doc.Add(subheader);

                //Line Separator
                var ls = GeneratePDFFile.SetLine();
                doc.Add(ls);

                //New Line
                var nl = GeneratePDFFile.SetNewLine();
                doc.Add(nl);

                // Table
                var culture = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("CurrencyFormat");
                var tbl = GeneratePDFFile.SetTable(5, true);

                //Table Header
                tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Description", iText.Layout.Properties.TextAlignment.CENTER));
                tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "TransNo", iText.Layout.Properties.TextAlignment.CENTER));
                tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Income", iText.Layout.Properties.TextAlignment.CENTER));
                tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Expenditure", iText.Layout.Properties.TextAlignment.CENTER));
                tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Trans Date", iText.Layout.Properties.TextAlignment.CENTER));

                //Table Data
                foreach (var item in transaction)
                {
                    var amountFormat = item.Amount != null ? Converter.Currency((decimal)item.Amount, culture) : string.Empty;
                    var description = !string.IsNullOrEmpty(item.Description) ? item.Description : item.SrtransItem;
                    var transDate = DateFormat.DateTimeNow(DateFormat.Date, Converter.DateOnlyToDateTime(item.TransDate, TimeOnly.MinValue));
                    var textAlignment = item.TransType == "IN" ? iText.Layout.Properties.TextAlignment.RIGHT : iText.Layout.Properties.TextAlignment.LEFT;

                    //Add Item To Cell
                    tbl.AddCell(GeneratePDFFile.SetCell(1, false, description, iText.Layout.Properties.TextAlignment.LEFT));
                    tbl.AddCell(GeneratePDFFile.SetCell(1, false, item.TransNo, iText.Layout.Properties.TextAlignment.LEFT));
                    tbl.AddCell(GeneratePDFFile.SetCell(1, false, item.TransType == "IN" ? amountFormat : string.Empty, iText.Layout.Properties.TextAlignment.RIGHT));
                    tbl.AddCell(GeneratePDFFile.SetCell(1, false, item.TransType == "OU" ? amountFormat : string.Empty, iText.Layout.Properties.TextAlignment.RIGHT));
                    tbl.AddCell(GeneratePDFFile.SetCell(1, false, transDate, iText.Layout.Properties.TextAlignment.RIGHT));
                }

                //Table Footer
                var sumIncome = sum.FirstOrDefault(item => item.Srtransaction == "Income");
                var sumExpenditure = sum.FirstOrDefault(item => item.Srtransaction == "Expenditure");
                var sumSummary = sum.FirstOrDefault(item => item.Srtransaction == "Summary");

                var _income = sumIncome != null ? Converter.Currency(sumIncome.Amount ?? 0, culture) : string.Empty;
                var _expenditure = sumIncome != null ? Converter.Currency(sumExpenditure.Amount ?? 0, culture) : string.Empty;
                var _summary = sumSummary != null ? Converter.Currency(sumSummary.Amount ?? 0, culture) : string.Empty;

                tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, "Summary", iText.Layout.Properties.TextAlignment.CENTER));
                tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, string.Empty, iText.Layout.Properties.TextAlignment.LEFT));
                tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, _income, iText.Layout.Properties.TextAlignment.RIGHT));
                tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, _expenditure, iText.Layout.Properties.TextAlignment.RIGHT));
                tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, _summary, iText.Layout.Properties.TextAlignment.RIGHT));
                doc.Add(tbl);

                var addHeader = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("IsAddHeader");
                var isAddHeader = Converter.StringToBool(addHeader, false);
                if (isAddHeader)
                {

                }

                var addFooter = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("IsAddFooter");
                var isAddFooter = Converter.StringToBool(addFooter, false);
                if (isAddFooter)
                {
                    var date = DateFormat.DateTimeNow(DateFormat.Dateshortmonthhourminute, null);
                    string footer = $"Generated On {date}";
                    var parFooter = GeneratePDFFile.SetParagraph(footer, 10, iText.Layout.Properties.TextAlignment.CENTER);
                    GeneratePDFFile.SetFooterPages(parFooter, pdfdoc, doc);
                }

                var addPageNumber = BusinessObjects.Entity.Custom.AppParameter.GetAppParameterValue("IsAddPageNumber");
                var isAddPageNumber = Converter.StringToBool(addPageNumber, false);
                if (isAddPageNumber)
                {
                    GeneratePDFFile.SetPagesNumber(pdfdoc, doc);
                }

                //Close Doc
                doc.Close();
                #endregion

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                response = new Response<byte[]>
                {
                    Data = fileBytes,
                    Message = fileBytes.Length > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = fileBytes.Length > 0
                };
                return Ok(response);
                //return File(fileBytes, "application/pdf", $"{filter.PersonID} Transaction Report");
            }
            catch (Exception e)
            {
                response = new Response<byte[]>
                {
                    Data = data,
                    Message = $"{(data.Length > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Length > 0
                };
                return BadRequest(response);
            }
        }
    }
}