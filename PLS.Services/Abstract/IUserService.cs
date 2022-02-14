using PLS.Entities.Dtos;
using PLS.Shared.Results.Abstract;

namespace PLS.Services.Abstract;

public interface IUserService
{
    Task<IDataResult<UserDto>> GetAsync(int userId);
    Task<IDataResult<UserListDto>> GetAllAsync();
    Task<IDataResult<UserListDto>> GetAllByNonDeletedAsync();
    Task<IDataResult<UserListDto>> GetAllByNonDeletedAndActiveAsync();
    Task<IResult> UpdateAsync(UserUpdateDto userUpdateDto, string updatedByUserName);
    Task<IResult> DeleteAsync(int userId, string deletedByUserName);
    Task<IResult> HardDeleteAsync(int userId);
}