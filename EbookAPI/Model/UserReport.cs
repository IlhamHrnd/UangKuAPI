using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.Model
{
    public class UserReport
    {
        [Key]
        [Required(ErrorMessage = "ReportNo Is Required")]
        public string? ReportNo { get; set; }
        public DateTime? DateErrorOccured { get; set; }
        public string? SRErrorLocation { get; set; }
        public string? SRErrorPossibility { get; set; }
        public string? ErrorCronologic { get; set; }
        public byte[]? Picture { get; set; }
        public bool? IsApprove { get; set; }
        public string? SRReportStatus { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string? ApprovedByUserID { get; set; }
        public DateTime? VoidDateTime { get; set; }
        public string? VoidByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public string? PersonID { get; set; }
    }

    public class PostUserReport
    {
        [Key]
        [Required(ErrorMessage = "ReportNo Is Required")]
        public string? ReportNo { get; set; }
        public DateTime DateErrorOccured { get; set; }
        public string? SRErrorLocation { get; set; }
        public string? SRErrorPossibility { get; set; }
        public string? ErrorCronologic { get; set; }
        public string? Picture { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public string? PersonID { get; set; }
    }

    public class PatchUserReport
    {
        [Key]
        [Required(ErrorMessage = "ReportNo Is Required")]
        public string? ReportNo { get; set; }
        public string? SRReportStatus { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string? ApprovedByUserID { get; set; }
        public DateTime? VoidDateTime { get; set; }
        public string? VoidByUserID { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
    }

    public class GetUserReport
    {
        [Key]
        [Required(ErrorMessage = "ReportNo Is Required")]
        public string? ReportNo { get; set; }
        public DateTime? DateErrorOccured { get; set; }
        public string? SRErrorLocation { get; set; }
        public string? SRErrorPossibility { get; set; }
        public string? ErrorCronologic { get; set; }
        public byte[]? Picture { get; set; }
        public bool? IsApprove { get; set; }
        public string? SRReportStatus { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? CreatedByUserID { get; set; }
        public string? PersonID { get; set; }
    }
}
