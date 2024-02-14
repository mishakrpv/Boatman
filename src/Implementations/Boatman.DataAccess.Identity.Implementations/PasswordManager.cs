using System.Security.Cryptography;
using System.Text;
using Boatman.DataAccess.Identity.Interfaces;

namespace Boatman.DataAccess.Identity.Implementations;

public class PasswordManager : IPasswordManager
{
    private readonly Random _random = new();

    public string GetNewSalt()
    {
        var buffer = new byte[32];
        _random.NextBytes(buffer);

        return Encoding.UTF8.GetString(buffer);
    }

    public string GetPasswordHash(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{password}:{salt}"));

            return Encoding.UTF8.GetString(hash);
        }
    }
}