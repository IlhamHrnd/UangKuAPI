using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.DataTransfer;
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

        public UserDto LoadByPrimaryKey(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new UserDto
                {
                    user = new EntityFramework.Models.User()
                };

            var query = (from u in _context.Users
                         where u.Username.Equals(userId)
                         select u).FirstOrDefault();

            if (string.IsNullOrEmpty(query?.Username))
                return new UserDto
                {
                    user = new EntityFramework.Models.User()
                };

            return new UserDto
            {
                user = query
            };
        }

        public bool IsUserAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            var u = LoadByPrimaryKey(userId);
            if (u?.user?.Username == null)
                return false;

            if (string.IsNullOrEmpty(u.user.Sraccess))
                return false;

            var asri = _appStandardReferenceItem.LoadByPrimaryKey("Access", u.user.Sraccess);
            if (asri?.appStandardReferenceItem?.ItemId == null)
                return false;

            if (asri.appStandardReferenceItem.ItemId == "Access-01" && asri.appStandardReferenceItem.ItemName == "Admin")
                return true;

            return false;
        }
    }
}
