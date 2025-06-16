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
    }
    
}