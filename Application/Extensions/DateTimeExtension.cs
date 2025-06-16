namespace CorectMyQuran.Application.Extensions;

public static class DateTimeExtension
{
    public static bool IsSameYearnMonth(this DateTime dateTime, DateOnly dateOnly)
    {
        return dateTime.Month == dateOnly.Month && dateTime.Year == dateOnly.Year;
    }

    public static bool IsSameYearnMonth(this DateTime dateTime, DateTime dateOnly)
    {
        return dateTime.Month == dateOnly.Month && dateTime.Year == dateOnly.Year;
    }

    public static bool IsSameYearnMonth(this DateOnly dateTime, DateTime dateOnly)
    {
        return dateTime.Month == dateOnly.Month && dateTime.Year == dateOnly.Year;
    }

    public static bool IsSameYearnMonth(this DateOnly dateTime, DateOnly dateOnly)
    {
        return dateTime.Month == dateOnly.Month && dateTime.Year == dateOnly.Year;
    }
}