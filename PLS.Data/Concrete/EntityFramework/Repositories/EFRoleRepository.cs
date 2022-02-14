using Microsoft.EntityFrameworkCore;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Shared.Data.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Repositories;

public class EFRoleRepository : EntityRepositoryBase<Role>, IRoleRepository
{
    public EFRoleRepository(DbContext context) : base(context)
    {
    }
}