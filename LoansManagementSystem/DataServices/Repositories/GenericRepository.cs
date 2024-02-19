using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.DataServices.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public readonly ILogger _logger;
    protected Db _db;
    internal DbSet<T> _dbSet;
    protected readonly Configurations _config;

    public GenericRepository(ILogger logger, Db db, IOptions<Configurations> config)
    {
        _db = db;
        _logger = logger;
        _dbSet = db.Set<T>();
        _config = config.Value;
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        throw new NotImplementedException();
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);

        return true;
    }

    public virtual async Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}