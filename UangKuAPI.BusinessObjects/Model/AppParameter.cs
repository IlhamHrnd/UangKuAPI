using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.BusinessObjects.Model
{
    public class AppParameter
    {
        [Key]
        [Required(ErrorMessage = "ParameterID Is Required")]
        public string? ParameterID { get; set; }
        public string? ParameterName { get; set; }
        public string? ParameterValue { get; set; }
        public string? SRControl { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public bool IsUsedBySystem { get; set; }
    }
}
