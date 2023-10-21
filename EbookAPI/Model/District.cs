using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class District
    {
        [Key]
        [Required(ErrorMessage = "DistrictID Is Required")]
        public int DisID { get; set; }
        public string? DisName { get; set; }
        public int CityID { get; set; }
    }
}
