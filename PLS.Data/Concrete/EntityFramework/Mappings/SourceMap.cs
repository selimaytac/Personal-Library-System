using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLS.Entities.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Mappings;

public class SourceMap : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Property(a => a.SourceName).HasMaxLength(150);
        builder.Property(a => a.Link).HasMaxLength(500);
        builder.Property(a => a.Language).HasMaxLength(40);
        builder.Property(a => a.CreatedByName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.ModifiedByName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.CreatedDate).IsRequired();
        builder.Property(a => a.ModifiedDate).IsRequired();
        builder.Property(a => a.IsActive).IsRequired();
        builder.Property(a => a.IsDeleted).IsRequired();
        builder.Property(a => a.Note).HasMaxLength(500);
        builder.HasOne<Category>(a => a.Category).WithMany(c => c.Sources).HasForeignKey(a => a.CategoryId);
        builder.HasOne<User>(a => a.User).WithMany(u => u.Sources).HasForeignKey(a => a.UserId);
        builder.HasMany<Tag>(a => a.Tags).WithMany(c => c.Sources);
        builder.ToTable("Sources");
    }
    
    // Todo: Add data here.
}