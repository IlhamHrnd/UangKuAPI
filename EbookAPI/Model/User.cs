using System.ComponentModel.DataAnnotations;

namespace EbookAPI.Model
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Username Is Required")]
        public string? Username { get; set; }
        public string? SexName { get; set; }
        public string? AccessName { get; set; }
        public string? StatusName { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUser { get; set; }
        public string? PersonID { get; set; }
    }

    public class UserName
    {
        public string? Sex { get; set; }
        public string? Access { get; set; }
        public bool IsActive { get; set; }
        public string? LastUpdateUser { get; set; }
    }
}
