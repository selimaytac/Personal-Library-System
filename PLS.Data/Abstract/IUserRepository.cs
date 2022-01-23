using PLS.Entities.Concrete;
using PLS.Shared.Data.Abstract;

namespace PLS.Data.Abstract;

public interface IUserRepository : IEntityRepository<User>
{
}