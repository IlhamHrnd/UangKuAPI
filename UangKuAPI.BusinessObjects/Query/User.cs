using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Interface;

namespace UangKuAPI.BusinessObjects.Query
{
    public class User : BaseQuery, IUser
    {
        private readonly IAppStandardReferenceItem _appStandardReferenceItem;
        public User(BaseFramework _context, IAppStandardReferenceItem appStandardReferenceItem) : base(_context)
        {
            _appStandardReferenceItem = appStandardReferenceItem;
        }

        public EF.User LoadByPrimaryKey(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new EF.User();

            var query = (from u in _context.Users
                         where u.Username.Equals(userId)
                         select u).FirstOrDefault();

            if (string.IsNullOrEmpty(query?.Username))
                return new EF.User();

            return query;
        }

        public bool IsUserAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            var u = LoadByPrimaryKey(userId);
            if (string.IsNullOrEmpty(u.Username))
                return false;

            if (string.IsNullOrEmpty(u.Sraccess))
                return false;

            var asri = _appStandardReferenceItem.LoadByPrimaryKey("Access", u.Sraccess);
            if (string.IsNullOrEmpty(asri.ItemId))
                return false;

            if (asri.ItemId == "Access-01" && asri.ItemName == "Admin")
                return true;

            return false;
        }
    }
}
