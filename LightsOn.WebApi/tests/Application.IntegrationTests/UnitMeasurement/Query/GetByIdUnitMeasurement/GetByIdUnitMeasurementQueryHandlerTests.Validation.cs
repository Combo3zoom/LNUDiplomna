using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.UnitMeasurement.Query.GetByIdUnitMeasurement;

public partial class GetByIdUnitMeasurementQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomUnitMeasurementTestCaseSource))]
    public async Task ShouldThrowNotFoundExceptionIfUnitMeasurementNonExist
        (Domain.Entities.UnitMeasurement incorrectUnitMeasurement)
    {
        var nonExistedCategoryExpense 
            = new Application.Client.Queries.GetByIdClient.GetByIdClient(incorrectUnitMeasurement.Id);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedCategoryExpense)).Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(-1)]
    public async Task ShouldThrowNotFoundExceptionIfUnitMeasurementIdNegative(int incorrectUnitMeasurementId)
    {
        var nonExistedUnitMeasurement = new GetByIdUnitMeasurementQuery(incorrectUnitMeasurementId);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedUnitMeasurement)).Should().ThrowAsync<InvalidOperationException>();
    }
}