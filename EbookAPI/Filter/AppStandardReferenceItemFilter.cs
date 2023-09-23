namespace UangKuAPI.Filter
{
    public class AppStandardReferenceItemFilter
    {
        public string? StandardReferenceID { get; set; }

        public AppStandardReferenceItemFilter()
        {
            StandardReferenceID = string.Empty;
        }

        public AppStandardReferenceItemFilter(string? standardID)
        {
            StandardReferenceID = string.IsNullOrEmpty(standardID) ? string.Empty : standardID;
        }
    }
}
