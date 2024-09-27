namespace UangKuAPI.BusinessObjects.Filter
{
    public class AppStandardRerefenceItemFilter : Base.Base
    {
        public string? StandardReferenceID {  get; set; }
        public bool? IsActive { get; set; }
        public bool? IsUsedBySystem { get; set; }
        public AppStandardRerefenceItemFilter() : base()
        {
            StandardReferenceID = string.Empty;
            IsActive = true;
            IsUsedBySystem = true;
        }
        public AppStandardRerefenceItemFilter(int pageNumber, int pageSize, string referenceID, bool isActive, bool isUsedBySystem) : base(pageNumber, pageSize)
        {
            StandardReferenceID = !string.IsNullOrEmpty(referenceID) ? referenceID : string.Empty;
            IsActive = isActive;
            IsUsedBySystem = isUsedBySystem;
        }
    }
}
