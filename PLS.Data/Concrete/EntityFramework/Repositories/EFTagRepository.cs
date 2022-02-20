using Microsoft.EntityFrameworkCore;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Shared.Data.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Repositories;

public class EFTagRepository :  EntityRepositoryBase<Tag>, ITagRepository
{
    public EFTagRepository(DbContext context) : base(context)
    {
    }
}