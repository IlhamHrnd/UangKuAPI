using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Interface;

namespace UangKuAPI.BusinessObjects.Query
{
    public class AppStandardReferenceItem : BaseQuery, IAppStandardReferenceItem
    {
        public AppStandardReferenceItem(BaseFramework _context) : base(_context)
        {
            
        }

        public EF.AppStandardReferenceItem LoadByPrimaryKey(string standardReferenceId, string itemId)
        {
            if (string.IsNullOrEmpty(standardReferenceId) || string.IsNullOrEmpty(itemId))
                return new EF.AppStandardReferenceItem();

            var query = (from asri in _context.AppStandardReferenceItems
                         where asri.StandardReferenceId.Equals(standardReferenceId) && asri.ItemId.Equals(itemId)
                         select asri).FirstOrDefault();

            if (string.IsNullOrEmpty(query?.ItemId))
                return new EF.AppStandardReferenceItem();

            return query;
        }

        public string GetItemName(string standardReferenceId, string itemId)
        {
            if (string.IsNullOrEmpty(standardReferenceId) || string.IsNullOrEmpty(itemId))
                return string.Empty;

            var asri = LoadByPrimaryKey(standardReferenceId, itemId);
            return asri.ItemId ?? string.Empty;
        }
    }
}
