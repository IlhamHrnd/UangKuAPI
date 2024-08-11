using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.BusinessObjects.Model
{
    public class AppStandardReferenceItem
    {
        [Key]
        [Required(ErrorMessage = "StandardReferenceID Is Required")]
        public string? StandardReferenceID { get; set; }
        public string? ItemID { get; set; }
        public string? ItemName { get; set; }
        public string? Note { get; set; }
        public bool? IsUsedBySystem { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public byte[]? ItemIcon { get; set; }
    }

    public class AppStandardReferenceItem2
    {
        [Key]
        [Required(ErrorMessage = "StandardReferenceID Is Required")]
        public string? StandardReferenceID { get; set; }
        public string? ItemID { get; set; }
        public string? ItemName { get; set; }
        public string? Note { get; set; }
        public bool? IsUsedBySystem { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public string? ItemIcon { get; set; }
        public long IconSize { get; set; }
    }
}
