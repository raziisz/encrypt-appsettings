using System.Security.Cryptography;
using System.Text;
public static class ConfigurationSecret
{
    
    public static string Decrypt(string valueEncrypted)
    {
        try
        {
#pragma warning disable CA1416 // Validate platform compatibility
            var decryptedData =  ProtectedData.Unprotect(
                Convert.FromBase64String(valueEncrypted ?? string.Empty),
                Encoding.UTF8.GetBytes("12345"),
                DataProtectionScope.LocalMachine
            );
#pragma warning restore CA1416 // Validate platform compatibility

            return Encoding.UTF8.GetString(decryptedData);
        }
        catch 
        {
            
            return string.Empty;
        }
    }

    public static string Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "";
        }
#pragma warning disable CA1416 // Validate platform compatibility
        var encrypted = ProtectedData.Protect(
            Encoding.UTF8.GetBytes(value),
            Encoding.UTF8.GetBytes("12345"),
            DataProtectionScope.LocalMachine
        );
#pragma warning restore CA1416 // Validate platform compatibility
        return Convert.ToBase64String(encrypted);
    }
}