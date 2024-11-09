using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserAPI")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Parameter _param;
        public UserController(AppDbContext context, IOptions<Parameter> param)
        {
            _context = context;
            _param = param.Value;
        }

        [HttpGet("GetAllUser", Name = "GetAllUser")]
        public ActionResult<PageResponse<User>> GetAllUser([FromQuery] UserFilter filter)
        {
            var pagedData = new List<User>();
            var response = new PageResponse<List<User>>(pagedData, 0, 0);

            try
            {
                var uQ = new BusinessObjects.Entity.Generated.UserQuery("uQ");
                var sexQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("sexQ");
                var accessQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("accessQ");
                var statusQ = new BusinessObjects.Entity.Generated.AppstandardreferenceitemQuery("statusQ");

                uQ.Select(uQ.Username)
                    .InnerJoin(sexQ).On(sexQ.StandardReferenceID == "Sex" && sexQ.ItemID == uQ.SRSex)
                    .InnerJoin(accessQ).On(accessQ.StandardReferenceID == "Access" && accessQ.ItemID == uQ.SRAccess)
                    .InnerJoin(statusQ).On(statusQ.StandardReferenceID == "Status" && statusQ.ItemID == uQ.SRStatus);
                DataTable dtRecord = uQ.LoadDataTable();

                uQ.Select(uQ.ActiveDate, uQ.LastLogin, uQ.LastUpdateDateTime, uQ.LastUpdateByUser, uQ.PersonID, 
                    sexQ.ItemName.As("SexName"), accessQ.ItemName.As("AccessName"), statusQ.ItemName.As("StatusName"))
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
                DataTable dt = uQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new PageResponse<List<User>>(pagedData, 0, 0)
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
                    var u = new User
                    {
                        Username = dr["Username"] != DBNull.Value ? (string)dr["Username"] : string.Empty,
                        Srsex = dr["SexName"] != DBNull.Value ? (string)dr["SexName"] : string.Empty,
                        Sraccess = dr["AccessName"] != DBNull.Value ? (string)dr["AccessName"] : string.Empty,
                        Srstatus = dr["StatusName"] != DBNull.Value ? (string)dr["StatusName"] : string.Empty,
                        ActiveDate = dr["ActiveDate"] != DBNull.Value ? (DateTime)dr["ActiveDate"] : new DateTime(),
                        LastLogin = dr["LastLogin"] != DBNull.Value ? (DateTime)dr["LastLogin"] : new DateTime(),
                        LastUpdateDateTime = dr["LastUpdateDateTime"] != DBNull.Value ? (DateTime)dr["LastUpdateDateTime"] : new DateTime(),
                        LastUpdateByUser = dr["LastUpdateByUser"] != DBNull.Value ? (string)dr["LastUpdateByUser"] : string.Empty,
                        PersonId = dr["PersonID"] != DBNull.Value ? (string)dr["PersonID"] : string.Empty
                    };
                    pagedData.Add(u);
                }
                var totalRecord = dtRecord.Rows.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllUser", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllUser", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<User>>(pagedData, filter.PageNumber, filter.PageSize)
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
                response = new PageResponse<List<User>>(pagedData, 0, 0)
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

        [HttpGet("GetLoginUserName", Name = "GetLoginUserName")]
        public ActionResult<Response<User>> GetLoginUserName([FromQuery] UserFilter filter)
        {
            var data = new User();
            var response = new Response<User>();

            try
            {
                if (string.IsNullOrEmpty(filter.Username))
                {
                    response = new Response<User>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Username"),
                        Succeeded = !string.IsNullOrEmpty(filter.Username)
                    };
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(filter.Password))
                {
                    response = new Response<User>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Password"),
                        Succeeded = !string.IsNullOrEmpty(filter.Password)
                    };
                    return BadRequest(response);
                }

                var u = new BusinessObjects.Entity.Generated.User();

                if (!u.LoadByPrimaryKey(filter.Username, Encryptor.DataEncrypt(filter.Password, _param.Key01)))
                {
                    response = new Response<User>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(data.Username) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(data.Username)
                    };
                    return NotFound(response);
                }

                var SexName = !string.IsNullOrEmpty(u.SRSex) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("Sex", u.SRSex) : string.Empty;
                var AccessName = !string.IsNullOrEmpty(u.SRAccess) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("Access", u.SRAccess) : string.Empty;
                var StatusName = !string.IsNullOrEmpty(u.SRStatus) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("Status", u.SRStatus) : string.Empty;

                data = new User
                {
                    Username = u.Username,
                    Srsex = SexName,
                    Sraccess = AccessName,
                    Email = Encryptor.DataDecrypt(u.Email, _param.Key01),
                    Srstatus = StatusName,
                    ActiveDate = u.ActiveDate ?? new DateTime(),
                    LastLogin = u.LastLogin ?? new DateTime(),
                    LastUpdateDateTime = u.LastUpdateDateTime ?? new DateTime(),
                    LastUpdateByUser = u.LastUpdateByUser,
                    PersonId = u.PersonID
                };

                response = new Response<User>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data.Username) ? AppConstant.NotFoundMsg : AppConstant.FoundMsg,
                    Succeeded = !string.IsNullOrEmpty(data.Username)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<User>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.Username) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.Username)
                };
                return BadRequest(response);
            }
        }

        [HttpPost("CreateUsername", Name = "CreateUsername")]
        public async Task<IActionResult> CreateUsername([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "User"));
                
                if (string.IsNullOrEmpty(user.Username))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Username"));
                
                if (string.IsNullOrEmpty(user.Password))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Password"));

                var data = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, user.Username));

                var u = new User
                {
                    Username = user.Username, Password = Encryptor.DataEncrypt(user.Password, _param.Key01), Srsex = user.Srsex,
                    Sraccess = user.Sraccess, Email = Encryptor.DataEncrypt(user.Email, _param.Key01), Srstatus = user.Srstatus,
                    ActiveDate = DateFormat.DateTimeNow(), LastLogin = DateFormat.DateTimeNow(), LastUpdateDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUser = _param.User, PersonId = user.PersonId
                };
                _context.Users.Add(u);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "User", user.Username))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "User", user.Username));
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
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Username"));

                var data = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == filter.Username);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.LastLogin = DateFormat.DateTimeNow();
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, filter.Username))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "Username", filter.Username));
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
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Username"));

                if (string.IsNullOrEmpty(filter.Password))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Password"));

                if (string.IsNullOrEmpty(filter.Email))
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "Email"));

                var data = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == filter.Username && u.Email == Encryptor.DataEncrypt(filter.Email, _param.Key01));
                
                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);
                
                data.Password = Encryptor.DataEncrypt(filter.Password, _param.Key01);
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUser = filter.Username;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, filter.Username))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "User", filter.Username));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetUsername", Name = "GetUsername")]
        public ActionResult<Response<User>> GetUsername([FromQuery] UserFilter filter)
        {
            var data = new User();
            var response = new Response<User>();

            try
            {
                if (string.IsNullOrEmpty(filter.Username))
                {
                    response = new Response<User>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Username"),
                        Succeeded = !string.IsNullOrEmpty(filter.Username)
                    };
                    return BadRequest(response);
                }

                var u = new BusinessObjects.Entity.Generated.User();

                if (!u.LoadByPrimaryKey(filter.Username))
                {
                    response = new Response<User>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(data.Username) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(data.Username)
                    };
                    return NotFound(response);
                }

                var SexName = !string.IsNullOrEmpty(u.SRSex) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("Sex", u.SRSex) : string.Empty;
                var AccessName = !string.IsNullOrEmpty(u.SRAccess) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("Access", u.SRAccess) : string.Empty;
                var StatusName = !string.IsNullOrEmpty(u.SRStatus) ? BusinessObjects.Entity.Custom.AppStandardReferenceItem.GetItemName("Status", u.SRStatus) : string.Empty;

                data = new User
                {
                    Username = u.Username,
                    Srsex = SexName,
                    Sraccess = AccessName,
                    Email = Encryptor.DataDecrypt(u.Email, _param.Key01),
                    Srstatus = StatusName,
                    ActiveDate = u.ActiveDate ?? new DateTime(),
                    LastLogin = u.LastLogin ?? new DateTime(),
                    LastUpdateDateTime = u.LastUpdateDateTime ?? new DateTime(),
                    LastUpdateByUser = u.LastUpdateByUser,
                    PersonId = u.PersonID
                };

                response = new Response<User>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(data.Username) ? AppConstant.NotFoundMsg : AppConstant.FoundMsg,
                    Succeeded = !string.IsNullOrEmpty(data.Username)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<User>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.Username) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.Username)
                };
                return BadRequest(response);
            }
        }

        [HttpPatch("UpdateUsername", Name = "UpdateUsername")]
        public async Task<IActionResult> UpdateUsername([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "User"));

                var data = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username);
                
                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);
                
                data.Srsex = user.Srsex;
                data.Srstatus = user.Srstatus;
                data.Sraccess = user.Sraccess;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUser = user.LastUpdateByUser;
                _context.Update(data);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, user.Username))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "User", user.Username));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}