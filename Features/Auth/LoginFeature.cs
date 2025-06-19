using CorectMyQuran.Application.Interfaces.Auth;
using CorectMyQuran.DateBase;
using CorectMyQuran.DateBase.Common.Errors;
using CorectMyQuran.Features.Auth.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CorectMyQuran.Features.Auth;

public record LoginCommand(string Phone, string Password) : IRequest<ErrorOr<AuthenticationResult>>;

public class LoginCommandHandler(
    MainContext context,
    IClaimGenerator claimGenerator,
    IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Admins
            .FirstOrDefaultAsync(a => a.Phone == request.Phone && a.Password == request.Password, cancellationToken);
        string? userId = null;
        if (user == null)
        {
            var sheikh = await context.Sheikhs.FirstOrDefaultAsync(a => a.Phone == request.Phone, cancellationToken);
            if (sheikh is not null)
                if (sheikh.VerifyPassword(request.Password))
                    userId = sheikh.Id.Value;
        }
        else
            userId = user.Id.Value;

        if(userId is null)
        {
            return Errors.Auth.InvalidCredentials;
        }
        var claims = await claimGenerator.GenerateClaims(userId);
        if (claims.IsError)
        {
            return claims.Errors;
        }

        var result = await jwtTokenGenerator.RefreshToken(claims.Value, TimeSpan.FromDays(30), userId, null,
            cancellationToken);
        return result;
    }
}