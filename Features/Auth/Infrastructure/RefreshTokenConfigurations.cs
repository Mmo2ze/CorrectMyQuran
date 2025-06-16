using CorectMyQuran.Features.Auth.Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorectMyQuran.Features.Auth.Infrastructure;

public class RefreshTokenConfigurations : IEntityTypeConfiguration<CorectMyQuran.Features.Auth.Domain.RefreshTokens.RefreshToken>
{


    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.TokenId);

    }
}