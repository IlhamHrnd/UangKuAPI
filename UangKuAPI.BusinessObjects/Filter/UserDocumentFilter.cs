namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserDocumentFilter : Base.Base
    {
        public string? TransType { get; set; }
        public string? DocumentID { get; set; }
        public string? PersonID { get; set; }
        public UserDocumentFilter() : base()
        {
            TransType = string.Empty;
            DocumentID = string.Empty;
            PersonID = string.Empty;
        }

        public UserDocumentFilter(int pageNumber, int pageSize, string? transType, string? documentID, string? personID) : base(pageNumber, pageSize)
        {
            TransType = !string.IsNullOrEmpty(transType) ? transType : string.Empty;
            DocumentID = !string.IsNullOrEmpty(documentID) ? documentID : string.Empty;
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
        }
    }
}
