using System.Text.Json;
using CorectMyQuran.Application.Interfaces.Auth;
using CorectMyQuran.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace CorectMyQuran.Services.Auth;

public class CookieManger : ICookieManger
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CookieManger(IHttpContextAccessor contextAccessor, IDateTimeProvider dateTimeProvider)
    {
        _contextAccessor = contextAccessor;
        _dateTimeProvider = dateTimeProvider;
    }

    public void SetProperty(string key, object value, TimeSpan period)
    {
        SetProperty(key, value, _dateTimeProvider.Now.Add(period));
    }

    public void SetProperty(string key, object value, DateTime date)
    {
        var options = new CookieOptions
        {
            Path = "/",
            Expires = date,
            // Accepts only secure cookies
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true
        };
        var stringValue = value is not string ? JsonSerializer.Serialize(value) : (string)value;
        _contextAccessor.HttpContext?.Response.Cookies.Append(key, stringValue, options);
    }

    [Authorize]
    public string? GetPropertyByClaimType(string key)
    {
        return _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == key)?.Value;
    }

    public string GetProperty(string key)
    {
        Console.WriteLine(_contextAccessor.HttpContext!.Request.Cookies[key]!);
        return _contextAccessor.HttpContext!.Request.Cookies[key]!;
        
    }

    public void Clear()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete("RefreshToken");
        _contextAccessor.HttpContext?.Response.Cookies.Delete("Id");
        _contextAccessor.HttpContext?.Response.Cookies.Delete("Autorized");
    }
}