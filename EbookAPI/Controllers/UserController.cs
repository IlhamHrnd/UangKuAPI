using EbookAPI.Context;
using EbookAPI.Filter;
using EbookAPI.Model;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EbookAPI.Controllers
{
    [Route("[controller]", Name = "UserAPI")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<User>> GetAllUser([FromQuery] UserFilter filter)
        {
            try
            {
                var validFilter = new UserFilter(filter.PageNumber, filter.PageSize);
                var pageNumber = validFilter.PageNumber;
                var pageSize = validFilter.PageSize;
                var query = $@"SELECT u.Username, u.ActiveDate,
                                u.LastLogin, u.LastUpdateDateTime, u.LastUpdateByUser, u.PersonID,
                                asri.ItemName AS 'SexName',
                                asri02.ItemName AS 'AccessName',
                                asri03.ItemName AS 'StatusName'
                                FROM User AS u
                                INNER JOIN AppStandardReferenceItem AS asri
    	                            ON asri.StandardReferenceID = 'Sex'
                                    AND asri.ItemID = u.SRSex
                                INNER JOIN AppStandardReferenceItem AS asri02
    	                            ON asri02.StandardReferenceID = 'Access'
                                    AND asri02.ItemID = u.SRAccess
                                INNER JOIN AppStandardReferenceItem AS asri03
    	                            ON asri03.StandardReferenceID = 'Status'
                                    AND asri03.ItemID = u.SRStatus;";
                var pagedData = await _context.Users.FromSqlRaw(query).ToListAsync();
                var totalRecord = await _context.Users.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / validFilter.PageSize);

                string? prevPageLink = validFilter.PageNumber > 1
                    ? Url.Link("UserAPI", new { PageNumber = validFilter.PageNumber - 1, validFilter.PageSize })
                    : null;

                string? nextPageLink = validFilter.PageNumber < totalPages
                    ? Url.Link("UserAPI", new { PageNumber = validFilter.PageNumber + 1, validFilter.PageSize })
                    : null;

                var response = new PageResponse<List<User>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
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

        [HttpGet("GetLoginUserName")]
        public async Task<ActionResult<User>> GetLoginUserName([FromQuery] UserNameFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.Username) || string.IsNullOrEmpty(filter.Password))
                {
                    return BadRequest("Username Or Password Is Required");
                }
                var query = $@"SELECT u.Username, u.ActiveDate,
                                u.LastLogin, u.LastUpdateDateTime, u.LastUpdateByUser, u.PersonID,
                                asri.ItemName AS 'SexName',
                                asri02.ItemName AS 'AccessName',
                                asri03.ItemName AS 'StatusName'
                                FROM User AS u
                                INNER JOIN AppStandardReferenceItem AS asri
    	                            ON asri.StandardReferenceID = 'Sex'
                                    AND asri.ItemID = u.SRSex
                                INNER JOIN AppStandardReferenceItem AS asri02
    	                            ON asri02.StandardReferenceID = 'Access'
                                    AND asri02.ItemID = u.SRAccess
                                INNER JOIN AppStandardReferenceItem AS asri03
    	                            ON asri03.StandardReferenceID = 'Status'
                                    AND asri03.ItemID = u.SRStatus
                                WHERE u.Username = '{filter.Username}'
                                    AND u.Password = '{Encryptor.Encryptor.DataEncrypt(filter.Password)}';";
                var response = await _context.Users.FromSqlRaw(query).ToListAsync();
                if (response == null || response.Count == 0 || !response.Any())
                {
                    return NotFound("User Not Found");
                }
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
    }
}
