using System.Security.Cryptography;
using System.Text;

namespace TechStore.BL.Auth;
internal static class PasswordGenerator
{
    public static string Generate(string value)
    {
        var bytes = new UTF8Encoding().GetBytes(value);
        
        byte[] hashBytes = SHA512.HashData(bytes);

        return Convert.ToBase64String(hashBytes);
    }
}
