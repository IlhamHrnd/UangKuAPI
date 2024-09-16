namespace UangKuAPI.BusinessObjects.Filter
{
    public class AppStandardReferenceFilter : Base.Base
    {
        public string? ReferenceID { get; set; }
        public AppStandardReferenceFilter() : base()
        {
            ReferenceID = string.Empty;   
        }
        public AppStandardReferenceFilter(int pageNumber, int pageSize, string referenceID) : base(pageNumber, pageSize)
        {
            ReferenceID = !string.IsNullOrEmpty(referenceID) ? referenceID : string.Empty;
        }
    }
}