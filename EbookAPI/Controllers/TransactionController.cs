﻿using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using static UangKuAPI.BusinessObjects.Helper.Helper;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using static UangKuAPI.BusinessObjects.Helper.Converter;
using static UangKuAPI.BusinessObjects.Helper.AppConstant;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Entity;
using Models = UangKuAPI.BusinessObjects.Model;
using System.Data;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "TransactionAPI")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("PostTransaction", Name = "PostTransaction")]
        public async Task<IActionResult> PostTransaction([FromBody] Transaction2 transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest($"Transaction Are Required");
                }

                var trans = await _context.Transaction
                    .Where(t => t.TransNo == transaction.TransNo)
                    .ToListAsync();

                if (trans.Any())
                {
                    return BadRequest($"{transaction.TransNo} Already Exist");
                }

                var tr = new Models.Transaction
                {
                    TransNo = transaction.TransNo, SRTransaction = transaction.SRTransaction, SRTransItem = transaction.SRTransItem,
                    Amount = transaction.Amount, Description = transaction.Description, Photo = StringToByte(transaction.Photo),
                    CreatedDateTime = DateFormat.DateTimeNow(), CreatedByUserID = transaction.CreatedByUserID, LastUpdateDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserID = transaction.LastUpdateByUserID, TransType = transaction.TransType, TransDate = transaction.TransDate,
                    PersonID = transaction.PersonID
                };
                _context.Add(tr);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"Transaction No {transaction.TransNo} Created Successfully")
                    : BadRequest($"Failed To Insert Data For Transaction No {transaction.TransNo}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchTransaction", Name = "PatchTransaction")]
        public async Task<IActionResult> PatchTransaction([FromBody] Transaction2 transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest($"Transaction Are Required");
                }

                var trans = await _context.Transaction
                    .FirstOrDefaultAsync(t => t.TransNo == transaction.TransNo);

                if (trans == null)
                {
                    return BadRequest($"{transaction.TransNo} Not Found");
                }

                trans.SRTransaction = transaction.SRTransaction;
                trans.SRTransItem = transaction.SRTransItem;
                trans.Amount = transaction.Amount;
                trans.Description = transaction.Description;
                trans.Photo = StringToByte(transaction.Photo);
                trans.LastUpdateDateTime = DateFormat.DateTimeNow();
                trans.LastUpdateByUserID = transaction.LastUpdateByUserID;
                trans.TransType = transaction.TransType;
                trans.TransDate = transaction.TransDate;
                _context.Update(trans);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{transaction.TransNo} Update Successfully")
                    : NotFound($"{transaction.TransNo} Not Found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetNewTransactionNo", Name = "GetNewTransactionNo")]
        public IActionResult GetNewTransactionNo([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.TransType))
                {
                    return BadRequest($"Transaction Type Is Required");
                }
                string transDate = DateFormat.DateTimeNow(Shortyearpattern, DateFormat.DateTimeNow());
                int number = 1;
                string formattedNumber = NumberingFormat(number, "D3");
                string transNo = $"TRA/{filter.TransType}/{transDate}-{formattedNumber}";

                while (_context.Transaction.Any(t => t.TransNo == transNo))
                {
                    number++;
                    formattedNumber = NumberingFormat(number, "D3");
                    transNo = $"TRA/{filter.TransType}/{transDate}-{formattedNumber}";
                }

                number++;

                return Ok(transNo);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllTransaction", Name = "GetAllTransaction")]
        public ActionResult<PageResponse<Models.Transaction>> GetAllTransaction([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"Person ID Is Required");
                }

                var tQ = new TransactionQuery("tQ");
                var transQ = new AppstandardreferenceitemQuery("transQ");
                var itemQ = new AppstandardreferenceitemQuery("itemQ");

                tQ.Select(tQ.TransNo, tQ.Amount, tQ.Description, tQ.Photo, tQ.TransType, tQ.PersonID,
                    tQ.TransDate, transQ.ItemName.As("SRTransaction"), itemQ.ItemName.As("SRTransItem"))
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .InnerJoin(itemQ).On(itemQ.StandardReferenceID == "Expenditure" && itemQ.ItemID == tQ.SRTransItem)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate);

                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    switch (filter.OrderBy)
                    {
                        case "OrderByTransaction-001":
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransNo.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransNo.Descending);
                            }
                            break;

                        case "OrderByTransaction-002":
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransType.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransType.Descending);
                            }
                            break;

                        case "OrderByTransaction-003":
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransDate.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransDate.Descending);
                            }
                            break;

                        default:
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransDate.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransDate.Descending);
                            }
                            break;
                    }
                }
                else
                {
                    if (filter.IsAscending ?? false)
                    {
                        tQ.OrderBy(tQ.TransDate.Ascending);
                    }
                    else
                    {
                        tQ.OrderBy(tQ.TransDate.Descending);
                    }
                }

                DataTable dtRecord = tQ.LoadDataTable();

                tQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var pagedData = new List<Models.Transaction>();

                foreach (DataRow dr in dt.Rows)
                {
                    var trans = new Models.Transaction
                    {
                        TransNo = (string)dr["TransNo"],
                        Amount = (decimal)dr["Amount"],
                        Description = (string)dr["Description"],
                        Photo = (byte[])dr["Photo"],
                        TransType = (string)dr["TransType"],
                        PersonID = (string)dr["PersonID"],
                        TransDate = (DateTime)dr["TransDate"],
                        SRTransaction = (string)dr["SRTransaction"],
                        SRTransItem = (string)dr["SRTransItem"]
                    };
                    pagedData.Add(trans);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllTransaction", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllTransaction", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<Models.Transaction>>(pagedData, filter.PageNumber, filter.PageSize)
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

        [HttpGet("GetAllPDFTransaction", Name = "GetAllPDFTransaction")]
        public ActionResult<List<Models.Transaction>> GetAllPDFTransaction([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"Person ID Is Required");
                }

                var tQ = new TransactionQuery("tQ");
                var transQ = new AppstandardreferenceitemQuery("transQ");
                var itemQ = new AppstandardreferenceitemQuery("itemQ");

                tQ.Select(tQ.TransNo, tQ.Amount, tQ.Description, tQ.Photo, tQ.TransType, tQ.PersonID,
                    tQ.TransDate, transQ.ItemName.As("SRTransaction"), itemQ.ItemName.As("SRTransItem"))
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .InnerJoin(itemQ).On(itemQ.ItemID == tQ.SRTransItem)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate);

                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    switch (filter.OrderBy)
                    {
                        case "OrderByTransaction-001":
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransNo.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransNo.Descending);
                            }
                            break;

                        case "OrderByTransaction-002":
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransType.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransType.Descending);
                            }
                            break;

                        case "OrderByTransaction-003":
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransDate.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransDate.Descending);
                            }
                            break;

                        default:
                            if (filter.IsAscending ?? false)
                            {
                                tQ.OrderBy(tQ.TransDate.Ascending);
                            }
                            else
                            {
                                tQ.OrderBy(tQ.TransDate.Descending);
                            }
                            break;
                    }
                }
                else
                {
                    if (filter.IsAscending ?? false)
                    {
                        tQ.OrderBy(tQ.TransDate.Ascending);
                    }
                    else
                    {
                        tQ.OrderBy(tQ.TransDate.Descending);
                    }
                }
                DataTable dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var response = new List<Models.Transaction>();

                foreach (DataRow dr in dt.Rows)
                {
                    var trans = new Models.Transaction
                    {
                        TransNo = (string)dr["TransNo"],
                        Amount = (decimal)dr["Amount"],
                        Description = (string)dr["Description"],
                        Photo = (byte[])dr["Photo"],
                        TransType = (string)dr["TransType"],
                        PersonID = (string)dr["PersonID"],
                        TransDate = (DateTime)dr["TransDate"],
                        SRTransaction = (string)dr["SRTransaction"],
                        SRTransItem = (string)dr["SRTransItem"]
                    };
                    response.Add(trans);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetTransactionNo", Name = "GetTransactionNo")]
        public ActionResult<Models.Transaction> GetTransactionNo([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.TransNo))
                {
                    return BadRequest($"Transaction No Is Required");
                }

                var tQ = new TransactionQuery("tQ");
                var transQ = new AppstandardreferenceitemQuery("transQ");
                var itemQ = new AppstandardreferenceitemQuery("itemQ");

                tQ.Select(tQ.TransNo, tQ.Amount, tQ.Description, tQ.Photo, tQ.TransType, tQ.PersonID,
                    tQ.TransDate, transQ.ItemName.As("SRTransaction"), itemQ.ItemName.As("SRTransItem"))
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .InnerJoin(itemQ).On(itemQ.ItemID == tQ.SRTransItem)
                    .Where(tQ.TransNo == filter.TransNo);
                DataTable dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var response = new List<Models.Transaction>();

                foreach (DataRow dr in dt.Rows)
                {
                    var trans = new Models.Transaction
                    {
                        TransNo = (string)dr["TransNo"],
                        Amount = (decimal)dr["Amount"],
                        Description = (string)dr["Description"],
                        Photo = (byte[])dr["Photo"],
                        TransType = (string)dr["TransType"],
                        PersonID = (string)dr["PersonID"],
                        TransDate = (DateTime)dr["TransDate"],
                        SRTransaction = (string)dr["SRTransaction"],
                        SRTransItem = (string)dr["SRTransItem"]
                    };
                    response.Add(trans);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetSumTransaction", Name = "GetSumTransaction")]
        public ActionResult<List<SumTransaction>> GetSumTransaction([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest("Person ID Is Required");
                }

                var tQ = new TransactionQuery("tQ");
                var transQ = new AppstandardreferenceitemQuery("transQ");

                tQ.Select(tQ.Amount.Sum(), transQ.ItemName.As("SRTransaction"))
                    .InnerJoin(transQ).On(transQ.StandardReferenceID == "Transaction" && transQ.ItemID == tQ.SRTransaction)
                    .GroupBy(tQ.SRTransaction)
                    .Where(tQ.PersonID == filter.PersonID && tQ.TransDate >= filter.StartDate && tQ.TransDate <= filter.EndDate);
                DataTable dt = tQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Transaction For {filter.PersonID} Not Found");
                }

                var response = new List<SumTransaction>();

                foreach (DataRow dr in dt.Rows)
                {
                    var sum = new SumTransaction
                    {
                        Amount = (decimal)dr["Amount"],
                        SRTransaction = (string)dr["SRTransaction"]
                    };
                    response.Add(sum);
                }

                return response;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}