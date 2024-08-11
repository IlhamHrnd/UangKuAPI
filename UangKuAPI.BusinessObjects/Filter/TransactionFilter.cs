using UangKuAPI.BusinessObjects.Helper;

namespace UangKuAPI.BusinessObjects.Filter
{
    public class TransactionFilter : Base
    {
        public string PersonID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? OrderBy { get; set; }
        public bool? IsAscending { get; set; }
        public string? TransType { get; set; }
        public string? TransNo { get; set; }
        public TransactionFilter() : base()
        {
            PersonID = string.Empty;
            OrderBy = string.Empty;
            IsAscending = false;
            TransType = string.Empty;
            TransNo = string.Empty;
        }
        public TransactionFilter(int pageNumber, int pageSize, string personID, DateTime? startDate, DateTime? endDate, 
            string? orderBy, bool? isAscending, string? transType, string? transNo) : base(pageNumber, pageSize)
        {
            DateTime date = DateTime.Now;
            
            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
            StartDate = startDate.HasValue ? startDate.Value : DateFormat.DateTimeNowDate(date.Year, date.Month, 1);
            EndDate = endDate.HasValue ? endDate.Value : DateFormat.DateTimeNowDate(date.Year, date.Month, date.Day);
            OrderBy = !string.IsNullOrEmpty(orderBy) ? orderBy : string.Empty;
            IsAscending = isAscending;
            TransType = !string.IsNullOrEmpty(transType) ? transType : string.Empty;
            TransNo = !string.IsNullOrEmpty(transNo) ? transNo : string.Empty;
        }
    }
}
