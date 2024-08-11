namespace UangKuAPI.BusinessObjects.Filter
{
    public class AppStandardReferenceItemFilter : Base
    {
        public string? StandardReferenceID { get; set; }
        public bool? isActive { get; set; }
        public bool? isUse { get; set; }

        public AppStandardReferenceItemFilter() : base()
        {
            StandardReferenceID = string.Empty;
            isActive = true;
            isUse = true;
        }

        public AppStandardReferenceItemFilter(int pageNumber, int pageSize, string? standardID, bool? isactive, bool? isuse) : base(pageNumber, pageSize)
        {
            StandardReferenceID = string.IsNullOrEmpty(standardID) ? string.Empty : standardID;
            isActive = isactive;
            isUse = isuse;
        }
    }
}
