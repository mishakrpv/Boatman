namespace Boatman.DataAccess.Identity.Interfaces;

public interface IPasswordManager
{
    string GetNewSalt();

    string GetPasswordHash(string password, string salt);
}