namespace LightsOn.WebApi.Brokers.DateTimes;

public interface IDateTimeBroker
{
    public DateTimeOffset GetCurrentDateTimeOffset();
}