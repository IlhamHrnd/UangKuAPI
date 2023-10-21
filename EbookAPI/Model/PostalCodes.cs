using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class PostalCodes
    {
        [Key]
        [Required(ErrorMessage = "PostalCode Is Required")]
        public int PostalID { get; set; }
        public int SubdisID { get; set; }
        public int DisID { get; set; }
        public int CityID { get; set; }
        public int ProvID { get; set; }
        public int PostalCode { get; set; }
    }
}
