using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class Profile
    {
        [Key]
        [Required(ErrorMessage = "PersonID Is Required")]
        public string? PersonID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PlaceOfBirth { get; set; }
        public byte[]? Photo { get; set; }
        public string? Address { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }
        public string? Subdistrict { get; set; }
        public string? District { get; set; }
        public int PostalCode { get; set; }
    }
}
