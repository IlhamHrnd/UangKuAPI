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

    public class PostAppStandardReferenceItem
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

    public class PatchAppStandardReferenceItem
    {
        [Key]
        [Required(ErrorMessage = "StandardReferenceID Is Required")]
        public string? referenceID { get; set; }
        public string? itemID { get; set; }
        public string? itemName { get; set; }
        public string? note { get; set; }
        public bool? isActive { get; set; }
        public bool? isUse { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? user { get; set; }
        public string? ItemIcon { get; set; }
        public long IconSize { get; set; }
    }
}
