using UangKuAPI.BusinessObjects.DataTransfer;

namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IUserReport
    {
        UserReportDto LoadByPrimaryKey(string reportNo);
        bool IsUserHasReport(string personId);
    }
}
