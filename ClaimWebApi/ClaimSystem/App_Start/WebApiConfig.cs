using ClaimSystem.DAL;
using ClaimSystem.DAL.CommonRepository;
using ClaimSystem.DAL.Entities;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using Unity.Lifetime;

namespace ClaimSystem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var container = new UnityContainer();
            //container.RegisterType<IRepository<ApplicationUser>, Repository<ApplicationUser>>();

            //container.RegisterType<IRepository<ReimbursementClaim>, Repository<ReimbursementClaim>>();

            //container.RegisterType<IRepository<ClaimDetails>, Repository<ClaimDetails>>();

            //container.RegisterType<Repository<ApplicationUser>,UserRepository>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IClaimRepository, ClaimRepository>();

            container.RegisterType<IClaimDetailRepository, ClaimDetailRepository>();

            config.DependencyResolver = new UnityResolver(container);







            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            

            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            jsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;


            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
