namespace CorectMyQuran.DateBase.Common.Models;

public record ValueObjectId<T>(string Value)
{
    public static T Create(string value)
    {
        //if(!value.StartsWith(GetPrefixedId()))
        //do something
        return (T)Activator.CreateInstance(typeof(T), value)!;
    }

    public override string ToString()
    {
        return Value;
    } 

    public static T CreateUnique()
    {
        var value = Guid.CreateVersion7().ToString();
        var valueWithOutDashes = value.Replace("-", "");
        var newValue = GetPrefixedId() + valueWithOutDashes;
        return Create(newValue);
    }

    private static string GetPrefixedId()
    {
        var typeName = typeof(T).Name.ToLower();
        return typeName.Length >= 2 ? typeName[..2] + "_" : typeName + "_";
    }

    public static bool IsValidId(string value)
    {
        return value.StartsWith(GetPrefixedId());
    }

}