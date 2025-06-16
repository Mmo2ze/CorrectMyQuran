using System.Security.Claims;
using CorectMyQuran.Features.Auth.Common;
using CorectMyQuran.Features.Auth.Domain.RefreshTokens;

namespace CorectMyQuran.Application.Interfaces.Auth;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(List<Claim> claims, DateTime expireTime, string? userId);
    string GenerateJwtToken(List<Claim> claims, TimeSpan period, string? userId);

    Task<ErrorOr<AuthenticationResult>> RefreshToken(List<Claim> claims, TimeSpan expireTime,
        string userId,
        RefreshToken? oldToken, CancellationToken cancellationToken = default);
}