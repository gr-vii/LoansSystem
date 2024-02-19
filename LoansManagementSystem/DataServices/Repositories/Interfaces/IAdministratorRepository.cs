using LoansManagementSystem.Entities.Models;

namespace LoansManagementSystem.DataServices.Repositories.Interfaces;

public interface IAdministratorRepository : IGenericRepository<Administrator>
{
    Task<Administrator> GetAdministratorByPhoneNumberAsync(string phoneNumber);
}