using LightsOn.Application.Common.Interfaces;

namespace LightsOn.Infrastructure.Services;

public class DateTimeOffSetService : IDateTimeOffSet
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
