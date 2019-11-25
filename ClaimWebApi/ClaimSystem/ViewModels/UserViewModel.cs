using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaimSystem.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PanNumber { get; set; }
        public string Bank { get; set; }
        public string BankAccountNumber { get; set; }
        public string UserRoleType { get; set; }

//        public ICollection<ReimbursementClaimView> CreatedClaims { get; set; }

    }
}