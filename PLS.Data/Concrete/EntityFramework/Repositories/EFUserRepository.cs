using Microsoft.EntityFrameworkCore;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Shared.Data.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Repositories;

public class EFUserRepository : EntityRepositoryBase<User>, IUserRepository 
{
    public EFUserRepository(DbContext context) : base(context)
    {
    }
}