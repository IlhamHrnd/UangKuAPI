﻿using EbookAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Filter;
using UangKuAPI.Helper;
using UangKuAPI.Model;

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
        public async Task<IActionResult> PostTransaction([FromBody] PostTransaction transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest($"Transaction Are Required");
                }
                DateTime dateTime = DateTime.Now;
                string updatedate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);
                string createdate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);
                string transdate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmdd);

                var query = $@"INSERT INTO `Transaction`(`TransNo`, `SRTransaction`, `SRTransItem`, `Amount`, `Description`, 
                                `Photo`, `CreatedDateTime`, `CreatedByUserID`, `LastUpdateDateTime`, `LastUpdateByUserID`, 
                                `TransType`, `TransDate`, `PersonID`) 
                                VALUES('{transaction.TransNo}', '{transaction.SRTransaction}', '{transaction.SRTransItem}',
                                '{transaction.Amount}', '{transaction.Description}', '{transaction.Photo}', '{createdate}',
                                '{transaction.CreatedByUserID}', '{updatedate}', '{transaction.LastUpdateByUserID}', '{transaction.TransType}', 
                                '{transdate}', '{transaction.PersonID}');";

                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

                if (rowsAffected > 0)
                {
                    return Ok($"Transaction No {transaction.TransNo} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For Transaction No {transaction.TransNo}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchTransaction", Name = "PatchTransaction")]
        public async Task<IActionResult> PatchTransaction([FromBody] PatchTransaction transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest($"Transaction Are Required");
                }
                string updatedate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhhmmss);
                string transdate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhh2);
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

                if (response > 0)
                {
                    return Ok($"{transaction.TransNo} Update Successfully");
                }
                else
                {
                    return NotFound($"{transaction.TransNo} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetNewTransactionNo", Name = "GetNewTransactionNo")]
        public IActionResult GetNewTransactionNo([FromQuery] string TransType)
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
                string transNo = $"TRA/{TransType}/{transDate}-{formattedNumber}";

                while (_context.Transaction.Any(t => t.TransNo == transNo))
                {
                    number++;
                    formattedNumber = number.ToString("D3");
                    transNo = $"TRA/{TransType}/{transDate}-{formattedNumber}";
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

                string startDate = DateTimeFormat.DateTimeStartMonth(1, DateStringFormat.Yymmddhh2);
                string endDate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhh2);

                var validFilter = new TransactionFilter(filter.PageNumber, filter.PageSize, filter.PersonID);
                var pageNumber = validFilter.PageNumber;
                var pageSize = validFilter.PageSize;
                var personID = validFilter.PersonID;
                var query = $@"SELECT t.TransNO, t.Amount, t.Description, t.Photo, t.TransType,
                                t.PersonID, t.TransDate,
                                asriTrans.ItemName AS SRTransaction,
                                asriTransItem.ItemName AS SRTransItem
                                FROM Transaction AS t
                                INNER JOIN AppStandardReferenceItem AS asriTrans
                                    ON asriTrans.ItemID = t.SRTransaction
                                    AND asriTrans.StandardReferenceID = 'Transaction'
                                INNER JOIN AppStandardReferenceItem AS asriTransItem
                                    ON asriTransItem.ItemID = t.SRTransItem
                                WHERE t.PersonID = '{personID}'
                                AND t.TransDate BETWEEN '{startDate}' AND '{endDate}'
                                ORDER BY t.TransNO DESC
                                OFFSET {(pageNumber - 1) * pageSize} ROWS
                                FETCH NEXT {pageSize} ROWS ONLY;";
                var pagedData = await _context.Transaction.FromSqlRaw(query).ToListAsync();

                if (pagedData == null || pagedData.Count == 0 || !pagedData.Any())
                {
                    return NotFound("Person ID Not Found");
                }

                var totalRecord = await _context.Transaction.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / validFilter.PageSize);

                string? prevPageLink = validFilter.PageNumber > 1
                    ? Url.Link("GetAllTransaction", new { PageNumber = validFilter.PageNumber - 1, validFilter.PageSize })
                    : null;

                string? nextPageLink = validFilter.PageNumber < totalPages
                    ? Url.Link("GetAllTransaction", new { PageNumber = validFilter.PageNumber + 1, validFilter.PageSize })
                    : null;

                var response = new PageResponse<List<Transaction>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
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

        [HttpGet("GetTransactionNo", Name = "GetTransactionNo")]
        public async Task<ActionResult<Transaction>> GetTransactionNo([FromQuery] string TransNo)
        {
            try
            {
                if (string.IsNullOrEmpty(TransNo))
                {
                    return BadRequest($"Transaction No Is Required");
                }
                var query = $@"SELECT t.TransNO, t.Amount, t.Description, t.Photo, t.TransType,
                                t.PersonID, t.TransDate,
                                asriTrans.ItemName AS SRTransaction,
                                asriTransItem.ItemName AS SRTransItem
                                FROM Transaction AS t
                                INNER JOIN AppStandardReferenceItem AS asriTrans
                                    ON asriTrans.ItemID = t.SRTransaction
                                    AND asriTrans.StandardReferenceID = 'Transaction'
                                INNER JOIN AppStandardReferenceItem AS asriTransItem
                                    ON asriTransItem.ItemID = t.SRTransItem
                                WHERE t.TransNo = '{TransNo}';";
                var response = await _context.Transaction.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound($"{TransNo} Not Found");
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
        public async Task<IActionResult> GetSumTransaction([FromQuery] string personID)
        {
            try
            {
                if (string.IsNullOrEmpty(personID))
                {
                    return BadRequest("Person ID Is Required");
                }
                string startDate = DateTimeFormat.DateTimeStartMonth(1, DateStringFormat.Yymmddhh2);
                string endDate = DateTimeFormat.DateTimeNow(DateStringFormat.Yymmddhh2);

                var query = $@"SELECT asri.ItemName AS 'SRTransaction', SUM(t.Amount) AS 'Amount'
                                FROM Transaction AS t
                                INNER JOIN AppStandardReferenceItem AS asri
                                    ON asri.ItemID = t.SRTransaction
                                WHERE t.PersonID = '{personID}'
                                    AND t.TransDate BETWEEN '{startDate}' AND '{endDate}'
                                GROUP BY t.SRTransaction;";

                var result = await _context.GetSumTransactions.FromSqlRaw(query).ToListAsync();

                if (result == null || result.Count == 0 || !result.Any())
                {
                    return NotFound($"Transaction For {personID} Not Found");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
