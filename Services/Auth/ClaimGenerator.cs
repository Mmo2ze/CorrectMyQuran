using System.Security.Claims;
using CorectMyQuran.Application.Interfaces.Auth;
using CorectMyQuran.Application.Variables;
using CorectMyQuran.DateBase;
using CorectMyQuran.DateBase.Common.Errors;
using CorectMyQuran.Features.Admins.Domain;
using CorectMyQuran.Features.Sheikh.Domain;
using CorectMyQuran.Features.Student.Domain;
using Microsoft.EntityFrameworkCore;

namespace CorectMyQuran.Services.Auth;

public class ClaimGenerator : IClaimGenerator
{
    private readonly MainContext _context;

    public ClaimGenerator(MainContext context)
    {
        _context = context;
    }


    public async Task<ErrorOr<List<Claim>>> GenerateClaims(string userId)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.Id, userId),
        };

        if (AdminId.IsValidId(userId))
        {
            var adminId = AdminId.Create(userId);
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == adminId);
            if (admin is null)
            {
                return Errors.Auth.InvalidCredentials;
            }
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Email, admin.Phone));
            claims.Add(new Claim(ClaimTypes.Name, admin.Name));
        }else if (SheikhId.IsValidId(userId))
        {
            var adminId = SheikhId.Create(userId);
            var admin = await _context.Sheikhs.FirstOrDefaultAsync(a => a.Id == adminId);
            if (admin is null)
            {
                return Errors.Auth.InvalidCredentials;
            }
            claims.Add(new Claim(ClaimTypes.Role, "Sheikh"));
            claims.Add(new Claim(ClaimTypes.Email, admin.Phone));
            claims.Add(new Claim(ClaimTypes.Name, admin.Name));
        }else if (StudentId.IsValidId(userId))
        {
            var studentId = StudentId.Create(userId);
            var student = await _context.Students.FirstOrDefaultAsync(a => a.Id == studentId);
            if (student is null)
            {
                return Errors.Auth.InvalidCredentials;
            }
            claims.Add(new Claim(ClaimTypes.Role, "Student"));
            claims.Add(new Claim(ClaimTypes.MobilePhone, student.Phone));
            claims.Add(new Claim(ClaimTypes.Name, student.Name));
        }

       
        return claims;
    }
}