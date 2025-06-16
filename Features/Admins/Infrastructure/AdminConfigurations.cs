using CorectMyQuran.Features.Admins.Domain;
using CorectMyQuran.Features.Auth.Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorectMyQuran.Features.Admins.Infrastructure;

public class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => AdminId.Create(x));
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        // relations 
        builder.HasMany<RefreshToken>()
            .WithOne()
            .HasForeignKey(x => x.AdminId);

        //index
        builder.HasIndex(x => x.Phone).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}