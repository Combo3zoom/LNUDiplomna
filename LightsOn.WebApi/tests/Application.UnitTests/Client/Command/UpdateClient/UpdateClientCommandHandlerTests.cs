using System.Linq.Expressions;
using LightsOn.Application.Client.Commands.UpdateClient;
using LightsOn.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.Client.Command.UpdateClient;

public partial class UpdateClientCommandHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private UpdateClientCommandHandler _updateClientCommandHandler;
    private Mock<IUpdateClientCommandHandlerStorageBroker> _updateClientCommandHandlerStorageBroker;
    
    public static IEnumerable<object[]> s_randomClientTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomClient()}
    };
    
    public UpdateClientCommandHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();

        var mockDbSet = SimulateDbSetWithInMemoryCollection();

        this._mockContext.Setup(c => c.Clients)
            .Returns(mockDbSet.Object);
        
        _updateClientCommandHandlerStorageBroker = new Mock<IUpdateClientCommandHandlerStorageBroker>();
        _updateClientCommandHandler = new UpdateClientCommandHandler(_mockContext.Object,
            _updateClientCommandHandlerStorageBroker.Object);
    }

    private static Domain.Entities.Client CreateRandomClient() => CreateClientFilter().Create();

    private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
    {
        return actualException => actualException.Message == expectedException.Message 
                                  && actualException.InnerException!.Message == expectedException.InnerException!.Message;
    }

    private static Filler<Domain.Entities.Client> CreateClientFilter()
    {
        var filler = new Filler<Domain.Entities.Client>();
        filler.Setup()
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
    
    private static Mock<DbSet<Domain.Entities.Client>> SimulateDbSetWithInMemoryCollection()
    {
        var clients = new List<Domain.Entities.Client>();
        var mockDbSet = new Mock<DbSet<Domain.Entities.Client>>();
        
        mockDbSet.As<IQueryable<Domain.Entities.Client>>()
            .Setup(m => m.Provider)
            .Returns(clients.AsQueryable().Provider);
        
        mockDbSet.As<IQueryable<Domain.Entities.Client>>()
            .Setup(m => m.Expression)
            .Returns(clients.AsQueryable().Expression);
        
        mockDbSet.As<IQueryable<Domain.Entities.Client>>()
            .Setup(m => m.ElementType)
            .Returns(clients.AsQueryable().ElementType);
        
        mockDbSet.As<IQueryable<Domain.Entities.Client>>()
            .Setup(m => m.GetEnumerator())
            .Returns(() => clients.GetEnumerator());

        return mockDbSet;
    }
}