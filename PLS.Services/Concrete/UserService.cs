using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;
using PLS.Shared.Results.Concrete;

namespace PLS.Services.Concrete;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IDataResult<UserDto>> GetAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetAsync(U => U.Id == userId, include => include.Role);
        if (user != null)
            return new DataResult<UserDto>(ResultStatus.Success, new UserDto
            {
                User = user,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<UserDto>(ResultStatus.Error, "No such user was found.", null);
    }

    public async Task<IDataResult<UserListDto>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync(null, include => include.Role);

        if (users.Any())
            return new DataResult<UserListDto>(ResultStatus.Success, $"{users.Count} records found.", new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<UserListDto>(ResultStatus.Error, "No records found.", null);
    }

    public async Task<IDataResult<UserListDto>> GetAllByNonDeletedAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync(u => !u.IsDeleted, include => include.Role);

        if (users.Any())
            return new DataResult<UserListDto>(ResultStatus.Success, $"{users.Count} records found.", new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<UserListDto>(ResultStatus.Error, "No records found.", null);
    }

    public async Task<IDataResult<UserListDto>> GetAllByNonDeletedAndActiveAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync(u => !u.IsDeleted && u.IsActive, include => include.Role);

        if (users.Any())
            return new DataResult<UserListDto>(ResultStatus.Success, $"{users.Count} records found.", new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<UserListDto>(ResultStatus.Error, "No records found.", null);
    }

    public async Task<IResult> UpdateAsync(UserUpdateDto userUpdateDto, string updatedByUserName)
    {
        var user = _mapper.Map<User>(userUpdateDto);
        user.ModifiedByName = updatedByUserName;
        user.ModifiedDate = DateTime.Now;
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, $"{user.UserName} has been successfully updated.");
    }

    public async Task<IResult> DeleteAsync(int userId, string deletedByUserName)
    {
        var userExist = await _unitOfWork.Users.AnyAsync(u => u.Id == userId);
        if (userExist)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Id == userId);
            user.IsDeleted = true;
            user.ModifiedDate = DateTime.Now;
            user.ModifiedByName = deletedByUserName;

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{user.UserName} has been successfully deleted.");
        }

        return new Result(ResultStatus.Error, $"There is no user with this id: {userId}.");
    }

    public async Task<IResult> HardDeleteAsync(int userId)
    {
        var userExist = await _unitOfWork.Users.AnyAsync(u => u.Id == userId);

        if (userExist)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Id == userId);
            await _unitOfWork.Users.DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success,
                $"{user.UserName} has been successfully deleted from the database.");
        }

        return new Result(ResultStatus.Error, $"There is no user with this id: {userId}.");
    }
}