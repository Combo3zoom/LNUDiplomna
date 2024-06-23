using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Material.Commands.CreateMaterial;

namespace LightsOn.Application.IntegrationTests.Material.Command.CreateMaterial;

public partial class CreateMaterialCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomMaterialAndIncorrectDecimalTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialCostIsLessZero(
        CreateMaterialCommand exceptedCreateMaterial, decimal incorrectCost)
    {
        var incorrectCreatedMaterial = exceptedCreateMaterial with { Cost = incorrectCost };
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCreatedMaterial)).Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [MemberData( nameof(s_randomMaterialAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialFullNameIsLargerThanNeed(
        CreateMaterialCommand exceptedCreateMaterial, string incorrectFullName)
    {
        var incorrectCreatedMaterial = new CreateMaterialCommand(incorrectFullName,
            exceptedCreateMaterial.ShortName, exceptedCreateMaterial.Model!, exceptedCreateMaterial.Cost,
            exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand);
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCreatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowNullExceptionIfMaterialFullNameIsNull(CreateMaterialCommand exceptedCreateMaterial,
        string incorrectFullName)
    {
        var incorrectCreatedMaterial = new CreateMaterialCommand(incorrectFullName,
            exceptedCreateMaterial.ShortName, exceptedCreateMaterial.Model!, exceptedCreateMaterial.Cost,
            exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand);
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCreatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialShortNameIsLargerThanNeed(
        CreateMaterialCommand exceptedCreateMaterial, string incorrectShortName)
    {
        var incorrectCreatedMaterial = new CreateMaterialCommand(exceptedCreateMaterial.FullName,
            incorrectShortName, exceptedCreateMaterial.Model!, exceptedCreateMaterial.Cost,
            exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand);
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCreatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowNullExceptionIfMaterialShortNameIsNull(CreateMaterialCommand exceptedCreateMaterial,
        string incorrectShortName)
    {
        var incorrectCreatedMaterial = new CreateMaterialCommand(exceptedCreateMaterial.FullName,
            incorrectShortName, exceptedCreateMaterial.Model, exceptedCreateMaterial.Cost,
            exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand);
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCreatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialModelIsLargerThanNeed(
        CreateMaterialCommand exceptedCreateMaterial, string incorrectModel)
    {
        var incorrectCreatedMaterial = new CreateMaterialCommand(exceptedCreateMaterial.FullName,
            exceptedCreateMaterial.ShortName, incorrectModel, exceptedCreateMaterial.Cost,
            exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand);
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectCreatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
}