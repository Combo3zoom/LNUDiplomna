using LightsOn.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace LightsOn.Application.IntegrationTests.TelegramBot;

[Collection("Tests")]
public partial class TelegramBotServiceTests : IClassFixture<Testing>
{
    private readonly TelegramBotService _telegramBotService;
    private readonly Mock<ILogger<TelegramBotService>> _loggerMock;
    
    public static IEnumerable<object[]> s_randomTelegramBotServiceTestsTestCaseSource = new List<object[]>
    {
        new object[] { "12345", "67890" }
    };

    public TelegramBotServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"TelegramBotSettings:Token", "dummy_token"},
            {"TelegramBotSettings:AllowedChatIds", "12345,67890"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _loggerMock = new Mock<ILogger<TelegramBotService>>();
        _telegramBotService = new TelegramBotService(configuration, _loggerMock.Object);
    }
}