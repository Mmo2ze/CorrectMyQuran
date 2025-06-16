using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CorectMyQuran;
using CorectMyQuran.Application.Interfaces.Auth;
using CorectMyQuran.Common;
using CorectMyQuran.DateBase;
using CorectMyQuran.DateBase.Interceptors;
using CorectMyQuran.Services.Auth;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

builder.Services.AddEndpointsApiExplorer();


Env.Load(".env");
builder.Services.AddDbContext<MainContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    options.UseNpgsql(connectionString);
    // .EnableSensitiveDataLogging()
    // .EnableDetailedErrors();
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = "CorrectMyQuran:";
    options.ConfigurationOptions = new ConfigurationOptions
    {
        Password = Environment.GetEnvironmentVariable("REDIS_PASSWORD"),
        Ssl = true,
        SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
        EndPoints = { Environment.GetEnvironmentVariable("REDIS_ENDPOINT")! }
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});
builder.Services
    .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = JwtSettings.Audience,
        ValidIssuer = JwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret))
    });

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


builder.Services.AddScoped<PublishDomainEventsInterceptor>();


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});
builder.Services.AddCors(options =>
                        options.AddDefaultPolicy(policy =>
                        {
                            policy
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()
                                .SetIsOriginAllowed(hostName => true);
                        })
                    );

builder.Services.AddApplication(builder.Configuration);


builder.Services.AddHttpContextAccessor();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
    {
        o.WithTheme(ScalarTheme.BluePlanet);

    });

}

app.UseHttpsRedirection();




app.MapControllers();

app.Run();