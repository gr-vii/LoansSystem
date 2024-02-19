namespace LoansManagementSystem.Utilities;

public static class Extensions
{
    public static (bool, string) VerifyAndCorrectPhone(this string phone)
    {
        try
        {
            string rawPhone = phone[phone.IndexOf('7')..];

            if (rawPhone.Length != 10 || rawPhone is null)
            {
                throw new Exception("Invalid Phone Number!");
            }

            return (true, "964" + rawPhone);

        }

        catch (ArgumentOutOfRangeException)
        {
            return (false, null);
        }
    }

    public static byte[] Base64Decode(this string s)
    {
        var paddingLength = 4 - s.Length % 4;

        if (paddingLength < 4)
        {
            s += new string('=', paddingLength);
        }

        return Convert.FromBase64String(s);
    }
}

