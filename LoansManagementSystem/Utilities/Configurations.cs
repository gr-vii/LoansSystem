namespace LoansManagementSystem.Utilities;

public class Configurations
{
    public required int MaxFailedLogins { get; set; }
    public required int OtpExpiration { get; set; }
    public required int LoanMaxValue { get; set; }
    public required int LoanMaxMonthsTerm { get; set; }
    public required int LoanIncomePercentage { get; set; }
    public required int LoanInterestRate { get; set; }
    public required string RabbitMqHostName { get; set; }
    public required string RabbitMqUserName { get; set; }
    public required string RabbitMqPassword { get; set; }
    public required string RabbitMqVirtualHost { get; set; }
}