using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class UserListDto : DtoGetBase
{
    public ICollection<User> Users { get; set; }
}