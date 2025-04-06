using UangKuAPI.BusinessObjects.DataTransfer;

namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IAppStandardReferenceItem
    {
        AppStandardReferenceItemDto LoadByPrimaryKey(string standardReferenceId, string itemId);
        string GetItemName(string standardReferenceId, string itemId);
    }
}
