using CorectMyQuran.Application.Behaviors;
using CorectMyQuran.Application.Interfaces.Auth;
using CorectMyQuran.Application.Services;
using CorectMyQuran.DateBase.Interceptors;
using CorectMyQuran.Services;
using CorectMyQuran.Services.Auth;
using FluentValidation;
using MediatR;

namespace CorectMyQuran;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(UnitOfWorkBehavior<,>)
        );

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<ICookieManger, CookieManger>();
        services.AddScoped<IClaimGenerator, ClaimGenerator>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IClaimsReader, ClaimsReader>();
        
    }
}