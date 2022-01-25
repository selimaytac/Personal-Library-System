using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Shared.Results.Abstract;

namespace PLS.Services.Abstract;

public interface IAuthService
{
    Task<IDataResult<User>> RegisterAsync(UserAddDto userAddDto);
    Task<IDataResult<UserDto>> GetAsync(int userId);
    Task<bool> UserExists(string username);
    Task<IDataResult<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request);
}