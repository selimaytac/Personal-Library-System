using PLS.Data.Abstract;
using PLS.Data.Concrete.EntityFramework.Contexts;
using PLS.Data.Concrete.EntityFramework.Repositories;

namespace PLS.Data.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly PLSContext _context;
    private EFSourceRepository _sourceRepository;
    private EFCategoryRepository _categoryRepository;
    private EFRoleRepository _roleRepository;
    private EFUserRepository _userRepository;
    private EFTagRepository _tagRepository;

    public UnitOfWork(PLSContext context)
    {
        _context = context;
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public IUserRepository Users => _userRepository ?? new EFUserRepository(_context);
    public ICategoryRepository Categories => _categoryRepository?? new EFCategoryRepository(_context);
    public ISourceRepository Sources => _sourceRepository ?? new EFSourceRepository(_context);
    public IRoleRepository Roles => _roleRepository ?? new EFRoleRepository(_context);
    public ITagRepository Tags => _tagRepository ?? new EFTagRepository(_context);

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}