using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimSystem.DAL.Entities
{
    public class ClaimDetails
    {
     
        [Key]
        public int ClaimDetailId { get; set; }
        
        [Required]
        public string ApprovedBy { get; set; } 

        [Required]
        public decimal ApprovedAmount { get; set; }
        [Required]
        public string InternalNotes { get; set; }

     //   [ForeignKey("ReimbursementClaim")]
        //public int ClaimId { get; set; }

        public ReimbursementClaim ReimbursementClaim {  get; set; }
    }
}
