using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.BusinessObjects.Model
{
    public class AppProgram
    {
        [Key]
        [Required(ErrorMessage = "ProgramID Is Required")]
        public string? ProgramID { get; set; }
        public string? ProgramName { get; set; }
        public string? Note { get; set; }
        public bool? IsProgram { get; set; }
        public bool? IsProgramAddAble { get; set; }
        public bool? IsProgramEditAble { get; set; }
        public bool? IsProgramDeleteAble { get; set; }
        public bool? IsProgramViewAble { get; set; }
        public bool? IsProgramApprovalAble { get; set; }
        public bool? IsProgramUnApprovalAble { get; set; }
        public bool? IsProgramVoidAble { get; set; }
        public bool? IsProgramUnVoidAble { get; set; }
        public bool? IsProgramPrintAble { get; set; }
        public bool? IsVisible { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public bool IsUsedBySystem { get; set; }
    }
}
