namespace UangKuAPI.Filter
{
    public class UserReportFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string PersonID { get; set; }
        public UserReportFilter()
        {
            PageNumber = 1;
            PageSize = 1;
            PersonID = string.Empty;
        }
        public UserReportFilter(int pageNumber, int pageSize, string personID)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 1 : pageSize;
            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
        }
    }
}
