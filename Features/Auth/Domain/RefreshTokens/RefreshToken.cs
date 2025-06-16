using CorectMyQuran.Features.Admins.Domain;
using CorectMyQuran.Features.Sheikh.Domain;
using CorectMyQuran.Features.Student.Domain;

namespace CorectMyQuran.Features.Auth.Domain.RefreshTokens;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    
    public Guid TokenId { get; set; }
    public DateTime Expires { get; set; }
    public bool IsActive => DateTime.UtcNow < Expires;
    public DateTime CreatedOn { get; set; }

    public TimeSpan Duration => Expires - CreatedOn;
    public AdminId? AdminId { get; set; } 
    public SheikhId? SheikhId { get; set; } 
    public StudentId? StudentId{ get; set; } 
}