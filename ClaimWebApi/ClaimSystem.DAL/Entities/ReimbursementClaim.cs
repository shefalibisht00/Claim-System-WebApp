using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimSystem.DAL.Entities
{
    public class ReimbursementClaim
    {
              
        public ReimbursementClaim()
        {
            this.ApprovedValue = 0;
            this.IsProcessed = false;
            this.Status = false;
        }

        [Key]
        public int ClaimId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [Required]
        public string ReimbursementType { get; set; }

        [Required]
        public decimal RequestedValue { get; set; }
        
        
        public decimal ApprovedValue { get; set; }

        [Required]
        public string Currency { get; set; }

      
        public bool IsProcessed { get; set; }

       
        public Nullable<bool> Status { get; set; }

        [StringLength(255)]
        public string UploadedFilePath { get; set; }

        // FK col name is ClaimOwnerID
        [Required]
        [ForeignKey("ApplicationUser")]
        public string ClaimOwnerId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; protected set; }

      //  public  ClaimDetails ClaimDetails { get; set; }

        //public ClaimFileDetails ClaimFileDetails { get; set; }
    }
}
