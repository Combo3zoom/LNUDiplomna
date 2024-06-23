using System.Linq.Expressions;
using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.Client.Command.CreateClient;

public partial class CreateClientCommandHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private CreateClientCommandHandler _handler;
    private Mock<ICreateClientCommandHandlerStorageBroker> _createClientCommandHandlerStorageBroker; 
    
    public CreateClientCommandHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        
        var mockDbSet = new Mock<DbSet<Domain.Entities.Client>>();
        _mockContext.Setup(c => c.Clients).Returns(mockDbSet.Object);

        _createClientCommandHandlerStorageBroker = new Mock<ICreateClientCommandHandlerStorageBroker>();
        _handler = new CreateClientCommandHandler(_mockContext.Object, _createClientCommandHandlerStorageBroker.Object);
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
}