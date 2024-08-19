using Microsoft.AspNetCore.Mvc;
using UangKuAPI.BusinessObjects.Entity;
using UangKuAPI.BusinessObjects.Filter;
using Models = UangKuAPI.BusinessObjects.Model;
using UangKuAPI.Context;
using System.Data;
using EbookAPI.Wrapper;
using static UangKuAPI.BusinessObjects.Helper.Helper;

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

        [HttpGet("GetAllAppProgram", Name = "GetAllAppProgram")]
        public async Task<ActionResult<Models.AppProgram>> GetAllAppProgram([FromQuery] AppProgramFilter filter)
        {
            var apQ = new AppprogramQuery("apQ");

            apQ.Select(apQ.ProgramID, apQ.ProgramName, apQ.Note, apQ.LastUpdateByUserID, apQ.LastUpdateDateTime,
                "<CASE WHEN apQ.IsProgram = 1 THEN 'true' ELSE 'false' END AS 'IsProgram'>", "<CASE WHEN apQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>",
                "<CASE WHEN apQ.IsProgramAddAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramAddAble'>", "<CASE WHEN apQ.IsProgramEditAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramEditAble'>",
                "<CASE WHEN apQ.IsProgramDeleteAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramDeleteAble'>", "<CASE WHEN apQ.IsProgramViewAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramViewAble'>",
                "<CASE WHEN apQ.IsProgramApprovalAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramApprovalAble'>", "<CASE WHEN apQ.IsProgramUnApprovalAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramUnApprovalAble'>",
                "<CASE WHEN apQ.IsProgramVoidAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramVoidAble'>", "<CASE WHEN apQ.IsProgramUnVoidAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramUnVoidAble'>",
                "<CASE WHEN apQ.IsProgramPrintAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramPrintAble'>", "<CASE WHEN apQ.IsVisible = 1 THEN 'true' ELSE 'false' END AS 'IsVisible'>");
            DataTable dtRecord = apQ.LoadDataTable();

            apQ.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
            DataTable dt = apQ.LoadDataTable();

            if (dt.Rows.Count == 0)
            {
                return NotFound($"Data Not Found");
            }

            var pagedData = new List<Models.AppProgram>();

            foreach (DataRow dr in dt.Rows)
            {
                var ap = new Models.AppProgram
                {
                    ProgramID = (string)dr["ProgramID"],
                    ProgramName = dr["ProgramName"] != DBNull.Value ? (string)dr["ProgramName"] : string.Empty,
                    Note = dr["Note"] != DBNull.Value ? (string)dr["Note"] : string.Empty,
                    LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                    LastUpdateByUserID = dr["LastUpdateByUserID"] != DBNull.Value ? (string)dr["LastUpdateByUserID"] : string.Empty,
                    IsProgram = dr["IsProgram"] != DBNull.Value ? bool.Parse((string)dr["IsProgram"]) : null,
                    IsProgramAddAble = dr["IsProgramAddAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramAddAble"]) : null,
                    IsProgramEditAble = dr["IsProgramEditAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramEditAble"]) : null,
                    IsProgramDeleteAble = dr["IsProgramDeleteAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramDeleteAble"]) : null,
                    IsProgramViewAble = dr["IsProgramViewAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramViewAble"]) : null,
                    IsProgramApprovalAble = dr["IsProgramApprovalAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramApprovalAble"]) : null,
                    IsProgramUnApprovalAble = dr["IsProgramUnApprovalAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramUnApprovalAble"]) : null,
                    IsProgramVoidAble = dr["IsProgramVoidAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramVoidAble"]) : null,
                    IsProgramUnVoidAble = dr["IsProgramUnVoidAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramUnVoidAble"]) : null,
                    IsProgramPrintAble = dr["IsProgramPrintAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramPrintAble"]) : null,
                    IsVisible = dr["IsVisible"] != DBNull.Value ? bool.Parse((string)dr["IsVisible"]) : null,
                    IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"])
                };
                pagedData.Add(ap);
            }
            var totalRecord = dtRecord.Rows.Count;
            var totalPages = (int)Math.Ceiling((double)totalRecord / filter.PageSize);

            string? prevPageLink = filter.PageNumber > 1
                ? Url.Link("GetAllAppProgram", new { PageNumber = filter.PageNumber - 1, filter.PageSize })
                : null;

            string? nextPageLink = filter.PageNumber < totalPages
                ? Url.Link("GetAllAppProgram", new { PageNumber = filter.PageNumber + 1, filter.PageSize })
                : null;

            var response = new PageResponse<List<Models.AppProgram>>(pagedData, filter.PageNumber, filter.PageSize)
            {
                TotalPages = totalPages,
                TotalRecords = totalRecord,
                PrevPageLink = prevPageLink,
                NextPageLink = nextPageLink
            };

            return Ok(response);
        }

        [HttpGet("GetAllAppProgramWithNoPageFilter", Name = "GetAllAppProgramWithNoPageFilter")]
        public async Task<ActionResult<Models.AppProgram>> GetAllAppProgramWithNoPageFilter()
        {
            var apQ = new AppprogramQuery("apQ");

            apQ.Select(apQ.ProgramID, apQ.ProgramName, apQ.Note, apQ.LastUpdateByUserID, apQ.LastUpdateDateTime,
                "<CASE WHEN apQ.IsProgram = 1 THEN 'true' ELSE 'false' END AS 'IsProgram'>", "<CASE WHEN apQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>",
                "<CASE WHEN apQ.IsProgramAddAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramAddAble'>", "<CASE WHEN apQ.IsProgramEditAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramEditAble'>",
                "<CASE WHEN apQ.IsProgramDeleteAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramDeleteAble'>", "<CASE WHEN apQ.IsProgramViewAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramViewAble'>",
                "<CASE WHEN apQ.IsProgramApprovalAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramApprovalAble'>", "<CASE WHEN apQ.IsProgramUnApprovalAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramUnApprovalAble'>",
                "<CASE WHEN apQ.IsProgramVoidAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramVoidAble'>", "<CASE WHEN apQ.IsProgramUnVoidAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramUnVoidAble'>",
                "<CASE WHEN apQ.IsProgramPrintAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramPrintAble'>", "<CASE WHEN apQ.IsVisible = 1 THEN 'true' ELSE 'false' END AS 'IsVisible'>");
            DataTable dt = apQ.LoadDataTable();

            if (dt.Rows.Count == 0)
            {
                return NotFound($"Data Not Found");
            }

            var response = new List<Models.AppProgram>();

            foreach (DataRow dr in dt.Rows)
            {
                var ap = new Models.AppProgram
                {
                    ProgramID = (string)dr["ProgramID"],
                    ProgramName = dr["ProgramName"] != DBNull.Value ? (string)dr["ProgramName"] : string.Empty,
                    Note = dr["Note"] != DBNull.Value ? (string)dr["Note"] : string.Empty,
                    LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                    LastUpdateByUserID = dr["LastUpdateByUserID"] != DBNull.Value ? (string)dr["LastUpdateByUserID"] : string.Empty,
                    IsProgram = dr["IsProgram"] != DBNull.Value ? bool.Parse((string)dr["IsProgram"]) : null,
                    IsProgramAddAble = dr["IsProgramAddAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramAddAble"]) : null,
                    IsProgramEditAble = dr["IsProgramEditAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramEditAble"]) : null,
                    IsProgramDeleteAble = dr["IsProgramDeleteAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramDeleteAble"]) : null,
                    IsProgramViewAble = dr["IsProgramViewAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramViewAble"]) : null,
                    IsProgramApprovalAble = dr["IsProgramApprovalAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramApprovalAble"]) : null,
                    IsProgramUnApprovalAble = dr["IsProgramUnApprovalAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramUnApprovalAble"]) : null,
                    IsProgramVoidAble = dr["IsProgramVoidAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramVoidAble"]) : null,
                    IsProgramUnVoidAble = dr["IsProgramUnVoidAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramUnVoidAble"]) : null,
                    IsProgramPrintAble = dr["IsProgramPrintAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramPrintAble"]) : null,
                    IsVisible = dr["IsVisible"] != DBNull.Value ? bool.Parse((string)dr["IsVisible"]) : null,
                    IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"])
                };
                response.Add(ap);
            }

            return Ok(response);
        }

        [HttpGet("GetAppProgramID", Name = "GetAppProgramID")]
        public async Task<ActionResult<Models.AppProgram>> GetAppProgramID([FromQuery] AppProgramFilter filter)
        {
            if (string.IsNullOrEmpty(filter.ProgramID))
            {
                return BadRequest($"ProgramID Is Required");
            }

            var apQ = new AppprogramQuery("apQ");

            apQ.Select(apQ.ProgramID, apQ.ProgramName, apQ.Note, apQ.LastUpdateByUserID, apQ.LastUpdateDateTime,
                "<CASE WHEN apQ.IsProgram = 1 THEN 'true' ELSE 'false' END AS 'IsProgram'>", "<CASE WHEN apQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>",
                "<CASE WHEN apQ.IsProgramAddAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramAddAble'>", "<CASE WHEN apQ.IsProgramEditAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramEditAble'>",
                "<CASE WHEN apQ.IsProgramDeleteAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramDeleteAble'>", "<CASE WHEN apQ.IsProgramViewAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramViewAble'>",
                "<CASE WHEN apQ.IsProgramApprovalAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramApprovalAble'>", "<CASE WHEN apQ.IsProgramUnApprovalAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramUnApprovalAble'>",
                "<CASE WHEN apQ.IsProgramVoidAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramVoidAble'>", "<CASE WHEN apQ.IsProgramUnVoidAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramUnVoidAble'>",
                "<CASE WHEN apQ.IsProgramPrintAble = 1 THEN 'true' ELSE 'false' END AS 'IsProgramPrintAble'>", "<CASE WHEN apQ.IsVisible = 1 THEN 'true' ELSE 'false' END AS 'IsVisible'>")
                .Where(apQ.ProgramID == filter.ProgramID);
            DataTable dt = apQ.LoadDataTable();

            if (dt.Rows.Count == 0)
            {
                return NotFound($"Data Not Found");
            }

            var response = new List<Models.AppProgram>();

            foreach (DataRow dr in dt.Rows)
            {
                var ap = new Models.AppProgram
                {
                    ProgramID = (string)dr["ProgramID"],
                    ProgramName = dr["ProgramName"] != DBNull.Value ? (string)dr["ProgramName"] : string.Empty,
                    Note = dr["Note"] != DBNull.Value ? (string)dr["Note"] : string.Empty,
                    LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                    LastUpdateByUserID = dr["LastUpdateByUserID"] != DBNull.Value ? (string)dr["LastUpdateByUserID"] : string.Empty,
                    IsProgram = dr["IsProgram"] != DBNull.Value ? bool.Parse((string)dr["IsProgram"]) : null,
                    IsProgramAddAble = dr["IsProgramAddAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramAddAble"]) : null,
                    IsProgramEditAble = dr["IsProgramEditAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramEditAble"]) : null,                                                                                     
                    IsProgramDeleteAble = dr["IsProgramDeleteAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramDeleteAble"]) : null,
                    IsProgramViewAble = dr["IsProgramViewAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramViewAble"]) : null,
                    IsProgramApprovalAble = dr["IsProgramApprovalAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramApprovalAble"]) : null,
                    IsProgramUnApprovalAble = dr["IsProgramUnApprovalAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramUnApprovalAble"]) : null,
                    IsProgramVoidAble = dr["IsProgramVoidAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramVoidAble"]) : null,
                    IsProgramUnVoidAble = dr["IsProgramUnVoidAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramUnVoidAble"]) : null,
                    IsProgramPrintAble = dr["IsProgramPrintAble"] != DBNull.Value ? bool.Parse((string)dr["IsProgramPrintAble"]) : null,
                    IsVisible = dr["IsVisible"] != DBNull.Value ? bool.Parse((string)dr["IsVisible"]) : null,
                    IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"])
                };
                response.Add(ap);
            }

            return Ok(response);
        }

        [HttpGet("GetNewProgramID", Name = "GetNewProgramID")]
        public IActionResult GetNewProgramID([FromQuery] AppProgramFilter filter)
        {
            try
            {
                var requiredFields = new List<(int Value, string Name)>
                {
                    (filter.FirstID, nameof(filter.FirstID)),
                    (filter.MiddleID, nameof(filter.MiddleID)),
                    (filter.LastID, nameof(filter.LastID))
                };

                foreach (var (value, name) in requiredFields)
                {
                    if (value == 0)
                    {
                        return BadRequest($"{name} Is Required");
                    }
                }

                string firstFormat = NumberingFormat(filter.FirstID, "D2");
                string secondFormat = NumberingFormat(filter.MiddleID, "D2");
                string lastFormat = NumberingFormat(filter.LastID, "D3");
                string programID = $"{firstFormat}.{secondFormat}.{lastFormat}";

                while (_context.AppPrograms.Any(ap => ap.ProgramID == programID))
                {
                    filter.LastID++;
                    lastFormat = NumberingFormat(filter.LastID, "D3");
                    programID = $"{firstFormat}.{secondFormat}.{lastFormat}";
                }

                filter.LastID++;
                return Ok(programID);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
