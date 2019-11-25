using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSystem.shared.dto
{
    public class EditReimbursementDto
    {
        public int ClaimId { get; set; }

        public DateTime Date { get; set; }

        public string ReimbursementType { get; set; }

        public Decimal RequestedValue { get; set; }

        public string Currency { get; set; }
    }
}
