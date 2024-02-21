using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Utilities;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.DataServices.Repositories.implementations;

public class LoansSystem : ILoansSystem, IDisposable
{
    private readonly Db _db;
    protected readonly Configurations _config;

    public IClientRepository Clients { get; }
    public ILoanApplicationRepository LoanApplications { get; }
    public ILoanPaymentRepository LoanPayments { get; }
    public IAdministratorRepository Administrators { get; }

    public LoansSystem(ILoggerFactory loggerFactory, Db db, IOptions<Configurations> config)
    {
        _db = db;
        _config = config.Value;

        var logger = loggerFactory.CreateLogger("logs");

        Clients = new ClientRepository(logger, db, config);
        LoanApplications = new LoanApplicationRepository(logger, db, config);
        LoanPayments = new LoanPaymentRepository(logger, db, config);
        Administrators = new AdministratorRepository(logger, db, config);
    }

    public async Task<bool> CompleteAsync()
    {
        var result = await _db.SaveChangesAsync();

        return result > 0;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}