using ClaimSystem.shared.dto;
using System;
using System.Collections.Generic;

namespace ClaimSystem.shared.dto
{
    public class ReimbursementClaimDto
    {
       

        public ReimbursementClaimDto()
        {
            this.ApprovedValue = 0;
            this.IsProcessed = false;
            this.Status = null;
        }

      
        public int ClaimId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string ReimbursementType { get; set; }
        
        public decimal RequestedValue { get; set; }

        public decimal ApprovedValue { get; set; }
        
        public string Currency { get; set; }
        
        public bool IsProcessed { get; set; }
        public bool? Status { get; set; }
        public string UploadedFilePath { get; set; }
        // ------ FK is ClaimOwnerID
        public string ClaimOwnerId { get; set; }

        public ApplicationUserDto ApplicationUser { get; set; }

        // ------

      //  public ClaimDetailsDto ClaimDetails { get; set; }

        //public ClaimFileDetails ClaimFileDetails { get; set; }
    }
}
