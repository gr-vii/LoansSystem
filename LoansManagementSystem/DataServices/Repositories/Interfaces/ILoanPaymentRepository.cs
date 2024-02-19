using LoansManagementSystem.Entities.Models;

namespace LoansManagementSystem.DataServices.Repositories.Interfaces;

public interface ILoanPaymentRepository : IGenericRepository<LoanPayment>
{
    Task<IEnumerable<LoanPayment?>> GetLoanPaymentsAsync(Guid loanId);
}