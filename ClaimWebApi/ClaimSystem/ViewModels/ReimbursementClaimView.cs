using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClaimSystem.ViewModels
{
    public class ReimbursementClaimView
    {


        public ReimbursementClaimView()
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

        public ClaimDetailsViewModel Claim_Details { get; set; }

      //  FK col name is ClaimOwnerID
         public string ClaimOwnerId { get; set; }

        //  public UserViewModel ApplicationUser { get; set; }
    }
}