using PLS.Entities.Concrete;

namespace PLS.Services.Utilities.Abstract;

public interface IJwtUtils
{
    public string GenerateJwtToken(User user);
    public int? ValidateJwtToken(string token);
}