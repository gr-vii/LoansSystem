using LoansManagementSystem.Entities.Models;

namespace LoansManagementSystem.DataServices.Repositories.Interfaces;

public interface ILoanApplicationRepository : IGenericRepository<LoanApplication>
{
    Task<IEnumerable<LoanApplication?>> GetClientLoansAsync(Guid clientId);
}