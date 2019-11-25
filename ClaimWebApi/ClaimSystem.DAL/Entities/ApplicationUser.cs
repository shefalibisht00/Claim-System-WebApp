using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimSystem.DAL.Entities
{
  
    public class ApplicationUser : IdentityUser
        {

        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string PanNumber { get; set; }
        [Required]
        public string Bank { get; set; }
        [Required]
        public string BankAccountNumber { get; set; }
        
        
    }
}
