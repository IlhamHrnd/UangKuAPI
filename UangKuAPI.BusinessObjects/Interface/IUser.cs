namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IUser
    {
        EF.User LoadByPrimaryKey(string userId);
        bool IsUserAdmin(string userId);
    }
}
