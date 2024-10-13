namespace UangKuAPI.BusinessObjects.Filter
{
    public class TransTypeFilter
    {
        public string? TransType { get; set; }
        public TransTypeFilter()
        {
            TransType = string.Empty;
        }
        public TransTypeFilter(string? transType)
        {
            TransType = !string.IsNullOrEmpty(transType) ? transType : string.Empty;
        }
    }
}