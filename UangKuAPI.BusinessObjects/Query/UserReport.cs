using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Interface;

namespace UangKuAPI.BusinessObjects.Query
{
    public class UserReport : BaseQuery, IUserReport
    {
        public UserReport(BaseFramework _context) : base(_context)
        {
            
        }

        public EF.UserReport LoadByPrimaryKey(string reportNo)
        {
            if (string.IsNullOrEmpty(reportNo))
                return new EF.UserReport();

            var query = (from ur in _context.UserReports
                         where ur.ReportNo.Equals(reportNo)
                         select ur).FirstOrDefault();

            if (string.IsNullOrEmpty(query?.ReportNo))
                return new EF.UserReport();

            return query;
        }

        public bool IsUserHasReport(string personId)
        {
            if (string.IsNullOrEmpty(personId))
                return false;

            var query = (from ur in _context.UserReports
                         where ur.PersonId.Equals(personId)
                         select ur.ReportNo).ToList();

            return query.Count > 0;
        }
    }
}
