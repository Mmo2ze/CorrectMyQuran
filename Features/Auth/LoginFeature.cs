using CorectMyQuran.Application.Interfaces.Auth;
using CorectMyQuran.DateBase;
using CorectMyQuran.DateBase.Common.Errors;
using CorectMyQuran.Features.Auth.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CorectMyQuran.Features.Auth;

public record LoginCommand(string Phone, string Password) : IRequest<ErrorOr<AuthenticationResult>>;

public class LoginCommandHandler(MainContext context,IClaimGenerator claimGenerator,IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResult>>
{

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Admins
            .FirstOrDefaultAsync(a => a.Phone == request.Phone && a.Password == request.Password, cancellationToken);
        if (user == null)
        {
            return Errors.Auth.InvalidCredentials;
        }
        var claims = await claimGenerator.GenerateClaims(user.Id.Value);
        if (claims.IsError)
        {
            return claims.Errors;
        }
        
        var result = await jwtTokenGenerator.RefreshToken(claims.Value, TimeSpan.FromDays(30), user.Id.Value, null, cancellationToken);
        return result;
    }
}