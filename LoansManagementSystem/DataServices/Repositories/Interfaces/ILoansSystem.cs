using LoansManagementSystem.Entities.Dtos.Requests;
using System.ComponentModel.DataAnnotations;

namespace LoansManagementSystem.DataServices.Repositories.Interfaces;

public interface ILoansSystem
{
    IClientRepository Clients { get; }
    ILoanApplicationRepository LoanApplications { get; }
    ILoanPaymentRepository LoanPayments { get; }
    IAdministratorRepository Administrators { get; }

    Task<bool> CompleteAsync();
    ValidationResult ValidateLoanApplication(CreateClientLoanApplicationRequest loan);
}