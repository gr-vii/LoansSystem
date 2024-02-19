using System.ComponentModel.DataAnnotations;

namespace LoansProcessingSystem;

public class LoanApplicationValidator
{
    public static ValidationResult Validate(LoanApplication loan)
    {
        var LoanApprovalRules = new
        {
            LoanMaxValue = 25000000,
            LoanMaxMonthsTerm = 12,
            LoanIncomePercentage = 40,
            LoanInterestRate = 5,
        };

        var errors = new List<string>();

        if (loan.Amount > LoanApprovalRules.LoanMaxValue)
        {
            errors.Add($"Loan amount should not exceed {LoanApprovalRules.LoanMaxValue}");
        }

        if (loan.Term > LoanApprovalRules.LoanMaxMonthsTerm)
        {
            errors.Add($"Loan term should not exceed {LoanApprovalRules.LoanMaxMonthsTerm} months");
        }

        decimal monthlyPayment = CalculateMonthlyPayment(loan.Amount, loan.Term, LoanApprovalRules.LoanInterestRate);
        decimal maxMonthlyPayment = CalculateMonthlyIncomeMaxPercentage(loan.MonthIncome, LoanApprovalRules.LoanIncomePercentage);

        if (monthlyPayment > maxMonthlyPayment)
        {
            errors.Add($"Monthly payment should not exceed {LoanApprovalRules.LoanIncomePercentage}% of monthly income");
        }

        return new ValidationResult(string.Join("\n", errors));
    }

    static decimal CalculateMonthlyPayment(decimal loanAmount, int term, int rate)
    {
        return (loanAmount + (loanAmount * rate)) / term;
    }

    static decimal CalculateMonthlyIncomeMaxPercentage(decimal income, int maxPercentage)
    {
        return income * maxPercentage / 100;
    }
}