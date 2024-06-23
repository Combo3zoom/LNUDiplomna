using LightsOn.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace LightsOn.Application.IntegrationTests.TelegramBot;

public partial class TelegramBotServiceTests
{
    [Theory]
    [MemberData(nameof(s_randomTelegramBotServiceTestsTestCaseSource))]
    public async Task SendMessageToAllowedUsers_LogsMessageSent(long firstChatId, long secondChatId)
    {
        await _telegramBotService.SendMessageToAllowedUsers("Test message");

        foreach (var chatId in new List<long> { firstChatId, secondChatId })
        {
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Failed to send message to {chatId}")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!),
                Times.Once);
        }
    }
}