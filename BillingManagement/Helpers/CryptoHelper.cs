using System.Text;
using System.Security.Cryptography;

namespace BillingManagement.Helpers;

public static class CryptoHelper
{
    private static readonly byte[] _salt = Encoding.ASCII.GetBytes("MySecretKey");

    public static string EncryptPassword(string password)
    {
        using var sha256 = new SHA256Managed();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPasswordBytes = AddSalt(passwordBytes);

        var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
        var hashString = Convert.ToBase64String(hashBytes);

        return hashString;
    }

    public static bool DecryptPassword(string attemptedPassword, string actualPasswordHash)
    {
        var hashedAttemptedPassword = EncryptPassword(attemptedPassword);
        return actualPasswordHash.Equals(hashedAttemptedPassword);
    }

    private static byte[] AddSalt(byte[] value)
    {
        var saltedValue = new byte[value.Length + _salt.Length];
        Array.Copy(value, saltedValue, value.Length);
        Array.Copy(_salt, 0, saltedValue, value.Length, _salt.Length);
        return saltedValue;
    }
}
