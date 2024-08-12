using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using static UangKuAPI.BusinessObjects.Helper.Helper;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using UangKuAPI.BusinessObjects.Filter;
using System.Linq.Expressions;

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
                string updatedate = DateFormat.DateTimeNow(Longyearpattern, DateFormat.DateTimeNow());
                string createdate = DateFormat.DateTimeNow(Longyearpattern, DateFormat.DateTimeNow());
                string transdate = DateFormat.DateTimeIsNull(transaction.TransDate);

                var query = $@"INSERT INTO `Transaction`(`TransNo`, `SRTransaction`, `SRTransItem`, `Amount`, `Description`, 
                                `Photo`, `CreatedDateTime`, `CreatedByUserID`, `LastUpdateDateTime`, `LastUpdateByUserID`, 
                                `TransType`, `TransDate`, `PersonID`) 
                                VALUES('{transaction.TransNo}', '{transaction.SRTransaction}', '{transaction.SRTransItem}',
                                '{transaction.Amount}', '{transaction.Description}', '{transaction.Photo}', '{createdate}',
                                '{transaction.CreatedByUserID}', '{updatedate}', '{transaction.LastUpdateByUserID}', '{transaction.TransType}', 
                                '{transdate}', '{transaction.PersonID}');";

                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);
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

                string updatedate = DateFormat.DateTimeNow(Longyearpattern, DateFormat.DateTimeNow());
                string transdate = DateFormat.DateTimeIsNull(transaction.TransDate);

                var query = $@"UPDATE `Transaction`
                                SET `SRTransaction` = '{transaction.SRTransaction}',
                                `SRTransItem` = '{transaction.SRTransItem}',
                                `Amount` = '{transaction.Amount}',
                                `Description` = '{transaction.Description}',
                                `Photo` = '{transaction.Photo}',   
                                `LastUpdateDateTime` = '{updatedate}',
                                `LastUpdateByUserID` = '{transaction.LastUpdateByUserID}',
                                `TransType` = '{transaction.TransType}',
                                `TransDate` = '{transdate}'
                                WHERE `TransNo` = '{transaction.TransNo}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);
                return response > 0 
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
        public async Task<ActionResult<Transaction>> GetAllTransaction([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"Person ID Is Required");
                }

                DateTime startDate = filter.StartDate.HasValue ? filter.StartDate.Value : DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endDate = filter.StartDate.HasValue ? filter.StartDate.Value : DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var pageNumber = filter.PageNumber;
                var pageSize = filter.PageSize;

                var query = (from t in _context.Transaction
                                   join trans in _context.AppStandardReferenceItems
                                      on new { StandardReferenceID = "Transaction", ItemID = t.SRTransaction }
                                      equals new { trans.StandardReferenceID, trans.ItemID }
                                    join item in _context.AppStandardReferenceItems
                                        on new { ItemID = t.SRTransItem}
                                        equals new { item.ItemID }
                                    where t.PersonID == filter.PersonID && t.TransDate >= startDate && t.TransDate <= endDate
                                   select new Transaction
                                   {
                                        TransNo = t.TransNo, Amount = t.Amount, Description = t.Description, Photo = t.Photo,
                                        TransType = t.TransType, PersonID = t.PersonID, TransDate = t.TransDate,
                                        SRTransaction = trans.ItemName, SRTransItem = item.ItemName
                                   })
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize);

                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    var orderByMap = new Dictionary<string, Expression<Func<Transaction, object>>>
                        {
                            { "OrderByTransaction-001", t => t.TransNo },
                            { "OrderByTransaction-002", t => t.TransType },
                            { "OrderByTransaction-003", t => t.TransDate }
                        };
                    if (orderByMap.TryGetValue(filter.OrderBy ?? string.Empty, out var orderByExpression))
                    {
                        query = (filter.IsAscending ?? false)
                            ? query.OrderBy(t => orderByExpression)
                            : query.OrderByDescending(t => orderByExpression);
                    }
                    else
                    {
                        // Default ordering if the provided OrderBy key is not found
                        query = (filter.IsAscending ?? false)
                            ? query.OrderBy(t => t.TransDate)
                            : query.OrderByDescending(t => t.TransDate);
                    }
                }
                else
                {
                    query = query.OrderByDescending(t => t.TransDate);
                }

                var pagedData = await query.ToListAsync();
                var totalRecord = await _context.Transaction
                    .Where(t => t.PersonID == filter.PersonID && t.TransDate >= startDate && t.TransDate <= endDate)
                    .CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                var tes = pagedData;

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllTransaction", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllTransaction", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<Transaction>>(pagedData, filter.PageNumber, filter.PageSize)
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecord,
                    PrevPageLink = prevPageLink,
                    NextPageLink = nextPageLink,
                    Message = pagedData.Count > 0 ? "Data Found" : "Data Not Found",
                    Succeeded = pagedData.Count > 0
                };

                return pagedData.Count > 0
                    ? Ok(response)
                    : NotFound("PersonID Not Found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetAllPDFTransaction", Name = "GetAllPDFTransaction")]
        public async Task<ActionResult<List<Transaction>>> GetAllPDFTransaction([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest($"Person ID Is Required");
                }

                DateTime startDate = filter.StartDate.HasValue ? filter.StartDate.Value : DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endDate = filter.StartDate.HasValue ? filter.StartDate.Value : DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var query = (from t in _context.Transaction
                                   join trans in _context.AppStandardReferenceItems
                                      on new { StandardReferenceID = "Transaction", ItemID = t.SRTransaction }
                                      equals new { trans.StandardReferenceID, trans.ItemID }
                                    join item in _context.AppStandardReferenceItems
                                        on new { ItemID = t.SRTransItem}
                                        equals new { item.ItemID }
                                    where t.PersonID == filter.PersonID && t.TransDate >= startDate && t.TransDate <= endDate
                                   select new Transaction
                                   {
                                        TransNo = t.TransNo, Amount = t.Amount, Description = t.Description, Photo = t.Photo,
                                        TransType = t.TransType, PersonID = t.PersonID, TransDate = t.TransDate,
                                        SRTransaction = trans.ItemName, SRTransItem = item.ItemName
                                   });

                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    var orderByMap = new Dictionary<string, Expression<Func<Transaction, object>>>
                        {
                            { "OrderByTransaction-001", t => t.TransNo },
                            { "OrderByTransaction-002", t => t.TransType },
                            { "OrderByTransaction-003", t => t.TransDate }
                        };
                    if (orderByMap.TryGetValue(filter.OrderBy ?? string.Empty, out var orderByExpression))
                    {
                        query = (filter.IsAscending ?? false)
                            ? query.OrderBy(t => orderByExpression)
                            : query.OrderByDescending(t => orderByExpression);
                    }
                    else
                    {
                        // Default ordering if the provided OrderBy key is not found
                        query = (filter.IsAscending ?? false)
                            ? query.OrderBy(t => t.TransDate)
                            : query.OrderByDescending(t => t.TransDate);
                    }
                }
                else
                {
                    query = query.OrderByDescending(t => t.TransDate);
                }

                var pagedData = await query.ToListAsync();
                return pagedData.Count > 0
                    ? Ok(pagedData)
                    : NotFound("PersonID Not Found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetTransactionNo", Name = "GetTransactionNo")]
        public async Task<ActionResult<Transaction>> GetTransactionNo([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.TransNo))
                {
                    return BadRequest($"Transaction No Is Required");
                }
                
                var response = await (from t in _context.Transaction
                                join trans in _context.AppStandardReferenceItems
                                      on new { StandardReferenceID = "Transaction", ItemID = t.SRTransaction }
                                      equals new { trans.StandardReferenceID, trans.ItemID }
                                join item in _context.AppStandardReferenceItems
                                    on new { ItemID = t.SRTransItem }
                                    equals new { item.ItemID }
                                where t.TransNo == filter.TransNo
                                select new Transaction
                                {
                                    TransNo = t.TransNo, Amount = t.Amount, Description = t.Description, Photo = t.Photo,
                                    TransType = t.TransType, PersonID = t.PersonID, TransDate = t.TransDate,
                                    SRTransaction = trans.ItemName, SRTransItem = item.ItemName
                                })
                                .ToListAsync();

                return response == null || response.Count == 0 || !response.Any() 
                    ? (ActionResult<Transaction>)NotFound($"{filter.TransNo} Not Found") 
                    : (ActionResult<Transaction>)Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpGet("GetSumTransaction", Name = "GetSumTransaction")]
        public async Task<IActionResult> GetSumTransaction([FromQuery] TransactionFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.PersonID))
                {
                    return BadRequest("Person ID Is Required");
                }

                DateTime startDate = filter.StartDate.HasValue ? filter.StartDate.Value : DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endDate = filter.StartDate.HasValue ? filter.StartDate.Value : DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var response = await (from t in _context.Transaction
                                join trans in _context.AppStandardReferenceItems
                                    on new { StandardReferenceID = "Transaction", ItemID = t.SRTransaction }
                                    equals new { trans.StandardReferenceID, trans.ItemID }
                                where t.PersonID == filter.PersonID && t.TransDate >= startDate && t.TransDate <= endDate
                                group t by new {t.SRTransaction, trans.ItemName} into g
                                select new SumTransaction
                                {
                                    Amount = g.Sum(t => t.Amount), SRTransaction = g.Key.ItemName
                                })
                                .ToListAsync();

                return response == null || response.Count == 0 || !response.Any()
                    ? NotFound($"Transaction For {filter.PersonID} Not Found")
                    : Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
