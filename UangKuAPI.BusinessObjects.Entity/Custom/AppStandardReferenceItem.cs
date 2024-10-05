using UangKuAPI.BusinessObjects.Entity.Generated;

namespace UangKuAPI.BusinessObjects.Entity.Custom
{
    public class AppStandardReferenceItem
    {
        public static string GetItemName(string? referenceID, string? itemID)
        {
            if (string.IsNullOrEmpty(referenceID) || string.IsNullOrEmpty(itemID))
                return string.Empty;
            
            var asri = new Appstandardreferenceitem();
            if (!asri.LoadByPrimaryKey(referenceID, itemID))
                return string.Empty;
            else
                return asri.ItemName;
        }
    }
}