using System;

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
            var dateTime = Helper.DateFormat.DateTimeNow();

            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 1 : pageSize;
            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
            StartDate = startDate.HasValue ? startDate.Value : Helper.DateFormat.DateTimeNowDate(dateTime.Year, dateTime.Month, 1);
            EndDate = endDate.HasValue ? endDate.Value : Helper.DateFormat.DateTimeNowDate(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }

    public class SumTransactionFilter
    {
        public string PersonID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SumTransactionFilter()
        {
            PersonID = string.Empty;
        }
        public SumTransactionFilter(string personID, DateTime? startDate, DateTime? endDate)
        {
            var dateTime = Helper.DateFormat.DateTimeNow();

            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
            StartDate = startDate.HasValue ? startDate.Value : Helper.DateFormat.DateTimeNowDate(dateTime.Year, dateTime.Month, 1);
            EndDate = endDate.HasValue ? endDate.Value : Helper.DateFormat.DateTimeNowDate(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
