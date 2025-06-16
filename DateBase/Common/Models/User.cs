namespace CorectMyQuran.DateBase.Common.Models;

public class User<TId>:Aggregate<TId>
{
    protected User(TId id):base(id)
    {
        
    }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public string Name { get; protected set; } = string.Empty;
    public virtual string Phone { get; protected set; } = string.Empty;
    public  string? Email { get; protected set; } = string.Empty;
    public bool? HasWhatsapp { get;  set; } 
    public virtual void SetHasWhatsapp(bool value)
    {
        HasWhatsapp = value;
    }
    public string ShortName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Name))
                return string.Empty;

            var nameParts = Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(' ', nameParts.Take(2));
        }
    }
    

}