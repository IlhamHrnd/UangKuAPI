namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserPictureFilter : Base
    {
        public string PersonID { get; set; }
        public bool IsDeleted { get; set; }
        public string PictureID { get; set; }
        public UserPictureFilter() : base()
        {
            PersonID = string.Empty;
            IsDeleted = false;
            PictureID = string.Empty;
        }

        public UserPictureFilter(int pageNumber, int pageSize, string? personID, bool isDeleted, string? pictureID ) : base(pageNumber, pageSize)
        {
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
            IsDeleted = isDeleted;
            PictureID = !string.IsNullOrEmpty(pictureID) ? pictureID : string.Empty;
        }
    }
}
