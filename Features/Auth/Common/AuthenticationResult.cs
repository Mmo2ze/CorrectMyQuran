namespace CorectMyQuran.Features.Auth.Common;

public record AuthenticationResult(
    string Token,
    DateTime ExpireDate,Role Role,bool IsRegistered =true);
public enum Role
{
    Admin,
    Sheikh,
    Student,
    CodeSent
}