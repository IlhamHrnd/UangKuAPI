using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UangKuAPI.BusinessObjects.Model
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Username Is Required")]
        public string? Username { get; set; }
        [Column("SRSex")]
        public string? SexName { get; set; }
        [Column("SRAccess")]
        public string? AccessName { get; set; }
        [Column("SRStatus")]
        public string? StatusName { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUser { get; set; }
        public string? PersonID { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
