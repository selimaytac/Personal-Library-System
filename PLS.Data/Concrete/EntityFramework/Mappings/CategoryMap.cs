using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLS.Entities.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Mappings;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(70);
        builder.Property(c => c.Description).HasMaxLength(500);
        builder.Property(c => c.CreatedByName).IsRequired().HasMaxLength(50);
        builder.Property(c => c.ModifiedByName).IsRequired().HasMaxLength(50);
        builder.Property(c => c.CreatedDate).IsRequired();
        builder.Property(c => c.ModifiedDate).IsRequired();
        builder.Property(c => c.IsActive).IsRequired();
        builder.Property(c => c.IsDeleted).IsRequired();
        builder.Property(c => c.Note).HasMaxLength(500);
        builder.ToTable("Categories");

        builder.HasData(new Category
        {
            Id = 1,
            Description = ".NET Tech",
            Name = ".NET",
            CreatedByName = "Initial",
            CreatedDate = DateTime.Now,
            ModifiedByName = "Initial",
            ModifiedDate = DateTime.Now,
            IsActive = true,
            IsDeleted = false,
            Note = "Initial"
        });
    }
    
    
}