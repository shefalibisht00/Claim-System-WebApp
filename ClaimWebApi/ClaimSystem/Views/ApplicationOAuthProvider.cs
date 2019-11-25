using ClaimSystem.DAL;
using ClaimSystem.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClaimSystem
{

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new ClaimContext());
            var manager = new UserManager<ApplicationUser>(userStore);
        
             var user =  manager.FindByEmail(context.UserName);
            
            if (context.Password.Equals(user.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("UserName", user.UserName));
                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim("FullName", user.FullName));
                identity.AddClaim(new Claim("Id", user.Id));
                identity.AddClaim(new Claim("Bank", user.Bank));
                identity.AddClaim(new Claim("PanNumber", user.PanNumber));
                identity.AddClaim(new Claim("BankAccountNumber", user.BankAccountNumber));
                var userRoles = manager.GetRoles(user.Id);
                foreach (string roleName in userRoles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                }
                //return data to client
                var additionalData = new AuthenticationProperties(new Dictionary<string, string>{
            {
                "role", Newtonsoft.Json.JsonConvert.SerializeObject(userRoles)
            }
        });
                var token = new AuthenticationTicket(identity, additionalData);
                context.Validated(token);
            }
            else
                return;
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}