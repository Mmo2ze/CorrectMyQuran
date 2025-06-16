using System.Security.Claims;

namespace CorectMyQuran.Application.Interfaces.Auth;

public interface IClaimGenerator
{
    Task<ErrorOr<List<Claim>>> GenerateClaims(string userId);
}