using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSystem.shared.dto
{
    public class ApplicationUserDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PanNumber { get; set; }
        public string Bank { get; set; }
        public string BankAccountNumber { get; set; }
        public string UserRoleType { get; set; }

       
        //  public ICollection<ReimbursementClaimDto> CreatedClaims { get; set; }
    }
}
