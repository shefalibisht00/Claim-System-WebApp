using AutoMapper;
using ClaimSystem.shared.dto;
using ClaimSystem.ViewModels;

namespace ClaimSystem.Mappings
{
    public class DomainViewModelMappingProfile : Profile
    {
        public DomainViewModelMappingProfile()
        {
            // ***** DONT CHANGE **********
            CreateMap<ApplicationUserDto, UserViewModel>().ReverseMap();

            CreateMap<ReimbursementClaimDto, ReimbursementClaimView>().ReverseMap();
            CreateMap<ReimbursementClaimDto, ApplicationUserDto>().ReverseMap();

          //  CreateMap<ReimbursementClaimDto, ApplicationUserDto>().ReverseMap();

            CreateMap<ReimbursementClaimDto, UserViewModel>().ReverseMap();

            CreateMap<ReimbursementClaimView, ApplicationUserDto>().ReverseMap();

            // *******************************
            CreateMap<ReimbursementClaimView, ClaimDetailsDto>().ReverseMap();

            CreateMap<DataDto,DataViewModel>();


            // ******
            CreateMap<ReimbursementClaimDto, ClaimDetailsViewModel>().ReverseMap();

            //  Claims Details
            CreateMap<ClaimDetailsDto, ClaimDetailsViewModel>().ReverseMap();

            CreateMap<ReimbursementClaimView, ClaimDetailsDto>().ReverseMap();




        }
    }

}