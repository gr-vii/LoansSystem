using AutoMapper;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Entities.Models;

namespace LoansManagementSystem.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<LoanApplication, ClientLoanApplicationResponse>()
                .ForMember(ds => ds.LoanId,
                    opt => opt.MapFrom(
                        src => src.Id));

            CreateMap<Client, ClientResponse>();

            CreateMap<LoanPayment, LoanPaymentResponse>()
                .ForMember(ds => ds.PaymentDate,
                    opt => opt.MapFrom(
                        src => src.InsDate))
                .ForMember(ds => ds.AmountPaid,
                    opt => opt.MapFrom(
                        src => src.Amount))
                .ForMember(ds => ds.TotalAmountPaid,
                    opt => opt.MapFrom(
                        src => src.Loan!.LoanPayment.Sum(p => p.Amount)))
                .ForMember(ds => ds.TotalAmountLeft,
                    opt => opt.MapFrom(
                        src => src.Loan!.Amount - src.Loan.LoanPayment.Sum(p => p.Amount)))
                .ForMember(ds => ds.TermTimeLeft,
                    opt => opt.MapFrom(
                        src => src.Loan!.Term - src.Loan.LoanPayment.Count))
                .ForMember(ds => ds.LoanId,
                opt => opt.MapFrom(
                    src => src.LoanId));
        }
    }
}
