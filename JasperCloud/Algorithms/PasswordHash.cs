using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JasperCloud.Algorithms;

public class Password
{
    public static (string, string) Hash(string password, string? saltString = null)
    {
        byte[] salt;

        if (saltString == null) 
        {
            salt = RandomNumberGenerator.GetBytes(128 / 8); 
        }
        else
        {
            salt = Convert.FromBase64String(saltString);
        }

    string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password!,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8));

        return (hash, Convert.ToBase64String(salt));
    }

    public static bool CheckMatch(string inputPassword, string passwordHash, string salt)
    {
        var (inputPasswordHash, _) = Hash(inputPassword, salt);

        return inputPasswordHash == passwordHash;
    }
}