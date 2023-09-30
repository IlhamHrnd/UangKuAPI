namespace UangKuAPI.Filter
{
    public class AppStandardReferenceFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public AppStandardReferenceFilter()
        {
            PageNumber = 1;
            PageSize = 25;
        }

        public AppStandardReferenceFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 25 ? 25 : pageSize;
        }
    }

    public class AppStandardReferenceIDFilter
    {
        public string? ReferenceID { get; set; }
        public AppStandardReferenceIDFilter()
        {
            ReferenceID = string.Empty;
        }
        public AppStandardReferenceIDFilter(string? referenceID)
        {
            ReferenceID = string.IsNullOrEmpty(referenceID) ? string.Empty : referenceID;
        }
    }
}
