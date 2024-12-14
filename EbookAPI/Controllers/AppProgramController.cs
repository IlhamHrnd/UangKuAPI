using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "AppProgramAPI")]
    [ApiController]
    public class AppProgramController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AppProgramController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetNewProgramID", Name = "GetNewProgramID")]
        public ActionResult<Response<string>> GetNewProgramID([FromQuery] AppProgramFilter filter)
        {
            var data = string.Empty;
            var response = new Response<string>();

            try
            {
                if (string.IsNullOrEmpty(filter.ProgramID))
                {
                    response = new Response<string>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ProgramID"),
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
                    data = $"PRG/{filter.ProgramID}/{transDate}-{formattedNumber}";
                    number++;
                } while (_context.AppPrograms.Any(p => p.ProgramId == data));

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

        [HttpGet("GetAllAppProgram", Name = "GetAllAppProgram")]
        public ActionResult<PageResponse<AppProgram>> GetAllAppProgram([FromQuery] AppProgramFilter filter)
        {
            var pagedData = new List<AppProgram>();
            var response = new PageResponse<List<AppProgram>>(pagedData, 0, 0);

            try
            {
                var apQ = new BusinessObjects.Entity.Generated.AppProgramQuery("apQ");
                var apColl = new BusinessObjects.Entity.Generated.AppProgramCollection();

                if (filter.IsVisible.HasValue)
                    apQ.Where(apQ.IsVisible == filter.IsVisible.Value);

                if (filter.IsUsedBySystem.HasValue)
                    apQ.Where(apQ.IsUsedBySystem == filter.IsUsedBySystem.Value);

                apColl.Load(apQ);
                if (apColl.Count == 0)
                {
                    response = new PageResponse<List<AppProgram>>(pagedData, 0, 0)
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

                foreach (var item in apColl)
                {
                    var ap = new AppProgram
                    {
                        ProgramId = item.ProgramID,
                        ProgramName = item.ProgramName,
                        Note = item.Note,
                        IsProgram = Converter.SbyteToUlong(item.IsProgram),
                        IsProgramAddAble = Converter.SbyteToUlong(item.IsProgramAddAble),
                        IsProgramEditAble = Converter.SbyteToUlong(item.IsProgramEditAble),
                        IsProgramDeleteAble = Converter.SbyteToUlong(item.IsProgramDeleteAble),
                        IsProgramViewAble = Converter.SbyteToUlong(item.IsProgramViewAble),
                        IsProgramApprovalAble = Converter.SbyteToUlong(item.IsProgramApprovalAble),
                        IsProgramUnApprovalAble = Converter.SbyteToUlong(item.IsProgramUnApprovalAble),
                        IsProgramVoidAble = Converter.SbyteToUlong(item.IsProgramVoidAble),
                        IsProgramUnVoidAble = Converter.SbyteToUlong(item.IsProgramUnVoidAble),
                        IsProgramPrintAble = Converter.SbyteToUlong(item.IsProgramPrintAble),
                        IsVisible = Converter.SbyteToUlong(item.IsVisible),
                        LastUpdateDateTime = item.LastUpdateDateTime ?? new DateTime(),
                        LastUpdateByUserId = item.LastUpdateByUserID,
                        IsUsedBySystem = Converter.SbyteToUlong(item.IsUsedBySystem) ?? 0
                    };
                    pagedData.Add(ap);
                }
                var totalRecord = apColl.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

                string? prevPageLink = filter.PageNumber > 1
                    ? Url.Link("GetAllAppProgram", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                    : null;

                string? nextPageLink = filter.PageNumber < totalPages
                    ? Url.Link("GetAllAppProgram", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                    : null;

                response = new PageResponse<List<AppProgram>>(pagedData, filter.PageNumber, filter.PageSize)
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
                response = new PageResponse<List<AppProgram>>(pagedData, 0, 0)
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

        [HttpGet("GetAppProgramID", Name = "GetAppProgramID")]
        public ActionResult<Response<AppProgram>> GetAppProgramID([FromQuery] AppProgramFilter filter)
        {
            var data = new AppProgram();
            var response = new Response<AppProgram>();

            try
            {
                if (string.IsNullOrEmpty(filter.ProgramID))
                {
                    response = new Response<AppProgram>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "ProgramID"),
                        Succeeded = !string.IsNullOrEmpty(data.ProgramId)
                    };
                    return BadRequest(response);
                }

                var ap = new BusinessObjects.Entity.Generated.AppProgram();

                if (!ap.LoadByPrimaryKey(filter.ProgramID))
                {
                    response = new Response<AppProgram>
                    {
                        Data = data,
                        Message = !string.IsNullOrEmpty(ap.ProgramID) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = !string.IsNullOrEmpty(ap.ProgramID)
                    };
                    return NotFound(response);
                }

                data = new AppProgram
                {
                    ProgramId = ap.ProgramID, ProgramName = ap.ProgramName, Note = ap.Note, IsProgram = Converter.SbyteToUlong(ap.IsProgram), IsProgramAddAble = Converter.SbyteToUlong(ap.IsProgramAddAble),
                    IsProgramEditAble = Converter.SbyteToUlong(ap.IsProgramEditAble), IsProgramDeleteAble = Converter.SbyteToUlong(ap.IsProgramDeleteAble), IsProgramViewAble = Converter.SbyteToUlong(ap.IsProgramViewAble),
                    IsProgramApprovalAble = Converter.SbyteToUlong(ap.IsProgramApprovalAble), IsProgramUnApprovalAble = Converter.SbyteToUlong(ap.IsProgramUnApprovalAble), IsProgramVoidAble = Converter.SbyteToUlong(ap.IsProgramVoidAble),
                    IsProgramUnVoidAble = Converter.SbyteToUlong(ap.IsProgramUnVoidAble), IsProgramPrintAble = Converter.SbyteToUlong(ap.IsProgramPrintAble), IsVisible = Converter.SbyteToUlong(ap.IsVisible),
                    LastUpdateDateTime = ap.LastUpdateDateTime ?? new DateTime(), LastUpdateByUserId = ap.LastUpdateByUserID, IsUsedBySystem = Converter.SbyteToUlong(ap.IsUsedBySystem) ?? 0
                };

                response = new Response<AppProgram>
                {
                    Data = data,
                    Message = !string.IsNullOrEmpty(ap.ProgramID) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = !string.IsNullOrEmpty(ap.ProgramID)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<AppProgram>
                {
                    Data = data,
                    Message = $"{(!string.IsNullOrEmpty(data.ProgramId) ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = !string.IsNullOrEmpty(data.ProgramId)
                };
                return BadRequest(response);
            }
        }

        [HttpPost("PostAppProgram", Name = "PostAppProgram")]
        public async Task<IActionResult> PostAppProgram([FromBody] AppProgram appProgram)
        {
            try
            {
                if (appProgram == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "App Program"));

                var data = await _context.AppPrograms
                    .FirstOrDefaultAsync(ap => ap.ProgramId == appProgram.ProgramId);

                if (data != null)
                    return BadRequest(string.Format(AppConstant.AlreadyExistMsg, appProgram.ProgramId));

                var ap = new AppProgram
                {
                    ProgramId = appProgram.ProgramId, ProgramName = appProgram.ProgramName, Note = appProgram.Note, IsProgram = appProgram.IsProgram, IsProgramAddAble = appProgram.IsProgramAddAble,
                    IsProgramEditAble = appProgram.IsProgramEditAble, IsProgramDeleteAble = appProgram.IsProgramDeleteAble, IsProgramViewAble = appProgram.IsProgramViewAble,
                    IsProgramApprovalAble = appProgram.IsProgramApprovalAble, IsProgramUnApprovalAble = appProgram.IsProgramUnApprovalAble, IsProgramVoidAble = appProgram.IsProgramVoidAble,
                    IsProgramUnVoidAble = appProgram.IsProgramUnVoidAble, IsProgramPrintAble = appProgram.IsProgramPrintAble, IsVisible = appProgram.IsVisible, LastUpdateDateTime = DateFormat.DateTimeNow(),
                    LastUpdateByUserId = appProgram.LastUpdateByUserId, IsUsedBySystem = appProgram.IsUsedBySystem
                };
                _context.AppPrograms.Add(ap);
                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.CreatedSuccessMsg, "App Program", appProgram.ProgramId))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Insert", "App Program", appProgram.ProgramId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("PatchAppProgram", Name = "PatchAppProgram")]
        public async Task<IActionResult> PatchAppProgram([FromBody] AppProgram appProgram)
        {
            try
            {
                if (appProgram == null)
                    return BadRequest(string.Format(AppConstant.RequiredMsg, "App Program"));

                var data = await _context.AppPrograms
                    .FirstOrDefaultAsync(ap => ap.ProgramId == appProgram.ProgramId);

                if (data == null)
                    return NotFound(AppConstant.NotFoundMsg);

                data.ProgramName = appProgram.ProgramName;
                data.Note = appProgram.Note;
                data.IsProgram = appProgram.IsProgram;
                data.IsProgramAddAble = appProgram.IsProgramAddAble;
                data.IsProgramEditAble = appProgram.IsProgramEditAble;
                data.IsProgramDeleteAble = appProgram.IsProgramDeleteAble;
                data.IsProgramViewAble = appProgram.IsProgramViewAble;
                data.IsProgramApprovalAble = appProgram.IsProgramApprovalAble;
                data.IsProgramUnApprovalAble = appProgram.IsProgramUnApprovalAble;
                data.IsProgramVoidAble = appProgram.IsProgramVoidAble;
                data.IsProgramUnVoidAble = appProgram.IsProgramUnVoidAble;
                data.IsProgramPrintAble = appProgram.IsProgramPrintAble;
                data.IsVisible = appProgram.IsVisible;
                data.LastUpdateDateTime = DateFormat.DateTimeNow();
                data.LastUpdateByUserId = appProgram.LastUpdateByUserId;
                data.IsUsedBySystem = appProgram.IsUsedBySystem;
                _context.Update(data);

                int rows = await _context.SaveChangesAsync();

                return rows > 0
                    ? Ok(string.Format(AppConstant.UpdateSuccessMsg, appProgram.ProgramName))
                    : BadRequest(string.Format(AppConstant.FailedMsg, "Update", "App Program", appProgram.ProgramName));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
