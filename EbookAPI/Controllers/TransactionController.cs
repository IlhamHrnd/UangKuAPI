using EbookAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "TransactionAPI")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _content;

        public TransactionController(AppDbContext content)
        {
            _content = content;
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
                string updatedate = $"{dateTime: yyyy-MM-dd HH:mm:ss}";
                string createdate = $"{dateTime: yyyy-MM-dd HH:mm:ss}";

                var query = $@"INSERT INTO `Transaction`(`TransNo`, `SRTransaction`, `SRTransItem`, `Amount`, `Description`, 
                                `Photo`, `CreatedDateTime`, `CreatedByUserID`, `LastUpdateDateTime`, `LastUpdateByUserID`) 
                                VALUES('{transaction.TransNo}', '{transaction.SRTransaction}', '{transaction.SRTransItem}',
                                '{transaction.Amount}', '{transaction.Description}', '{transaction.Photo}', '{createdate}',
                                '{transaction.CreatedByUserID}', '{updatedate}', '{transaction.LastUpdateByUserID}');";

                await _content.Database.ExecuteSqlRawAsync(query);

                return Ok($"Transaction No {transaction.TransNo} Created Successfully");
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
                DateTime dateTime = DateTime.Now;
                string updatedate = $"{dateTime: yyyy-MM-dd HH:mm:ss}";

                //UPDATE `Transaction` SET `TransNo`='[value-1]',`SRTransaction`='[value-2]',`SRTransItem`='[value-3]',`Amount`='[value-4]',`Description`='[value-5]',`Photo`='[value-6]',`CreatedDateTime`='[value-7]',`CreatedByUserID`='[value-8]',`LastUpdateDateTime`='[value-9]',`LastUpdateByUserID`='[value-10]' WHERE 1

                var query = $@"UPDATE `Transaction`
                                SET `SRTransaction` = '{transaction.SRTransaction}',
                                `SRTransItem` = '{transaction.SRTransItem}',
                                `Amount` = '{transaction.Amount}',
                                `Description` = '{transaction.Description}',
                                `Photo` = '{transaction.Photo}',   
                                `LastUpdateDateTime` = '{updatedate}',
                                `LastUpdateByUserID` = '{transaction.LastUpdateByUserID}'
                                WHERE `TransNo` = '{transaction.TransNo}';";

                var response = await _content.Database.ExecuteSqlRawAsync(query);

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
    }
}
