namespace LoansManagementSystem.Entities.Models;

public class LoanApplication : Base
{
    public LoanApplication()
    {
        LoanPayment = new HashSet<LoanPayment>();
    }

    public string FullName { get; set; } = null!;
    public string EmploymentType { get; set; } = null!;
    public int MonthIncome { get; set; }
    public int Term { get; set; }
    public int Amount { get; set; }
    public string Purpose { get; set; } = null!;
    public Guid ClientId { get; set; }
    public bool ApprovalStatus { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<LoanPayment> LoanPayment { get; set; }
}