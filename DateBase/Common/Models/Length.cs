namespace CorectMyQuran.DateBase.Common.Models;

public record Length(int Min, int Max)
{
    public  bool ValidateLength(string value)
    {
        return value.Length >= Min && value.Length <= Max;
    }
    public  (int Min, int Max) GetLength(int min, int max)
    {
        return (min, max);
    }
};