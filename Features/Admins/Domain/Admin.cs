using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.Features.Admins.Domain;

public class Admin: Aggregate<AdminId>
{
    public AdminId Id { get; private set; }

    public string Name { get; private set; }

    public string Phone { get; private set; }
    public string Password { get; private set; }


    private Admin() { } // Required for EF Core

    public Admin(AdminId id, string name, string password, string phone) : base(id)
    {
        Name = name;
        Password = password;
        Phone = phone;
    }

    
}