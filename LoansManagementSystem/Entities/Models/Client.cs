namespace LoansManagementSystem.Entities.Models;

public class Client : User
{
    public Client()
    {
        Loans = new HashSet<LoanApplication>();
    }

    public string AccountNumber { get; set; } = null!;

    public virtual ICollection<LoanApplication> Loans { get; set; }
}