using System.Security.Claims;
using CorectMyQuran.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace CorectMyQuran.Services;

[Authorize]
public class ClaimsReader : IClaimsReader
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsReader(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetByClaimType(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }

    public List<string> GetRoles()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
            .ToList() ?? [];
    }

    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.First(c => c.Type == "Id").Value;
    }
}