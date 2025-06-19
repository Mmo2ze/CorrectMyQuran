using System.Security.Cryptography;
using System.Text;

namespace CorectMyQuran.Common;

public static class RandomPassword
{
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";

    public static string Generate(int length = 12)
    {
        var password = new StringBuilder();
        using var rng = RandomNumberGenerator.Create();
        var buffer = new byte[sizeof(uint)];

        for (int i = 0; i < length; i++)
        {
            rng.GetBytes(buffer);
            uint num = BitConverter.ToUInt32(buffer, 0);
            password.Append(AllowedChars[(int)(num % (uint)AllowedChars.Length)]);
        }

        return password.ToString();
    }
}