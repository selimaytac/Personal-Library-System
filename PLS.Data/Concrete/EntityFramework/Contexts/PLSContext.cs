using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using PLS.Data.Concrete.EntityFramework.Mappings;
using PLS.Entities.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Contexts;

public class PLSContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<Source>? Sources { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Tag>? Tags { get; set; }

    public PLSContext(DbContextOptions<PLSContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SourceMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new TagMap());
    }
}