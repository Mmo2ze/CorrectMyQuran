using CorectMyQuran.Application.Services;

namespace CorectMyQuran.Services;

public class DateTimeProvider : IDateTimeProvider
{
	public DateTime Now => DateTime.UtcNow.AddHours(3);
	public DateOnly Today => DateOnly.FromDateTime(Now);
	public TimeOnly TimeNow => TimeOnly.FromDateTime(Now);
}