using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.BusinessObjects.Model
{
    public class Transaction
    {
        [Key]
        [Required(ErrorMessage = "TransactionNo Is Required")]
        public string? TransNo { get; set; }
        public string? SRTransaction { get; set; }
        public string? SRTransItem { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
        public string? TransType { get; set; }
        public string? PersonID { get; set; }
        public DateTime? TransDate { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
    }

    public class Transaction2
    {
        [Key]
        [Required(ErrorMessage = "TransactionNo Is Required")]
        public string? TransNo { get; set; }
        public string? SRTransaction { get; set; }
        public string? SRTransItem { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public string? TransType { get; set; }
        public DateTime? TransDate { get; set; }
        public string? PersonID { get; set; }
    }

    public class SumTransaction
    {
        public decimal Amount { get; set; }
        public string? SRTransaction { get; set; }
    }
}
