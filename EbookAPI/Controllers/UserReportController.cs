using Microsoft.AspNetCore.Mvc;
using UangKuAPI.BusinessObjects.Base;

namespace UangKuAPI.Controllers
{
    [Route("[controller]", Name = "UserReportAPI")]
    [ApiController]
    public class UserReportController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserReportController(AppDbContext context)
        {
            _context = context;
        }
    }
}