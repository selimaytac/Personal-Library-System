using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Shared.Results.Abstract;

namespace PLS.Services.Abstract;

public interface IAuthService
{
    Task<IDataResult<User>> RegisterAsync(UserAddDto userAddDto);
    Task<IDataResult<UserDto>> GetAsync(int userId);
    Task<bool> UserExistsAsync(string username);
    Task<IDataResult<string>> AuthenticateAsync(AuthenticateRequest request);
}