using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.DataServices.Repositories.implementations;

public class LoanApplicationRepository : GenericRepository<LoanApplication>, ILoanApplicationRepository
{
    public LoanApplicationRepository(ILogger logger, Db db, IOptions<Configurations> config) : base(logger, db, config) { }

    public async Task<IEnumerable<LoanApplication>> GetClientLoansAsync(Guid clientId)
    {
        try
        {
            return _dbSet.Where(e => e.ClientId == clientId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetClientLoansAsync function error", typeof(LoanApplicationRepository));

            throw;
        }
    }

    public override async Task<IEnumerable<LoanApplication>> All()
    {
        try
        {
            return await _dbSet.Select(e => e)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(e => e.InsDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} All function error", typeof(LoanApplicationRepository));

            throw;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.Status == true);

            if (result == null)
                return false;

            result.Status = false;
            result.LupDate = DateTime.UtcNow;

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override async Task<bool> Update(LoanApplication client)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.Id == client.Id && e.Status == true);

            if (result == null)
                return false;

            result.Amount = client.Amount;
            result.EmploymentType = client.EmploymentType;
            result.MonthIncome = client.MonthIncome;
            result.Purpose = client.Purpose;
            result.Term = client.Term;
            result.LupDate = DateTime.UtcNow;

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}