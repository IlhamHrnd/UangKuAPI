namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserReportFilter : Base.Base
    {
        public string? TransType { get; set; }
        public bool? IsApproved { get; set; }
        public string? PersonID { get; set; }
        public string? ReportNo { get; set; }
        public UserReportFilter() : base()
        {
            TransType = string.Empty;
            IsApproved = null;
            PersonID = string.Empty;
            ReportNo = string.Empty;
        }

        public UserReportFilter(int pageNumber, int pageSize, string? transType, bool? isApproved, string? personID, string? reportNo) : base(pageNumber, pageSize)
        {
            TransType = !string.IsNullOrEmpty(transType) ? transType : string.Empty;
            IsApproved = isApproved.HasValue ? isApproved.Value : null;
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
            ReportNo = !string.IsNullOrEmpty(reportNo) ? reportNo : string.Empty;
        }
    }
}