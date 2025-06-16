using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.Features.Auth.Domain.RefreshTokens;

public record RefreshTokenId(string Value) : ValueObjectId<RefreshTokenId>(Value)
{
    public RefreshTokenId() : this(Guid.CreateVersion7().ToString())
    {
    }
}