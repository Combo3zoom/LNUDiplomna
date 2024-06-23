using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using LightsOn.Application.CategoryExpense.Queries.GetByIdCategoryExpense;
using LightsOn.Application.Common.Interfaces;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.CategoryExpense.Queries.GetByIdCategoryExpense;

public partial class GetByIdCategoryExpenseQueryHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private IConfigurationProvider _configuration = null!;
    private GetByIdCategoryExpenseQueryHandler _getByIdCategoryExpenseHandler;
    private Mock<IGetByIdCategoryExpenseQueryHandlerStorageBroker> _getByIdCategoryExpenseStorageBroker;
    private IMapper _mapper = null!;

    public static IEnumerable<object[]> s_randomCategoryExpenseTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomCategoryExpense()}
    };
    
    public GetByIdCategoryExpenseQueryHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        MapperSettings();  

        _getByIdCategoryExpenseStorageBroker = new Mock<IGetByIdCategoryExpenseQueryHandlerStorageBroker>();
        _getByIdCategoryExpenseHandler = new GetByIdCategoryExpenseQueryHandler(_mockContext.Object,
            _mapper!, _getByIdCategoryExpenseStorageBroker.Object);
    }
    
    private static Domain.Entities.CategoryExpense CreateRandomCategoryExpense() => CreateCategoryExpenseFilter().Create();

    private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
    {
        return actualException => actualException.Message == expectedException.Message 
                                  && actualException.InnerException!.Message == expectedException.InnerException!.Message;
    }

    private static Filler<Domain.Entities.CategoryExpense> CreateCategoryExpenseFilter()
    {
        var filler = new Filler<Domain.Entities.CategoryExpense>();
        filler.Setup()
            .OnProperty(x => x.Created).Use(() => DateTimeOffset.UtcNow)
            .OnProperty(x => x.LastModified).Use(() => null);

        return filler;
    }
    
    private void MapperSettings()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));
        _mapper = _configuration.CreateMapper();   
    }
}