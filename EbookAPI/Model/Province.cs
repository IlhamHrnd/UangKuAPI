using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class Province
    {
        [Key]
        [Required(ErrorMessage = "ProvinceID Is Required")]
        public int ProvID { get; set; }
        public string? ProvName { get; set; }
        public int LocationID { get; set; }
        public int Status { get; set; }
    }
}
