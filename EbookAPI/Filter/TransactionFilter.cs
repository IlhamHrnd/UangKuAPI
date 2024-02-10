namespace UangKuAPI.Filter
{
    public class TransactionFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string PersonID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TransactionFilter()
        {
            PageNumber = 1;
            PageSize = 1;
            PersonID = string.Empty;
        }
        public TransactionFilter(int pageNumber, int pageSize, string personID, DateTime? startDate, DateTime? endDate)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 1 : pageSize;
            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
            StartDate = startDate.HasValue ? startDate.Value : Helper.DateTimeFormat.DateTimeFirstDate(1, Helper.DateTimeFormat.DateTimeNowDate());
            EndDate = endDate.HasValue ? endDate.Value : Helper.DateTimeFormat.DateTimeNowDate();
        }
    }
}
