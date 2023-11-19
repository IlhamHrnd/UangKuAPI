namespace UangKuAPI.Filter
{
    public class PictureFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string PersonID { get; set; }
        public bool IsDeleted { get; set; }
        public PictureFilter()
        {
            PageNumber = 1;
            PageSize = 10;
            PersonID = string.Empty;
            IsDeleted = false;
        }
        public PictureFilter(int pageNumber, int pageSize, string personID, bool isDeleted)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
            IsDeleted = isDeleted;
        }
    }
}
