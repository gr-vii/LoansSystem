using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoansManagementSystem.Security;

public class Jwt : IJwt
{
    public string UserId { get; set; }
    public string UserRole { get; set; }
}

public static class JwtHelper
{
    public static string GetJwt(string tsk, DateTime expiry, List<Claim> userClaims)
    {
        return new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken(
                "LMS",
                "LoansManagementSystem",
                userClaims,
                expires: expiry,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(tsk)), SecurityAlgorithms.HmacSha256)));
    }

    public static List<Claim> SetClaims(Guid userId, string userRole)
    {
        List<Claim> tokenClaims = new()
        {
            new Claim("UserId", userId.ToString()),
            new Claim("UserRole", userRole),
        };

        return tokenClaims;
    }
}