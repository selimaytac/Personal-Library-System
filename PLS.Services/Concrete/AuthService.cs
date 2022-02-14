using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Services.Utilities.Abstract;
using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;
using PLS.Shared.Results.Concrete;

namespace PLS.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
    {
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _jwtUtils = jwtUtils;
    }

    public async Task<IDataResult<User>> RegisterAsync(UserAddDto userAddDto)
    {
        var isMailUniq = await _unitOfWork.Users.AnyAsync(u => u.Email == userAddDto.Email);

        if (isMailUniq)
            return new DataResult<User>(ResultStatus.Error,
                "User with this email already exists", null);

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
        var user = await _unitOfWork.Users.GetAsync(u => u.Id == userId);

        if (user != null)
        {
            return new DataResult<UserDto>(ResultStatus.Success,
                $"The user {user.UserName} has been successfully found.",
                new UserDto
                {
                    User = user,
                    ResultStatus = ResultStatus.Success,
                });
        }

        return new DataResult<UserDto>(ResultStatus.Error, "User not found.", null);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _unitOfWork.Users.AnyAsync(x => x.UserName == username);
    }

    public async Task<IDataResult<string>> AuthenticateAsync(AuthenticateRequest request)
    {
        var user = await _unitOfWork.Users.GetAsync(x => x.UserName == request.Username, y => y.Role);

        if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new DataResult<string>(ResultStatus.Error, "Wrong username or password.", null);
        }

        var jwtToken = _jwtUtils.GenerateJwtToken(user);

        return new DataResult<string>(ResultStatus.Success, "Successfully authorized.", jwtToken);
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

        return computedHash.SequenceEqual(passwordHash);
    }
}