using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.DataServices.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(ILogger logger, Db db, IOptions<Configurations> config) : base(logger, db, config)
    {
    }

    public async Task<Client?> GetClientByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber && e.Status == true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetClientByPhoneNumberAsync function error", typeof(LoanApplicationRepository));
            throw;
        }
    }

    public override async Task<IEnumerable<Client>> All()
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
            _logger.LogError(e, "{Repo} All function error", typeof(ClientRepository));

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

    public override async Task<bool> Update(Client client)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.Id == client.Id && e.Status == true);

            if (result == null)
                return false;

            result.PhoneNumber = client.PhoneNumber;
            result.AccountNumber = client.PhoneNumber;
            result.Password = client.Password;
            result.Salt = client.Salt;
            result.LastName = client.LastName;
            result.FirstName = client.FirstName;
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