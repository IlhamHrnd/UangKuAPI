using Microsoft.AspNetCore.Mvc;
using System.Data;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Entity.Generated;
using UangKuAPI.BusinessObjects.Filter;
using UangKuAPI.BusinessObjects.Models;
using UangKuAPI.BusinessObjects.Response;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "AppStandardReferenceItemAPI")]
    [ApiController]
    public class AppStandardReferenceItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppStandardReferenceItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllReferenceItemID", Name = "GetAllReferenceItemID")]
        public ActionResult<Response<List<AppStandardReferenceItem>>> GetAllReferenceItemID([FromQuery] AppStandardRerefenceItemFilter filter)
        {
            var data = new List<AppStandardReferenceItem>();
            var response = new Response<List<AppStandardReferenceItem>>();

            try
            {
                if (string.IsNullOrEmpty(filter.StandardReferenceID))
                {
                    response = new Response<List<AppStandardReferenceItem>>
                    {
                        Data = data,
                        Message = string.Format(AppConstant.RequiredMsg, "Standard ReferenceID"),
                        Succeeded = !string.IsNullOrEmpty(filter.StandardReferenceID)
                    };
                    return BadRequest(response);
                }

                var asriQ = new AppstandardreferenceitemQuery("asriQ");

                asriQ.Select(asriQ.StandardReferenceID, asriQ.ItemID, asriQ.Note, asriQ.LastUpdateDateTime, asriQ.LastUpdateByUserID,
                    asriQ.ItemIcon, asriQ.ItemName,
                    "<CASE WHEN asriQ.IsUsedBySystem = 1 THEN 'true' ELSE 'false' END AS 'IsUsedBySystem'>",
                    "<CASE WHEN asriQ.IsActive = 1 THEN 'true' ELSE 'false' END AS 'IsActive'>")
                    .Where(asriQ.StandardReferenceID == filter.StandardReferenceID)
                    .OrderBy(asriQ.ItemName.Ascending);

                if (filter.IsActive.HasValue)
                    asriQ.Where(asriQ.IsActive == filter.IsActive);

                if (filter.IsUsedBySystem.HasValue)
                    asriQ.Where(asriQ.IsUsedBySystem == filter.IsUsedBySystem);
                var dt = asriQ.LoadDataTable();

                if (dt.Rows.Count == 0)
                {
                    response = new Response<List<AppStandardReferenceItem>>
                    {
                        Data = data,
                        Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                        Succeeded = data.Count > 0
                    };
                    return NotFound(response);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var asri = new AppStandardReferenceItem
                    {
                        StandardReferenceId = (string)dr["StandardReferenceID"],
                        ItemId = (string)dr["ItemID"],
                        ItemName = dr["ItemName"] != DBNull.Value ? (string)dr["ItemName"] : string.Empty,
                        Note = (string)dr["Note"],
                        IsUsedBySystem = bool.Parse((string)dr["IsUsedBySystem"]),
                        IsActive = bool.Parse((string)dr["IsActive"]),
                        LastUpdateDateTime = (DateTime)dr["LastUpdateDateTime"],
                        LastUpdateByUserId = (string)dr["LastUpdateByUserID"],
                        ItemIcon = dr["ItemIcon"] != DBNull.Value ? (byte[])dr["ItemIcon"] : null
                    };
                    data.Add(asri);
                }

                response = new Response<List<AppStandardReferenceItem>>
                {
                    Data = data,
                    Message = data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg,
                    Succeeded = data.Count > 0
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<List<AppStandardReferenceItem>>
                {
                    Data = data,
                    Message = $"{(data.Count > 0 ? AppConstant.FoundMsg : AppConstant.NotFoundMsg)} - {e.Message}",
                    Succeeded = data.Count > 0
                };
                return BadRequest(response);
            }
        }
    }
}
