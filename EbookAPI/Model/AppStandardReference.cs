using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class AppStandardReference
    {
        [Key]
        [Required(ErrorMessage = "StandardReferenceID Is Required")]
        public string? StandardReferenceID { get; set; }
        public string? StandardReferenceName { get; set; }
        public int ItemLength { get; set; }
        public bool IsUsedBySystem { get; set; }
        public bool IsActive { get; set; }
        public string? Note { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
    }
}
