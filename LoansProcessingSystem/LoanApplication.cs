namespace LoansProcessingSystem;

public class LoanApplication
{
    public string Id { get; set; } = null!;
    public int MonthIncome { get; set; }
    public int Term { get; set; }
    public int Amount { get; set; }
}