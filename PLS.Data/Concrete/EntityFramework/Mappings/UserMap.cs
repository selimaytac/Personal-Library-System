using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLS.Entities.Concrete;

namespace PLS.Data.Concrete.EntityFramework.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    private byte[] passwordHash;
    private byte[] passwordSalt;

    public UserMap()
    {
        using var hmac = new HMACSHA512();

        passwordSalt = hmac.Key;

        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("random123."));
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(70);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
        builder.Property(u => u.CreatedByName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.ModifiedByName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.CreatedDate).IsRequired();
        builder.Property(u => u.ModifiedDate).IsRequired();
        builder.Property(u => u.IsActive).IsRequired();
        builder.Property(u => u.IsDeleted).IsRequired();
        builder.Property(u => u.Note).HasMaxLength(500);
        builder.ToTable("Users");

        builder.HasData(new User
        {
            Id = 1,
            UserName = "FirstUser",
            Email = "testmail@gmail.com",
            RoleId = 1,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsActive = true,
            IsDeleted = false,
            CreatedByName = "Initial",
            ModifiedByName = "Initial"
        });
    }
}