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
            PageSize = 25;
            PersonID = string.Empty;
            IsDeleted = false;
        }
        public PictureFilter(int pageNumber, int pageSize, string personID, bool isDeleted)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 25 ? 25 : pageSize;
            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
            IsDeleted = isDeleted;
        }
    }

    public class DeletedPictureFilter
    {
        public string LastUpdateUserID { get; set; }
        public bool IsDeleted { get; set; }
        public DeletedPictureFilter()
        {
            LastUpdateUserID = string.Empty;
            IsDeleted = false;
        }
        public DeletedPictureFilter(string lastupdateUserID, bool isDeleted)
        {
            LastUpdateUserID = string.IsNullOrEmpty(lastupdateUserID) ? string.Empty : lastupdateUserID;
            IsDeleted = isDeleted;
        }
    }
}
