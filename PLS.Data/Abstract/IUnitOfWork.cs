namespace PLS.Data.Abstract;

public interface IUnitOfWork : IAsyncDisposable
{
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    ISourceRepository Sources { get; }
    IRoleRepository Roles { get; }
    ITagRepository Tags { get; }
    Task<int> SaveAsync();
}