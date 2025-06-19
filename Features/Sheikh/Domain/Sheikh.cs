using CorectMyQuran.Common;
using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.Features.Sheikh.Domain;

public class Sheikh:Aggregate<SheikhId>
{
    public SheikhId Id { get; private set; }

    public string Name { get; private set; }

    public string Phone { get; private set; }
    
    public string Password { get; private set; }

    private Sheikh() { } // Required for EF Core
    public Sheikh(SheikhId id, string name, string password, string phone):base(id)
    {
        Name = name;
        Password = password;
        Phone = phone;
        Id = id;
    }

    public static (Sheikh,string password) CreateSheikh(string name, string phone)
    {
        var password = RandomPassword.Generate();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var id = SheikhId.CreateUnique();
        var sheikh = new Sheikh(id, name, hashedPassword, phone);
        return (sheikh, password);
    }
    
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Password);
    }
    
}