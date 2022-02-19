using PLS.Entities.Dtos;
using PLS.Shared.Results.Abstract;

namespace PLS.Services.Abstract;

public interface IUserService
{
    Task<IDataResult<UserDto>> GetAsync(int userId);
    Task<IDataResult<UserDto>> GetCurrentUserAsync(string userName);
    Task<IDataResult<UserListDto>> GetAllAsync(bool isDeleted = false, bool isActive = true);
    Task<IDataResult<int>> GetUserCountAsync(bool isDeleted = false, bool isActive = true);
    Task<IResult> UpdateAsync(UserUpdateDto userUpdateDto, string updatedByUserName, string userRole);
    Task<IResult> DeleteAsync(int userId, string deletedByUserName);
    Task<IResult> RestoreDeletedAsync(int userId, string restoredByUserName);
    Task<IResult> HardDeleteAsync(int userId);
}