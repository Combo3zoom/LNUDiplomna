using System.Linq.Expressions;
using LightsOn.Application.Common.Interfaces;
using LightsOn.Application.Engine.Commands.DeleteEngine;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.Engine.Command.DeleteEngine;

public partial class DeleteEngineCommandHandlerTests
{
     private Mock<IApplicationDbContext> _mockContext;
    private Mock<IDeleteEngineCommandHandleStorageBroker> _deleteEngineCommandHandlerStorageBroker;
    private DeleteEngineCommandHandle _deleteEngineHandlerCommand;
    public static IEnumerable<object[]> s_randomEngineTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomEngine()}
    };
    public DeleteEngineCommandHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();

        var mockDbSet = SimulateDbSetWithInMemoryCollection();

        this._mockContext.Setup(c => c.Engines)
            .Returns(mockDbSet.Object);

        _deleteEngineCommandHandlerStorageBroker =
            new Mock<IDeleteEngineCommandHandleStorageBroker>();
        _deleteEngineHandlerCommand = new DeleteEngineCommandHandle(_mockContext.Object,
            _deleteEngineCommandHandlerStorageBroker.Object);
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
        filler.Setup();

        return filler;
    }
    
    private static Mock<DbSet<Domain.Entities.Engine>> SimulateDbSetWithInMemoryCollection()
    {
        var engine = new List<Domain.Entities.Engine>();
        var mockDbSet = new Mock<DbSet<Domain.Entities.Engine>>();
        
        mockDbSet.As<IQueryable<Domain.Entities.Engine>>()
            .Setup(m => m.Provider)
            .Returns(engine.AsQueryable().Provider);
        
        mockDbSet.As<IQueryable<Domain.Entities.Engine>>()
            .Setup(m => m.Expression)
            .Returns(engine.AsQueryable().Expression);
        
        mockDbSet.As<IQueryable<Domain.Entities.Engine>>()
            .Setup(m => m.ElementType)
            .Returns(engine.AsQueryable().ElementType);
        
        mockDbSet.As<IQueryable<Domain.Entities.Engine>>()
            .Setup(m => m.GetEnumerator())
            .Returns(() => engine.GetEnumerator());

        return mockDbSet;
    }
}