namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserPictureFilter : Base.Base
    {
        public string? PersonID { get; set; }
        public string? PictureID { get; set; }
        public bool? IsDelete { get; set; }
        public UserPictureFilter() : base()
        {
            PersonID = string.Empty;
            PictureID = string.Empty;
            IsDelete = null;
        }
        public UserPictureFilter(int pageNumber, int pageSize, string personID, string pictureID, bool? isDelete) : base(pageNumber, pageSize)
        {
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
            PictureID = !string.IsNullOrEmpty(pictureID) ? pictureID : string.Empty;
            IsDelete = isDelete.HasValue ? isDelete.Value : null;
        }
    }
}