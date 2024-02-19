using LoansManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LoansManagementSystem.DataServices.Contexts;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options) { }

    public virtual DbSet<Client> Client { get; set; }
    public virtual DbSet<Administrator> Administrator { get; set; }
    public virtual DbSet<LoanApplication> LoanApplication { get; set; }
    public virtual DbSet<LoanPayment> LoanPayment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LoanApplication>(e =>
        {
            e.HasOne(c => c.Client)
                .WithMany(l => l.Loans)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Loans_Client");
        });

        modelBuilder.Entity<LoanPayment>(e =>
        {
            e.HasOne(c => c.Loan)
                .WithMany(l => l.LoanPayment)
                .HasForeignKey(c => c.LoanId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Loans_LoansPayment");
        });
    }
}