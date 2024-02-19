using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Utilities;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

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
    //TODO: remove
    public ValidationResult ValidateLoanApplication(CreateClientLoanApplicationRequest loan)
    {
        var errors = new List<string>();

        if (loan.Amount > _config.LoanMaxValue)
        {
            errors.Add($"Loan amount should not exceed {_config.LoanMaxValue}");
        }

        if (loan.Term > _config.LoanMaxMonthsTerm)
        {
            errors.Add($"Loan term should not exceed {_config.LoanMaxMonthsTerm} months");
        }

        decimal monthlyPayment = CalculateMonthlyPayment(loan.Amount, loan.Term, _config.LoanInterestRate);
        decimal maxMonthlyPayment = CalculateMonthlyIncomeMaxPercentage(loan.MonthIncome, _config.LoanIncomePercentage);

        if (monthlyPayment > maxMonthlyPayment)
        {
            errors.Add($"Monthly payment should not exceed {_config.LoanIncomePercentage}% of monthly income");
        }

        return new ValidationResult(string.Join("\n", errors));
    }

    private decimal CalculateMonthlyPayment(decimal loanAmount, int term, int rate)
    {
        return (loanAmount + loanAmount * rate) / term;
    }

    private decimal CalculateMonthlyIncomeMaxPercentage(decimal income, int maxPercentage)
    {
        return income * maxPercentage / 100;
    }
}