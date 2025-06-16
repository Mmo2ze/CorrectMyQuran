using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CorectMyQuran.Application.Extensions;
using CorectMyQuran.Application.Interfaces.Auth;
using CorectMyQuran.Application.Services;
using CorectMyQuran.Application.Variables;
using CorectMyQuran.DateBase;
using CorectMyQuran.DateBase.Common.Errors;
using CorectMyQuran.Features.Admins.Domain;
using CorectMyQuran.Features.Auth.Common;
using CorectMyQuran.Features.Auth.Domain.RefreshTokens;
using CorectMyQuran.Features.Sheikh.Domain;
using CorectMyQuran.Features.Student.Domain;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Role = CorectMyQuran.Features.Auth.Common.Role;

namespace CorectMyQuran.Services.Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICookieManger _cookieManger;
    private readonly MainContext _dbContext;
    private readonly IDistributedCache _cache;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions,
        ICookieManger cookieManger, MainContext dbContext, IDistributedCache cache)
    {
        _dateTimeProvider = dateTimeProvider;
        _cookieManger = cookieManger;
        _dbContext = dbContext;
        _cache = cache;
    }

    public string GenerateJwtToken(List<Claim> claims, DateTime expireTime,
        string? userId)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
        var jwtExpireMinutes = JwtSettings.ExpireMinutes;
        var jwtExpireTime = DateTime.UtcNow.AddMinutes(jwtExpireMinutes);
        var securityToken = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            claims: claims,
            audience: JwtSettings.Audience,
            expires: jwtExpireTime,
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateJwtToken(List<Claim> claims, TimeSpan period, string userId)
    {
        return GenerateJwtToken(claims, _dateTimeProvider.Now.Add(period), userId);
    }

    public async Task<ErrorOr<AuthenticationResult>> RefreshToken(List<Claim> claims, TimeSpan expireTime,
        string userId, RefreshToken? oldToken, CancellationToken cancellationToken = default)
    {

        var baseRefreshToken = GenerateRefreshToken(expireTime);
        Role role;
        if (claims.Any(x => x.Value == DateBase.Common.Enums.Role.CodeSent.ToString()))
            role = Role.CodeSent;
        else if (AdminId.IsValidId(userId))
        {
            var adminId = AdminId.Create(userId);
            baseRefreshToken.AdminId = adminId;
            role = Role.Admin;
        }
        else if (SheikhId.IsValidId(userId))
        {
            var sheikhId = SheikhId.Create(userId);
            baseRefreshToken.SheikhId = sheikhId;
            role = Role.Sheikh;
        }else if (StudentId.IsValidId(userId))
        {
            var studentId = StudentId.Create(userId);
            baseRefreshToken.StudentId = studentId;
            role = Role.Student;
        }
        else
        {
            return Errors.Auth.InvalidCredentials;
        }
        var token = GenerateJwtToken(claims, TimeSpan.FromMinutes(JwtSettings.ExpireMinutes), userId);
        await using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                _dbContext.RefreshTokens.Add(baseRefreshToken);
                if (oldToken is not null)
                {
                    _dbContext.Remove(oldToken);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                if (oldToken != null) await _cache.RemoveAsync(oldToken.Token, cancellationToken);
                await _cache.SetRecordAsync(baseRefreshToken.Token, baseRefreshToken, baseRefreshToken.Duration);
            }
            catch (OperationCanceledException ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                if (oldToken is not null)
                    _cookieManger.SetProperty(CookieVariables.RefreshToken, oldToken.Token, expireTime);
                _cookieManger.SetProperty(CookieVariables.Id, userId, expireTime);
                _cookieManger.SetProperty(CookieVariables.Autorized,
                    role.ToString() == DateBase.Common.Enums.Role.CodeSent.ToString() ? "false" : "true", expireTime);
                Console.WriteLine("request was canceled" + ex.Message);
                return Error.Unexpected("Error while saving refresh token.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                if (oldToken is not null)
                    _cookieManger.SetProperty(CookieVariables.RefreshToken, oldToken.Token, expireTime);
                _cookieManger.SetProperty(CookieVariables.Id, userId, expireTime);
                _cookieManger.SetProperty(CookieVariables.Autorized,
                    role.ToString() == DateBase.Common.Enums.Role.CodeSent.ToString() ? "false" : "true", expireTime);
                Console.WriteLine("saving refresh token failed. " + ex.Message);
                return Error.Unexpected("Error while saving refresh token.");
            }
        }


        _cookieManger.SetProperty(CookieVariables.RefreshToken, baseRefreshToken.Token, expireTime);
        _cookieManger.SetProperty(CookieVariables.Id, userId, expireTime);
        _cookieManger.SetProperty(CookieVariables.Autorized,
            role.ToString() == DateBase.Common.Enums.Role.CodeSent.ToString() ? "false" : "true", expireTime);

        return new AuthenticationResult(token, baseRefreshToken.Expires, role);
    }

    private RefreshToken GenerateRefreshToken(TimeSpan expireTime)
    {
        var randomNumber = new byte[32];

        using var generator = new RNGCryptoServiceProvider();

        generator.GetBytes(randomNumber);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            Expires = DateTime.UtcNow.Add(expireTime),
            CreatedOn = DateTime.UtcNow,
            TokenId = Guid.NewGuid()
        };
    }
}