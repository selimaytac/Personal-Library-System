using Microsoft.EntityFrameworkCore;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Shared.Data.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Repositories;

public class EFSourceRepository : EntityRepositoryBase<Source>, ISourceRepository
{
    public EFSourceRepository(DbContext context) : base(context)
    {
    }
}