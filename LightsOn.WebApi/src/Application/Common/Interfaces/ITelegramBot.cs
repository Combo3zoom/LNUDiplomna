namespace LightsOn.Application.Common.Interfaces;

public interface ITelegramBot
{
    Task SendMessageToAllowedUsers(string message);
}