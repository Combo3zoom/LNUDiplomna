using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using LightsOn.Application.Client.Queries.GetByIdClient;
using LightsOn.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.Client.Queries.GetByIdClient;

public partial class GetByIdClientQueryHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private IConfigurationProvider _configuration = null!;
    private GetByIdClientHandler _getByIdClientHandler;
    private Mock<IGetByIdClientStorageBroker> _getByIdClientStorageBroker;
    private IMapper _mapper = null!;

    public static IEnumerable<object[]> s_randomClientTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomClient()}
    };
    public GetByIdClientQueryHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        
        MapperSettings();

        _getByIdClientStorageBroker = new Mock<IGetByIdClientStorageBroker>();
        _getByIdClientHandler = new GetByIdClientHandler(_mockContext.Object, this._mapper!, _getByIdClientStorageBroker.Object);
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
    
    private void MapperSettings()
    {
        this._configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        this._mapper = _configuration.CreateMapper();   
    }
}