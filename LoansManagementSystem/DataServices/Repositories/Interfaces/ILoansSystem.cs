using System.ComponentModel.DataAnnotations;
using LoansManagementSystem.Entities.Dtos.Requests;

namespace LoansManagementSystem.DataServices.Repositories.Interfaces;

public interface ILoansSystem
{
    IClientRepository Clients { get; }
    ILoanApplicationRepository LoanApplications { get; }
    ILoanPaymentRepository LoanPayment { get; }

    Task<bool> CompleteAsync();
    ValidationResult ValidateLoanApplication(CreateClientLoanApplicationRequest loan);
}