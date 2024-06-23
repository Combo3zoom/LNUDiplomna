using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using LightsOn.Application.CategoryExpense.Queries.GetCategoryExpenses;
using LightsOn.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.CategoryExpense.Queries.GetCategoryExpenses;

public partial class GetCategoryExpensesQueryHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private Mock<IGetCategoryExpensesQueryHandlerStorageBroker> _mockGetCategoryExpensesQueryStorageBroker;
    private IConfigurationProvider _configuration = null!;
    private GetCategoryExpensesQueryHandler _getCategoryExpensesQueryHandler;
    private IMapper _mapper = null!;
    
    public static IEnumerable<object[]> s_randomCategoryExpensesTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomCategoryExpenses()}
    };
    
    public GetCategoryExpensesQueryHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();

        var mockDbSet = SimulateDbSetWithInMemoryCollection();

        this._mockContext.Setup(c => c.CategoryExpenses)
            .Returns(mockDbSet.Object);
        
        MapperSettings();

        _mockGetCategoryExpensesQueryStorageBroker = new Mock<IGetCategoryExpensesQueryHandlerStorageBroker>();
        _getCategoryExpensesQueryHandler = new GetCategoryExpensesQueryHandler(_mockContext.Object, this._mapper!, 
            _mockGetCategoryExpensesQueryStorageBroker.Object);
    }

    private static Domain.Entities.CategoryExpense CreateRandomCategoryExpenses() => CreateCategoryExpensesFilter().Create();

    private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
    {
        return actualException => actualException.Message == expectedException.Message 
                                  && actualException.InnerException!.Message == expectedException.InnerException!.Message;
    }

    private static Filler<Domain.Entities.CategoryExpense> CreateCategoryExpensesFilter()
    {
        var filler = new Filler<Domain.Entities.CategoryExpense>();
        filler.Setup()
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
    
    private static Mock<DbSet<Domain.Entities.CategoryExpense>> SimulateDbSetWithInMemoryCollection()
    {
        var clients = new List<Domain.Entities.CategoryExpense>();
        var mockDbSet = new Mock<DbSet<Domain.Entities.CategoryExpense>>();
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.Provider)
            .Returns(clients.AsQueryable().Provider);
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.Expression)
            .Returns(clients.AsQueryable().Expression);
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.ElementType)
            .Returns(clients.AsQueryable().ElementType);
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.GetEnumerator())
            .Returns(() => clients.GetEnumerator());

        return mockDbSet;
    }
    
    private void MapperSettings()
    {
        this._configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        this._mapper = _configuration.CreateMapper();   
    }
}