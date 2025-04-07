namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IAppStandardReferenceItem
    {
        EF.AppStandardReferenceItem LoadByPrimaryKey(string standardReferenceId, string itemId);
        string GetItemName(string standardReferenceId, string itemId);
    }
}
