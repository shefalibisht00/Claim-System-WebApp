using AutoMapper;
using ClaimSystem.DAL.Entities;
using ClaimSystem.shared.dto;

namespace ClaimSystem.BLL.Mappings
{
    public class DomainEntityMappingProfile : Profile
    {
        public DomainEntityMappingProfile()
        {

            // ***** DONT CHANGE **********
            CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();
           CreateMap<ReimbursementClaimDto, ReimbursementClaim>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();

            CreateMap<ReimbursementClaimDto, ApplicationUser>().ReverseMap();
            // ***** DONT CHANGE **********


            // Add on
            CreateMap<ReimbursementClaimDto, ClaimDetailsDto>().ReverseMap();

            //  Claims Details
            CreateMap<ClaimDetailsDto, ClaimDetails>().ReverseMap();

            CreateMap<ReimbursementClaimDto, ClaimDetails>().ReverseMap();

            

        }
    }
    

}