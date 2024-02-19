namespace LoansManagementSystem.Entities.Models;

public class Base
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime InsDate { get; set; } = DateTime.UtcNow;
    public DateTime LupDate { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true;
}