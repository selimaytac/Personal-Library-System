namespace PLS.Services.Utilities.Abstract;

public interface IAuthUtils
{
     void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
     bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
}