using CorectMyQuran.Features.Auth.Domain.RefreshTokens;
using CorectMyQuran.Features.Sheikh.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorectMyQuran.Features.Sheikh.Infrastructure;

public class SheikhConfigurations : IEntityTypeConfiguration<CorectMyQuran.Features.Sheikh.Domain.Sheikh>
{


    public void Configure(EntityTypeBuilder<Sheikh.Domain.Sheikh> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => SheikhId.Create(x));
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        // relations 
        builder.HasMany<RefreshToken>()
            .WithOne()
            .HasForeignKey(x => x.SheikhId);

        //index
        builder.HasIndex(x => x.Phone).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}