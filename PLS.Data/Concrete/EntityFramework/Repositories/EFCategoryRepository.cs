using Microsoft.EntityFrameworkCore;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Shared.Data.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Repositories;

public class EFCategoryRepository : EntityRepositoryBase<Category>, ICategoryRepository
{
    public EFCategoryRepository(DbContext context) : base(context)
    {
    }
}