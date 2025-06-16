namespace CorectMyQuran.Application.Services;

public interface IClaimsReader
{
	string? GetByClaimType(string claimType);
	List<string> GetRoles();
	string? GetUserId();
}