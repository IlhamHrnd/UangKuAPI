using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class Subdistrict
    {
        [Key]
        [Required(ErrorMessage = "SubdistrictID Is Required")]
        public int SubdisID { get; set; }
        public string? SubdisName { get; set; }
        public int DisID { get; set; }
    }
}
