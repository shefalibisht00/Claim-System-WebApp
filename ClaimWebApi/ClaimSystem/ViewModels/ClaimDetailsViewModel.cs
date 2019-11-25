using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClaimSystem.ViewModels
{
    public class ClaimDetailsViewModel
    {
        

        [Key]
        public int ClaimDetailId { get; set; }

        [Required]
        public string ApprovedBy { get; set; }

        [Required]
        public decimal ApprovedAmount { get; set; }

        [Required]
        public string InternalNotes { get; set; }

        //[ForeignKey("ReimbursementClaim")]
        //public int claimId { get; set; }

        public  ReimbursementClaimView ReimbursementClaim { get; set; }
        
    }
}