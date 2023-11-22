using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class AppParameter
    {
        [Key]
        [Required(ErrorMessage = "ParameterID Is Required")]
        public string? ParameterID { get; set; }
        public string? ParameterName { get; set; }
        public string? ParameterValue { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public bool IsUsedBySystem { get; set; }
    }

    //Class Static Untuk Parameter
    //Jangan DIGANTI
    public static class AppParameterValue
    {
        private static string? maxpicture = "MaxPicture";
        public static string? MaxPicture { get => maxpicture; set => maxpicture = value; }
    }
}
