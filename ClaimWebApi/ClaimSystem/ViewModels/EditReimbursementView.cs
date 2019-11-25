using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaimSystem.ViewModels
{
    public class EditReimbursementView
    {
        

        public int ClaimId { get; set; }

        public DateTime Date { get; set; }

        public string ReimbursementType { get; set; }

        public Decimal RequestedValue { get; set; }

        public string Currency { get; set; }
    }
}