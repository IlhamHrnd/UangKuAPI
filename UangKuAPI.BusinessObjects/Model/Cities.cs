using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.BusinessObjects.Model
{
    public class Cities
    {
        [Key]
        [Required(ErrorMessage = "CityID Is Required")]
        public int CityID { get; set; }
        public string? CityName { get; set; }
        public int? ProvID { get; set; }
    }
}
