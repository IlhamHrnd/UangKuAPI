using UangKuAPI.BusinessObjects.DataTransfer;

namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IUser
    {
        UserDto LoadByPrimaryKey(string userId);
        bool IsUserAdmin(string userId);
    }
}
