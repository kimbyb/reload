namespace Reload.Services;

public class TimeService : ITimeService
{
    public string GetMessage()
    {
        return $"Current time: {DateTime.Now}";
    }
}