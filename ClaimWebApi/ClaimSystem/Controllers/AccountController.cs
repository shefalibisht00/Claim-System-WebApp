using ClaimSystem.BLL;
using ClaimSystem.DAL;
using ClaimSystem.DAL.Entities;
using ClaimSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Web.Http;

namespace ClaimSystem.Controllers
{
    public class AccountController : ApiController
    {
        private UserService claimService;
        public AccountController(UserService claimService)
        {
            this.claimService = claimService;
        }

        [Route("api/User/Register")]
        [HttpPost]
        public IdentityResult Register(UserViewModel model)
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var isUserPresent = claimService.UserEmailExist(model.Email);
            if (!isUserPresent)
            {
                var userStore = new UserStore<ApplicationUser>(new ClaimContext());
                var manager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser() { FullName = model.FullName, Email = model.Email };
                user.UserName = model.Email;
                user.Password = model.Password;
                user.PasswordHash = model.Password;
                user.ConfirmPassword = model.ConfirmPassword;
                user.PanNumber = model.PanNumber;
                user.Bank = model.Bank;
                user.BankAccountNumber = model.BankAccountNumber;
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 3
                };
                IdentityResult result = manager.Create(user, model.Password);
                model.UserRoleType = "NonAdmin";
                Debug.WriteLine("\n\nsdsd  --> " + user.Id);
                manager.AddToRole(user.Id, model.UserRoleType);
                return result;
            }
            return null;
            
        }


        [HttpGet]
        [Route("api/GetUserClaims")]
        public UserViewModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;

            var model = new UserViewModel()
            {
                Id = identityClaims.FindFirst("Id").Value,
                UserName = identityClaims.FindFirst("Username").Value,
                Bank = identityClaims.FindFirst("Bank").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FullName = identityClaims.FindFirst("FullName").Value,
                BankAccountNumber = identityClaims.FindFirst("BankAccountNumber").Value,
                PanNumber = identityClaims.FindFirst("PanNumber").Value,
                UserRoleType = "NonAdmin"

            };
            return model;
        }




      
    }
}
