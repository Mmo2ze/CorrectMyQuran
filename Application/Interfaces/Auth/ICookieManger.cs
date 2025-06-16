namespace CorectMyQuran.Application.Interfaces.Auth;

public interface ICookieManger
{
	void SetProperty(string key, object value,TimeSpan period);
	void SetProperty(string key, object value,DateTime date);
	string? GetPropertyByClaimType(string key);
	string GetProperty(string key);
	void Clear();
}