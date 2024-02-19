using LoansManagementSystem.Utilities;
using System.Security.Cryptography;

namespace LoansManagementSystem.Security;

public static class Hasher
{
    public static void ApplyPasswordRules(string providedCurrentPw,
        string newPw,
        string newPwConfirm,
        string currentPwHashed,
        string currentSalt,
        out string newHashedPw,
        out string newSalt)
    {
        CheckHashedPassword(providedCurrentPw, currentSalt, out string providedCurrentPwHashed);

        if (currentPwHashed != providedCurrentPwHashed)
        {
            throw new BadHttpRequestException("Invalid current password");
        }

        if (providedCurrentPw == newPw)
        {
            throw new BadHttpRequestException("New password cannot be the same as old password");
        }

        if (newPw != newPwConfirm)
        {
            throw new BadHttpRequestException("New password is not the same as its confirmation");
        }

        if (newPw.Length < 8)
        {
            throw new BadHttpRequestException($"Password must at least be 8 characters long");
        }

        bool hasUppercase = false, hasLowercase = false, hasDigit = false;

        foreach (var c in newPw)
        {
            if (char.IsUpper(c))
            {
                hasUppercase = true;
            }

            if (char.IsLower(c))
            {
                hasLowercase = true;
            }

            if (char.IsDigit(c))
            {
                hasDigit = true;
            }
        }

        if (!hasUppercase)
        {
            throw new BadHttpRequestException("Password must contain at least one upper case character");
        }

        if (!hasLowercase)
        {
            throw new BadHttpRequestException("Password must contain at least one lower case character");
        }

        if (!hasDigit)
        {
            throw new BadHttpRequestException("Password must contain at least one digit");
        }

        GetHashedPassword(newPw, out newHashedPw, out newSalt);
    }

    public static void GetHashedPassword(string providedPassword, out string hashedPassword, out string salt)
    {
        byte[] saltBytes = new byte[20];

        RandomNumberGenerator.Create().GetNonZeroBytes(saltBytes);

        salt = Convert.ToBase64String(saltBytes);

        using Rfc2898DeriveBytes hasher = new(providedPassword, saltBytes, 10000);

        hashedPassword = Convert.ToBase64String(hasher.GetBytes(20));
    }

    public static void CheckHashedPassword(string providedPassword, string providedSalt, out string hashedPassword)
    {
        using Rfc2898DeriveBytes hasher = new(providedPassword, providedSalt.Base64Decode(), 10000);

        hashedPassword = Convert.ToBase64String(hasher.GetBytes(20));
    }
}