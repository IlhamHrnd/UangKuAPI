namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserReportFilter : Base
    {
        public string PersonID { get; set; }
        public bool IsAdmin { get; set; }
        public string ReportNo { get; set; }
        public UserReportFilter() : base()
        {
            PersonID = string.Empty;
            IsAdmin = false;
            ReportNo = string.Empty;
        }
        public UserReportFilter(int pageNumber, int pageSize, string personID, bool isAdmin, string reportNo) : base(pageSize, pageNumber)
        {
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
            IsAdmin = isAdmin;
            ReportNo = !string.IsNullOrEmpty(reportNo) ? reportNo : string.Empty;
        }
    }
}
