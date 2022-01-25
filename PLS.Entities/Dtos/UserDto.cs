using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class UserDto : DtoGetBase
{
    public User User { get; set; }
}