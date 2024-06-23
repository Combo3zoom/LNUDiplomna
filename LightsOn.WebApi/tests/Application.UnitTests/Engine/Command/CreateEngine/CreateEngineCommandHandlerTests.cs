using System.Linq.Expressions;
using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.Engine.Commands.CreateEngine;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.Engine.Command.CreateEngine;

public partial class CreateEngineCommandHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private CreateEngineCommandHandler _handler;
    private Mock<ICreateEngineCommandHandlerStorageBroker> _mockCreateEngineCommandHandlerStorageBroker;

    public static IEnumerable<object[]> s_randomEngineTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomEngine()}
    };
    public CreateEngineCommandHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        
        var mockDbSet = new Mock<DbSet<Domain.Entities.Engine>>();
        _mockContext.Setup(c => c.Engines).Returns(mockDbSet.Object);

        _mockCreateEngineCommandHandlerStorageBroker = new Mock<ICreateEngineCommandHandlerStorageBroker>();
        _handler = new CreateEngineCommandHandler(_mockContext.Object, _mockCreateEngineCommandHandlerStorageBroker.Object);
    }

    private static Domain.Entities.Engine CreateRandomEngine() => CreateEngineFilter().Create();

    private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
    {
        return actualException => actualException.Message == expectedException.Message 
                                  && actualException.InnerException!.Message == expectedException.InnerException!.Message;
    }

    private static Filler<Domain.Entities.Engine> CreateEngineFilter()
    {
        var filler = new Filler<Domain.Entities.Engine>();

        return filler;
    }
}