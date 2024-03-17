namespace UangKuAPI.Filter
{
    public class AppStandardReferenceItemFilter
    {
        public string? StandardReferenceID { get; set; }
        public bool? isActive { get; set; }
        public bool? isUse { get; set; }

        public AppStandardReferenceItemFilter()
        {
            StandardReferenceID = string.Empty;
            isActive = true;
            isUse = true;
        }

        public AppStandardReferenceItemFilter(string? standardID, bool? isactive, bool? isuse)
        {
            StandardReferenceID = string.IsNullOrEmpty(standardID) ? string.Empty : standardID;
            isActive = isactive;
            isUse = isuse;
        }
    }
}
