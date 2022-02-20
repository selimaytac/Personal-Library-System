using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Services.Utilities.Abstract;
using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;
using PLS.Shared.Results.Concrete;

namespace PLS.Services.Concrete;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthUtils _authUtils;


    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IAuthUtils authUtils)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
        _authUtils = authUtils;
    }

    public async Task<IDataResult<UserDto>> GetAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetAsync(u => u.Id == userId);
        if (user != null)
            return new DataResult<UserDto>(ResultStatus.Success, new UserDto
            {
                User = user,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<UserDto>(ResultStatus.Error, "No such user was found.", null!);
    }

    public async Task<IDataResult<UserDto>> GetCurrentUserAsync(string userName)
    {
        var user = await _unitOfWork.Users.GetAsync(u => u.UserName == userName);

        if (user != null)
            return new DataResult<UserDto>(ResultStatus.Success, new UserDto
            {
                User = user,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<UserDto>(ResultStatus.Error, "No such user was found.", null!);
    }


    public async Task<IDataResult<UserListDto>> GetAllAsync(bool isDeleted = false, bool isActive = true)
    {
        var users = await _unitOfWork.Users.GetAllAsync(u => u.IsDeleted == isDeleted && u.IsActive == isActive);

        if (users.Any())
            return new DataResult<UserListDto>(ResultStatus.Success, $"{users.Count} records found.", new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<UserListDto>(ResultStatus.Error, "No records found.", null!);
    }

    public async Task<IDataResult<int>> GetUserCountAsync(bool isDeleted = false, bool isActive = true)
    {
        var userCount = await _unitOfWork.Users.GetAllAsync(u => u.IsDeleted == isDeleted && u.IsActive == isActive);

        if (userCount.Any())
            return new DataResult<int>(ResultStatus.Success, $"{userCount.Count} records found.", userCount.Count);

        return new DataResult<int>(ResultStatus.Error, "No records found.", 0);
    }

    public async Task<IResult> UpdateAsync(UserUpdateDto userUpdateDto, string updatedByUserName, string userRole)
    {
        var userExists = await _unitOfWork.Users.GetAsync(u => u.Id == userUpdateDto.Id, u => u.Role);
        if (userExists != null)
        {
            var updatedByUser = await _unitOfWork.Users.GetAsync(u => u.UserName == updatedByUserName);
            
            if(updatedByUser == null)       
                return new Result(ResultStatus.Error, "No such user was found.");
            
            switch (userRole)
            {
                case RoleTypes.Admin when userExists.Role.Name == RoleTypes.SuperAdmin:
                    return new Result(ResultStatus.Error, "Admin can not update SuperAdmin.");
                case RoleTypes.User when userExists.Id != updatedByUser.Id:
                    return new Result(ResultStatus.Error, "User can not update other users.");
            }

            var isMailUniq =
                await _unitOfWork.Users.AnyAsync(u => u.Email == userUpdateDto.Email && u.Id != userUpdateDto.Id);

            if (isMailUniq)
                return new DataResult<User>(ResultStatus.Error,
                    "User with this email already exists", null!);

            var isUserNameUniq =
                await _unitOfWork.Users.AnyAsync(u => u.UserName == userUpdateDto.UserName && u.Id != userUpdateDto.Id);

            if (isUserNameUniq)
                return new DataResult<User>(ResultStatus.Error,
                    "User with this username already exists", null!);

            var user = _mapper.Map<User>(userUpdateDto);
            _authUtils.CreatePasswordHash(userUpdateDto.Password, out var passwordHash, out var passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            user.ModifiedByName = updatedByUserName;
            user.ModifiedDate = DateTime.Now;

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{user.UserName} has been successfully updated.");
        }

        return new Result(ResultStatus.Error, $"There is no user with this id: {userUpdateDto.Id}.");
    }

    public async Task<IResult> DeleteAsync(int userId, string deletedByUserName, string userRole)
    {
        var deletedUser = await _unitOfWork.Users.GetAsync(u => u.Id == userId);

        if (deletedUser != null)
        {
            if (userRole == RoleTypes.Admin && deletedUser.Role.Name == RoleTypes.SuperAdmin)
                return new Result(ResultStatus.Error, "Admin can not delete SuperAdmin.");

            deletedUser.IsDeleted = true;
            deletedUser.ModifiedDate = DateTime.Now;
            deletedUser.ModifiedByName = deletedByUserName;

            await _unitOfWork.Users.UpdateAsync(deletedUser);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{deletedUser.UserName} has been successfully deleted.");
        }

        return new Result(ResultStatus.Error, $"There is no user with this id: {userId}.");
    }

    public async Task<IResult> RestoreDeletedAsync(int userId, string restoredByUserName)
    {
        var deletedUser = await _unitOfWork.Users.GetAsync(u => u.Id == userId);

        if (deletedUser != null)
        {
            deletedUser.IsDeleted = false;
            deletedUser.ModifiedDate = DateTime.Now;
            deletedUser.ModifiedByName = restoredByUserName;

            await _unitOfWork.Users.UpdateAsync(deletedUser);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{deletedUser.UserName} has been successfully restored.");
        }

        return new Result(ResultStatus.Error, $"There is no user with this id: {userId}.");
    }

    public async Task<IResult> HardDeleteAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetAsync(u => u.Id == userId);

        if (user != null)
        {
            await _unitOfWork.Users.DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success,
                $"{user.UserName} has been successfully deleted from the database.");
        }

        return new Result(ResultStatus.Error, $"There is no user with this id: {userId}.");
    }
}