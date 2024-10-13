using UangKuAPI.BusinessObjects.Base;

namespace UangKuAPI.BusinessObjects.Filter
{
    public class TransactionFilter : Base.Base
    {
        public string? PersonID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? OrderBy { get; set; }
        public bool? IsAscending { get; set; }
        public string? TransNo { get; set; }
        public TransactionFilter()
        {
            PersonID = string.Empty;
            StartDate = DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = DateFormat.DateTimeNow();
            OrderBy = string.Empty;
            IsAscending = true;
            TransNo = string.Empty;
        }
        public TransactionFilter(int pageNumber, int pageSize, string personID, DateTime? startDate, DateTime? endDate, string? orderBy, bool? isAscending, string transNo) : base(pageNumber, pageSize)
        {
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
            StartDate = startDate ?? DateFormat.DateTimeNowDate(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = endDate ?? DateFormat.DateTimeNow();
            OrderBy = !string.IsNullOrEmpty(orderBy) ? orderBy : string.Empty;
            IsAscending = isAscending;
            TransNo = !string.IsNullOrEmpty(transNo) ? transNo : string.Empty;
        }
    }
}