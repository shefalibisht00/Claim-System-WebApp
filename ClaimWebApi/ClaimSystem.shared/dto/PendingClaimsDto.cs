using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSystem.shared.dto
{
    public class PendingClaimsDto
    {
        public int ClaimId { get; set; }

        public DateTime Date { get; set; }

        public string ReimbursementType { get; set; }

        public decimal RequestedValue { get; set; }

        public decimal ApprovedValue { get; set; }

        public string Currency { get; set; }

        public bool IsProcessed { get; set; }

        public Nullable<bool> Status { get; set; }


        public string UploadedFilePath { get; set; }
     
        public string ClaimOwnerId { get; set; }

        public string ClaimOwnerEmail { get; set; }

    }
}
