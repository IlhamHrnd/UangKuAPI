using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Helper;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;
using UangKuAPI.BusinessObjects.Filter;

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

        [HttpGet("GetAllUser", Name = "GetAllUser")]
        public async Task<ActionResult<User>> GetAllUser([FromQuery] UserFilter filter)
        {
            try
            {
                var pageNumber = filter.PageNumber;
                var pageSize = filter.PageSize;
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
                                                ORDER BY u.Username ASC
                                                OFFSET {(pageNumber - 1) * pageSize} ROWS
                                                FETCH NEXT {pageSize} ROWS ONLY;";
                var pagedData = await _context.Users.FromSqlRaw(query).ToListAsync();
                var totalRecord = await _context.Users.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllUser", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllUser", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<User>>(pagedData, filter.PageNumber, filter.PageSize)
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

        [HttpGet("GetLoginUserName", Name = "GetLoginUserName")]
        public async Task<ActionResult<User>> GetLoginUserName([FromQuery] UserFilter filter)
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

        [HttpPost("CreateUsername", Name = "CreateUsername")]
        public async Task<IActionResult> CreateUsername([FromBody] User user, [FromQuery] string password, [FromQuery] string email)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                {
                    return BadRequest("User Data, Password, And Email Are Required.");
                }

                string access = "Access-02";
                string status = "Status-001";
                string date = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);

                var query = $@"INSERT INTO `User`(`Username`, `Password`, `SRSex`, `SRAccess`, `Email`, `SRStatus`, `ActiveDate`, `LastLogin`, `LastUpdateDateTime`, `LastUpdateByUser`, `PersonID`)
                        VALUES ('{user.Username}', '{Encryptor.Encryptor.DataEncrypt(password)}', 
                                '{user.SexName}', '{access}', '{Encryptor.Encryptor.DataEncrypt(email)}', 
                                '{status}', '{date}', '{date}', '{date}', '{user.Username}', '{user.Username}');";

                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(query);

                if (rowsAffected > 0)
                {
                    return Ok($"User {user.Username} Created Successfully");
                }
                else
                {
                    return BadRequest($"Failed To Insert Data For Username {user.Username}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateLastLogin", Name = "UpdateLastLogin")]
        public async Task<IActionResult> UpdateLastLogin([FromQuery] string username)
        {
            try
            {
                string date = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);

                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username Data Is Required.");
                }

                var query = $@"UPDATE `User`
                                SET `LastLogin` = '{date}'
                                WHERE `Username` = '{username}';";

                await _context.Database.ExecuteSqlRawAsync(query);

                return Ok($"User {username} Update Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdatePasswordUser", Name = "UpdatePasswordUser")]
        public async Task<IActionResult> UpdatePasswordUser([FromQuery] string username, [FromQuery] string password, [FromQuery] string email)
        {
            try
            {
                string date = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);

                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username Data Is Required.");
                }

                var query = $@"UPDATE `User`
                                SET `Password` = '{Encryptor.Encryptor.DataEncrypt(password)}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUser` = '{username}'
                                WHERE `Username` = '{username}'
                                    AND `Email` = '{Encryptor.Encryptor.DataEncrypt(email)}';";

                var response =  await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"User {username} Update Successfully");
                }
                else
                {
                    return NotFound($"Username {username} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetUsername", Name = "GetUsername")]
        public async Task<ActionResult<User>> GetUsername([FromQuery] UserFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.Username))
                {
                    return BadRequest("Username Is Required");
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
                                WHERE u.Username = '{filter.Username}';";
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

        [HttpPatch("UpdateUsername", Name = "UpdateUsername")]
        public async Task<IActionResult> UpdateUsername([FromQuery] string username, [FromBody] UserName user)
        {
            try
            {
                string date = DateFormat.DateTimeNow(Longyearpattern, DateTime.Now);

                if (string.IsNullOrEmpty(username) || user == null)
                {
                    return BadRequest("All Data Is Required.");
                }

                string active = user.IsActive ? "Status-001" : "Status-002";

                var query = $@"UPDATE `User`
                                SET `SRSex` = '{user.Sex}',
                                `SRAccess` = '{user.Access}',
                                `SRStatus` = '{active}',
                                `LastUpdateDateTime` = '{date}',
                                `LastUpdateByUser` = '{user.LastUpdateUser}'
                                WHERE `Username` = '{username}';";

                var response = await _context.Database.ExecuteSqlRawAsync(query);

                if (response > 0)
                {
                    return Ok($"User {username} Update Successfully");
                }
                else
                {
                    return NotFound($"Username {username} Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
