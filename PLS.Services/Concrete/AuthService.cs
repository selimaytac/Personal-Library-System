using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Entities.Configures;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Services.Utilities.Abstract;
using PLS.Shared.Helpers;
using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;
using PLS.Shared.Results.Concrete;

namespace PLS.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IOptions<AppSettings> appSettings, IMapper mapper, IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
    {
        _appSettings = appSettings.Value;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _jwtUtils = jwtUtils;
    }

    public async Task<IDataResult<User>> RegisterAsync(UserAddDto userAddDto)
    {
        CreatePasswordHash(userAddDto.Password, out var passwordHash, out var passwordSalt);

        var user = _mapper.Map<User>(userAddDto);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveAsync();

        return new DataResult<User>(ResultStatus.Success, $"The user {user.UserName} has been successfully created.",
            user);
    }

    public async Task<IDataResult<UserDto>> GetAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetAsync(u => u.Id == userId, include => include.Role);

        if (user != null)
        {
            return new DataResult<UserDto>(ResultStatus.Success, new UserDto
            {
                User = user,
                ResultStatus = ResultStatus.Success
            });
        }

        return new DataResult<UserDto>(ResultStatus.Error, "User not found.", null);
    }

    public async Task<bool> UserExists(string username)
    {
        return await _unitOfWork.Users.AnyAsync(x => x.UserName == username);
    }

    public async Task<IDataResult<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request)
    {
        var user = await _unitOfWork.Users.GetAsync(x => x.UserName == request.Username);

        if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new DataResult<AuthenticateResponse>(ResultStatus.Error, "Wrong username or password.", null);
        }

        var jwtToken = _jwtUtils.GenerateJwtToken(user);

        return new DataResult<AuthenticateResponse>(ResultStatus.Success, "Successfully authorized.", new AuthenticateResponse{User = user, Token = jwtToken});
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();

        passwordSalt = hmac.Key;

        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
    }
}