using LoansManagementSystem.Entities.Models;

namespace LoansManagementSystem.DataServices.Repositories.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<Client?> GetClientByPhoneNumberAsync(string phoneNumber);
}