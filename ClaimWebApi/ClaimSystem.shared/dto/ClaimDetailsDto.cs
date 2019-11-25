using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSystem.shared.dto
{
    public class ClaimDetailsDto
    {
        public int ClaimDetailId { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public decimal ApprovedAmount { get; set; }
      
        public string InternalNotes { get; set; }

          public ReimbursementClaimDto ReimbursementClaim { get; set; }
        
    }
}
