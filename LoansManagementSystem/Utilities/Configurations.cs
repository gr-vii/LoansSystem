using Microsoft.Extensions.Caching.Memory;

namespace LoansManagementSystem.Utilities;

public class Configurations
{
    public const string CacheKeyUsersJwtExpiration = "JwtExpirey";
    public const string CacheKeyUsersTsk = "TSK";

    public required int MaxFailedLogins { get; set; }
    public required int LoanMaxValue { get; set; }
    public required int LoanMaxMonthsTerm { get; set; }
    public required int LoanIncomePercentage { get; set; }
    public required int LoanInterestRate { get; set; }
    public required string RabbitMqHostName { get; set; }
    public required string RabbitMqUserName { get; set; }
    public required string RabbitMqPassword { get; set; }
    public required string RabbitMqVirtualHost { get; set; }

    public static void Reinit(IMemoryCache cache, string usersTsk, int jwtExpiration)
    {
        cache.Set(CacheKeyUsersJwtExpiration, jwtExpiration);

        if (!string.IsNullOrEmpty(usersTsk))
        {
            cache.Set(CacheKeyUsersTsk, usersTsk);
        }
    }
}