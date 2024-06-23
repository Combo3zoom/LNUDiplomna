using System.Linq.Expressions;
using LightsOn.Application.CategoryExpense.Commands.UpdateCategoryExpense;
using LightsOn.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tynamix.ObjectFiller;

namespace LightsOn.Application.UnitTests.CategoryExpense.Command.UpdateCategoryExpense;

public partial class UpdateCategoryExpenseCommandHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private Mock<IUpdateCategoryExpenseCommandHandlerStorageBroker> _updateCategoryExpenseCommandHandlerStorageBroker;
    private UpdateCategoryExpenseCommandHandler _updateCategoryExpenseCommandHandler;
    public static IEnumerable<object[]> s_randomCategoryExpenseTestCaseSource = new List<object[]>
    {
        new object[] {CreateRandomCategoryExpense()}
    };

    public UpdateCategoryExpenseCommandHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();

        var mockDbSet = SimulateDbSetWithInMemoryCollection();

        _mockContext.Setup(c => c.CategoryExpenses)
            .Returns(mockDbSet.Object);

        _updateCategoryExpenseCommandHandlerStorageBroker =
            new Mock<IUpdateCategoryExpenseCommandHandlerStorageBroker>();
        _updateCategoryExpenseCommandHandler = new UpdateCategoryExpenseCommandHandler(_mockContext.Object,
            _updateCategoryExpenseCommandHandlerStorageBroker.Object);
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
    
    private static Mock<DbSet<Domain.Entities.CategoryExpense>> SimulateDbSetWithInMemoryCollection()
    {
        var categoryExpenses = new List<Domain.Entities.CategoryExpense>();
        var mockDbSet = new Mock<DbSet<Domain.Entities.CategoryExpense>>();
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.Provider)
            .Returns(categoryExpenses.AsQueryable().Provider);
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.Expression)
            .Returns(categoryExpenses.AsQueryable().Expression);
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.ElementType)
            .Returns(categoryExpenses.AsQueryable().ElementType);
        
        mockDbSet.As<IQueryable<Domain.Entities.CategoryExpense>>()
            .Setup(m => m.GetEnumerator())
            .Returns(() => categoryExpenses.GetEnumerator());

        return mockDbSet;
    }

    private List<Domain.Entities.CategoryExpense> CreateRandomCategoryExpenses(int countClients)
    {
        var categoryExpenses = new List<Domain.Entities.CategoryExpense>();
        for (var i = 0; i < countClients; i++)
            categoryExpenses.Add(CreateRandomCategoryExpense());

        return categoryExpenses;
    }
}