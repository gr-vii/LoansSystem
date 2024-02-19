using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.DataServices.Repositories;

public class LoanPaymentRepository : GenericRepository<LoanPayment>, ILoanPaymentRepository
{
    public LoanPaymentRepository(ILogger logger, Db db, IOptions<Configurations> config) : base(logger, db, config) { }

    public async Task<IEnumerable<LoanPayment?>> GetLoanPaymentsAsync(Guid loanId)
    {
        try
        {
            return _dbSet.Where(e => e.LoanId == loanId).Include(lp => lp.Loan);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetLoanPaymentsAsync function error", typeof(LoanPaymentRepository));

            throw;
        }
    }
}