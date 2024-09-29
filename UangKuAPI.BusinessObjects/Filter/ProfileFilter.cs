namespace UangKuAPI.BusinessObjects.Filter
{
    public class ProfileFilter : Base.Base
    {
        public string? PersonID { get; set; }
        public ProfileFilter() : base()
        {
            PersonID = string.Empty;
        }
        public ProfileFilter(int pageNumber, int pageSize, string personID) : base(pageNumber, pageSize)
        {
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
        }
    }
}