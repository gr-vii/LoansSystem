using AutoMapper;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Entities.Models;

namespace LoansManagementSystem.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<CreateClientLoanApplicationRequest, LoanApplication>();

            CreateMap<UpdateClientRequest, Client>();

            CreateMap<CreateClientRequest, Client>();

            CreateMap<UpdateClientLoanApplicationRequest, LoanApplication>()
                .ForMember(ds => ds.Id,
                    opt => opt.MapFrom(
                        src => src.Id));

            CreateMap<SignClientInRequest, Client>();

            CreateMap<SignAdministratorInRequest, Administrator>();

            CreateMap<CreateLoanPaymentRequest, LoanPayment>();

            CreateMap<SetStatusClientLoanApplicationRequest, LoanApplication>()
                .ForMember(ds => ds.ApprovalStatus,
                    opt => opt.MapFrom(
                        src => src.Status));
        }
    }
}
