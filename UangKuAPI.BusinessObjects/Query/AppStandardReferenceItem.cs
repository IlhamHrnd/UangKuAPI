using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.DataTransfer;
using UangKuAPI.BusinessObjects.Interface;

namespace UangKuAPI.BusinessObjects.Query
{
    public class AppStandardReferenceItem : BaseQuery, IAppStandardReferenceItem
    {
        public AppStandardReferenceItem(BaseFramework _context) : base(_context)
        {
            
        }

        public AppStandardReferenceItemDto LoadByPrimaryKey(string standardReferenceId, string itemId)
        {
            if (string.IsNullOrEmpty(standardReferenceId) || string.IsNullOrEmpty(itemId))
                return new AppStandardReferenceItemDto
                {
                    appStandardReferenceItem = new EntityFramework.Models.AppStandardReferenceItem()
                };

            var query = (from asri in _context.AppStandardReferenceItems
                         where asri.StandardReferenceId.Equals(standardReferenceId) && asri.ItemId.Equals(itemId)
                         select asri).FirstOrDefault();

            if (string.IsNullOrEmpty(query?.ItemId))
                return new AppStandardReferenceItemDto
                {
                    appStandardReferenceItem = new EntityFramework.Models.AppStandardReferenceItem()
                };

            return new AppStandardReferenceItemDto
            {
                appStandardReferenceItem = query
            };
        }

        public string GetItemName(string standardReferenceId, string itemId)
        {
            if (string.IsNullOrEmpty(standardReferenceId) || string.IsNullOrEmpty(itemId))
                return string.Empty;

            var asri = LoadByPrimaryKey(standardReferenceId, itemId);
            return asri?.appStandardReferenceItem?.ItemId ?? string.Empty;
        }
    }
}
