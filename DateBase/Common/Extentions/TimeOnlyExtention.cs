namespace CorectMyQuran.DateBase.Common.Extentions;

public static class TimeOnlyExtensions
{
    public static TimeOnly SetSecondsToZero(this TimeOnly time)
    {
        return new TimeOnly(time.Hour, time.Minute,0); 
    }
}