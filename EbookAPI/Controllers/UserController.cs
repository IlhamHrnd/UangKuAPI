using UangKuAPI.Context;
using EbookAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.Helper;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Entity;
using System.Data;
using Models = UangKuAPI.BusinessObjects.Model;

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
        public async Task<ActionResult<Models.User>> GetAllUser([FromQuery] UserFilter filter)
        {
            try
            {
                var uQ = new UserQuery("uQ");
                var sexQ = new AppstandardreferenceitemQuery("sexQ");
                var accessQ = new AppstandardreferenceitemQuery("accessQ");
                var statusQ = new AppstandardreferenceitemQuery("statusQ");

                uQ.Select(uQ.Username, uQ.ActiveDate, uQ.LastLogin, uQ.LastUpdateDateTime,
                    uQ.LastUpdateByUser, uQ.PersonID, sexQ.ItemName.As("SexName"), accessQ.ItemName.As("AccessName"),
                    statusQ.ItemName.As("StatusName"))
                    .InnerJoin(sexQ).On(sexQ.StandardReferenceID == "Sex" && sexQ.ItemID == uQ.SRSex)
                    .InnerJoin(accessQ).On(accessQ.StandardReferenceID == "Access" && accessQ.ItemID == uQ.SRAccess)
                    .InnerJoin(statusQ).On(statusQ.StandardReferenceID == "Status" && statusQ.ItemID == uQ.SRStatus)
                    .OrderBy(uQ.Username.Ascending);
                DataTable dtRecord = uQ.LoadDataTable();

                uQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = uQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"Data Not Found");
                }

                var pagedData = new List<Models.User>();

                foreach (DataRow dr in dt.Rows)
                {
                    var userItem = new Models.User
                    {
                        Username = (string)dr["Username"],
                        ActiveDate = (DateTime)dr["ActiveDate"],
                        LastLogin = (DateTime)dr["LastLogin"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUser = (string)dr["LastUpdateByUser"],
                        PersonID = (string)dr["PersonID"],
                        SexName = (string)dr["SexName"],
                        AccessName = (string)dr["AccessName"],
                        StatusName = (string)dr["StatusName"]
                    };
                    pagedData.Add(userItem);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllUser", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllUser", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                var response = new PageResponse<List<Models.User>>(pagedData, filter.PageNumber, filter.PageSize)
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
        public async Task<ActionResult<Models.User>> GetLoginUserName([FromQuery] UserFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.Username) || string.IsNullOrEmpty(filter.Password))
                {
                    return BadRequest("Username Or Password Is Required");
                }

                var uQ = new UserQuery("uQ");
                var sexQ = new AppstandardreferenceitemQuery("sexQ");
                var accessQ = new AppstandardreferenceitemQuery("accessQ");
                var statusQ = new AppstandardreferenceitemQuery("statusQ");

                uQ.Select(uQ.Username, uQ.ActiveDate, uQ.LastLogin, uQ.LastUpdateDateTime,
                    uQ.LastUpdateByUser, uQ.PersonID, sexQ.ItemName.As("SexName"), accessQ.ItemName.As("AccessName"),
                    statusQ.ItemName.As("StatusName"))
                    .InnerJoin(sexQ).On(sexQ.StandardReferenceID == "Sex" && sexQ.ItemID == uQ.SRSex)
                    .InnerJoin(accessQ).On(accessQ.StandardReferenceID == "Access" && accessQ.ItemID == uQ.SRAccess)
                    .InnerJoin(statusQ).On(statusQ.StandardReferenceID == "Status" && statusQ.ItemID == uQ.SRStatus)
                    .OrderBy(uQ.Username.Ascending)
                    .Where(uQ.Username == filter.Username && uQ.Password == Encryptor.Encryptor.DataEncrypt(filter.Password));
                DataTable dt = uQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"{filter.Username} Not Found");
                }

                var user = new List<Models.User>();

                foreach (DataRow dr in dt.Rows)
                {
                    var userItem = new Models.User
                    {
                        Username = (string)dr["Username"],
                        ActiveDate = (DateTime)dr["ActiveDate"],
                        LastLogin = (DateTime)dr["LastLogin"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUser = (string)dr["LastUpdateByUser"],
                        PersonID = (string)dr["PersonID"],
                        SexName = (string)dr["SexName"],
                        AccessName = (string)dr["AccessName"],
                        StatusName = (string)dr["StatusName"]
                    };
                    user.Add(userItem);
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpPost("CreateUsername", Name = "CreateUsername")]
        public async Task<IActionResult> CreateUsername([FromBody] Models.User user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
                {
                    return BadRequest("User Data, Password, And Email Are Required.");
                }

                var addUser = await _context.Users
                    .Where(u => u.Username == user.Username)
                    .ToListAsync();

                if (addUser.Any())
                {
                    return BadRequest($"{user.Username} Already Exist");
                }

                var newUser = new Models.User
                {
                    Username = user.Username, Password = Encryptor.Encryptor.DataEncrypt(user.Password),
                    SexName = user.SexName, AccessName = user.AccessName, Email = Encryptor.Encryptor.DataEncrypt(user.Email),
                    StatusName = user.StatusName, ActiveDate = DateFormat.DateTimeNow(), LastLogin = DateFormat.DateTimeNow(),
                    LastUpdateDateTime = DateFormat.DateTimeNow(), LastUpdateByUser = user.Username, PersonID = user.PersonID
                };
                _context.Users.Add(newUser);

                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Ok($"User {user.Username} Created Successfully")
                    : BadRequest($"Failed To Insert Data For Username {user.Username}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdateLastLogin", Name = "UpdateLastLogin")]
        public async Task<IActionResult> UpdateLastLogin([FromQuery] UserFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.Username))
                {
                    return BadRequest("Username Data Is Required.");
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == filter.Username);

                if (user == null)
                {
                    return BadRequest($"{filter.Username} Not Found");
                }

                user.LastLogin = DateFormat.DateTimeNow();
                _context.Update(user);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{filter.Username} Updated Successfully")
                    : BadRequest($"Failed To Update Data For Username {filter.Username}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("UpdatePasswordUser", Name = "UpdatePasswordUser")]
        public async Task<IActionResult> UpdatePasswordUser([FromQuery] UserFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.Username))
                {
                    return BadRequest("Username Data Is Required.");
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == filter.Username && u.Email == Encryptor.Encryptor.DataEncrypt(filter.Email));

                if (user == null)
                {
                    return BadRequest($"{filter.Username} Not Found");
                }

                user.Password = Encryptor.Encryptor.DataEncrypt(filter.Password);
                _context.Update(user);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{filter.Username} Updated Successfully")
                    : BadRequest($"Failed To Update Data For Username {filter.Username}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetUsername", Name = "GetUsername")]
        public async Task<ActionResult<Models.User>> GetUsername([FromQuery] UserFilter filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.Username))
                {
                    return BadRequest("Username Is Required");
                }

                var uQ = new UserQuery("uQ");
                var sexQ = new AppstandardreferenceitemQuery("sexQ");
                var accessQ = new AppstandardreferenceitemQuery("accessQ");
                var statusQ = new AppstandardreferenceitemQuery("statusQ");

                uQ.Select(uQ.Username, uQ.ActiveDate, uQ.LastLogin, uQ.LastUpdateDateTime,
                    uQ.LastUpdateByUser, uQ.PersonID, sexQ.ItemName.As("SexName"), accessQ.ItemName.As("AccessName"),
                    statusQ.ItemName.As("StatusName"))
                    .InnerJoin(sexQ).On(sexQ.StandardReferenceID == "Sex" && sexQ.ItemID == uQ.SRSex)
                    .InnerJoin(accessQ).On(accessQ.StandardReferenceID == "Access" && accessQ.ItemID == uQ.SRAccess)
                    .InnerJoin(statusQ).On(statusQ.StandardReferenceID == "Status" && statusQ.ItemID == uQ.SRStatus)
                    .OrderBy(uQ.Username.Ascending)
                    .Where(uQ.Username == filter.Username);
                var dt = uQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    return NotFound($"{filter.Username} Not Found");
                }

                var user = new List<Models.User>();

                foreach (DataRow dr in dt.Rows)
                {
                    var userItem = new Models.User
                    {
                        Username = (string)dr["Username"],
                        ActiveDate = (DateTime)dr["ActiveDate"],
                        LastLogin = (DateTime)dr["LastLogin"],
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUser = (string)dr["LastUpdateByUser"],
                        PersonID = (string)dr["PersonID"],
                        SexName = (string)dr["SexName"],
                        AccessName = (string)dr["AccessName"],
                        StatusName = (string)dr["StatusName"]
                    };
                    user.Add(userItem);
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }

        [HttpPatch("UpdateUsername", Name = "UpdateUsername")]
        public async Task<IActionResult> UpdateUsername([FromBody] Models.User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Username) || user == null)
                {
                    return BadRequest("All Data Is Required.");
                }

                var updateUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username);

                if (updateUser == null)
                {
                    return BadRequest($"{user.Username} Not Found");
                }

                updateUser.SexName = user.SexName;
                updateUser.StatusName = user.StatusName;
                updateUser.LastUpdateDateTime = DateFormat.DateTimeNow();
                updateUser.LastUpdateByUser = user.LastUpdateByUser;
                _context.Update(updateUser);

                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0
                    ? Ok($"{user.Username} Updated Successfully")
                    : BadRequest($"Failed To Update Data For Username {user.Username}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
