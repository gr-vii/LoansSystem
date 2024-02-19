using AutoMapper;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Utilities;

namespace LoansManagementSystem.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<CreateClientLoanApplicationRequest, LoanApplication>()
                .ForMember(ds => ds.InsDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow))
                .ForMember(ds => ds.LupDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow))
                .ForMember(ds => ds.Status,
                    opt => opt.MapFrom(
                        src => true));

            CreateMap<UpdateClientRequest, LoanApplication>()
                .ForMember(ds => ds.LupDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow));

            CreateMap<CreateClientRequest, Client>()
                .ForMember(ds => ds.InsDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow))
                .ForMember(ds => ds.LupDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow))
                .ForMember(ds => ds.Status,
                    opt => opt.MapFrom(
                        src => true))
                .ForMember(ds => ds.Salt,
                    //TODO do salt
                    opt => opt.MapFrom(src => "1234"));

            CreateMap<UpdateClientLoanApplicationRequest, Client>()
                .ForMember(ds => ds.LupDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow));

            CreateMap<CreateAccountRequest, Client>()
                .ForMember(ds => ds.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber.VerifyAndCorrectPhone()))
                .ForMember(ds => ds.InsDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(ds => ds.LupDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(ds => ds.Status,
                    opt => opt.MapFrom(src => true));

            CreateMap<CreateLoanPaymentRequest, LoanPayment>()
                .ForMember(ds => ds.InsDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow))
                .ForMember(ds => ds.LupDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow))
                .ForMember(ds => ds.Status,
                    opt => opt.MapFrom(
                        src => true));

            CreateMap<SetStatusClientLoanApplicationRequest, LoanApplication>()
                .ForMember(ds => ds.LupDate,
                    opt => opt.MapFrom(
                        src => DateTime.UtcNow))
                .ForMember(ds => ds.ApprovalStatus,
                    opt => opt.MapFrom(
                        src => src.Status));
        }
    }
}
