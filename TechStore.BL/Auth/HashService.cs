using System.Security.Cryptography;
using System.Text;

namespace TechStore.BL.Auth;

internal static class HashService
{
    public static string Compute(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var bytes = new UTF8Encoding().GetBytes(value);
        
        byte[] hashBytes = SHA512.HashData(bytes);

        return Convert.ToBase64String(hashBytes);
    }
}
