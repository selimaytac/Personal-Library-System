using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;
using PLS.Shared.Results.Abstract;

namespace PLS.Entities.Dtos;

public class AuthenticateResponse : DtoGetBase
{
    public User User { get; set; }
    public string Token { get; set; }
}