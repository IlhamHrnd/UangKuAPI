namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IUserReport
    {
        EF.UserReport LoadByPrimaryKey(string reportNo);
        bool IsUserHasReport(string personId);
    }
}
