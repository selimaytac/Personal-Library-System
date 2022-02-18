using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLS.Entities.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Mappings;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
        builder.Property(r => r.TagName).IsRequired().HasMaxLength(50);
        builder.Property(r => r.TagDescription).HasMaxLength(250);
        builder.Property(r => r.CreatedByName).IsRequired().HasMaxLength(50);
        builder.Property(r => r.ModifiedByName).IsRequired().HasMaxLength(50);
        builder.Property(r => r.CreatedDate).IsRequired();
        builder.Property(r => r.ModifiedDate).IsRequired();
        builder.Property(r => r.IsActive).IsRequired();
        builder.Property(r => r.IsDeleted).IsRequired();
        builder.Property(r => r.Note).HasMaxLength(500);
        builder.ToTable("Tags");

        builder.HasData(new Tag
        {
            Id = 1,
            TagName = ".NET",
            TagDescription =
                ".NET is a free and open-source, managed computer software framework for Windows, Linux, and macOS operating systems.",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsActive = true,
            IsDeleted = false,
            CreatedByName = "Initial",
            ModifiedByName = "Initial"
        });
    }
}