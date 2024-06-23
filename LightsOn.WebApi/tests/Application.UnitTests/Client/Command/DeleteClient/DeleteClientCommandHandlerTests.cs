using System.Linq.Expressions;
using LightsOn.Application.Client.Commands.CreateClient;
using LightsOn.Application.Client.Commands.DeleteClient;
using LightsOn.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.Client.Command.DeleteClient;

public partial class DeleteServiceDescriptionCommandHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private DeleteCompanyPhoneNumberCommandHandler _deleteCompanyPhoneNumberCommandHandler;
    private Mock<IDeleteClientCommandHandlerStorageBroker> _deleteClientCommandHandlerStorageBroker;

    public static IEnumerable<object[]> s_randomClientTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomClient()}
    };
    public DeleteServiceDescriptionCommandHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();

        var mockDbSet = SimulateDbSetWithInMemoryCollection();

        this._mockContext.Setup(c => c.Clients)
            .Returns(mockDbSet.Object);

        _deleteClientCommandHandlerStorageBroker = new Mock<IDeleteClientCommandHandlerStorageBroker>();
        _deleteCompanyPhoneNumberCommandHandler = new DeleteCompanyPhoneNumberCommandHandler(_mockContext.Object,
            _deleteClientCommandHandlerStorageBroker.Object);
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
    
    private Mock<DbSet<Domain.Entities.Client>> SimulateDbSetWithInMemoryCollection()
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

    private List<Domain.Entities.Client> FillingClientDbSet(int countClients)
    {
        var clients = new List<Domain.Entities.Client>();
        for (var i = 0; i < countClients; i++)
            clients.Add(CreateRandomClient());

        foreach (var client in clients)
            _mockContext.Setup(context => context.Clients.FindAsync(It.Is<object?[]?>(
                    objects => objects != null && objects.Cast<int>()
                        .Any(o => o == client.Id)), CancellationToken.None))
                .ReturnsAsync(client);

        return clients;
    }
}