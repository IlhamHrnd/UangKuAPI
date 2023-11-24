using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class UserPicture
    {
        [Key]
        [Required(ErrorMessage = "PictureID Is Required")]
        public string? PictureID { get; set; }
        public byte[]? Picture { get; set; }
        public string? PictureName { get; set; }
        public string? PictureFormat { get; set; }
        public string? PersonID { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
    }
    public class PostUserPicture
    {
        [Key]
        [Required(ErrorMessage = "PictureID Is Required")]
        public string? PictureID { get; set; }
        public string? Picture { get; set; }
        public string? PictureName { get; set; }
        public string? PictureFormat { get; set; }
        public string? PersonID { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public long PictureSize { get; set; }
    }
    public class GetUserPicture
    {
        [Key]
        [Required(ErrorMessage = "PictureID Is Required")]
        public string? PictureID { get; set; }
        public byte[]? Picture { get; set; }
        public string? PictureName { get; set; }
        public string? PictureFormat { get; set; }
        public string? PersonID { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
    }
}
