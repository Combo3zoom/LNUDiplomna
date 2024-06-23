using LightsOn.Application.Common.Exceptions;
using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.Material.Commands.UpdateMaterial;

namespace LightsOn.Application.IntegrationTests.Material.Command.UpdateMaterial;

public partial class UpdateMaterialCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomMaterialAndIncorrectDecimalTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialCostIsLessZero(
        CreateMaterialCommand exceptedCreateMaterial, decimal incorrectCost)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterial);
        
        var incorrectUpdatedMaterial = new UpdateMaterialCommand(createdMaterialId, exceptedCreateMaterial.FullName,
            exceptedCreateMaterial.ShortName, exceptedCreateMaterial.Model!, incorrectCost,
            new UpdateUnitMeasurementForUpdateMaterialCommand(
                exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand.Name));
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectUpdatedMaterial)).Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [MemberData( nameof(s_randomMaterialAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialFullNameIsLargerThanNeed(
        CreateMaterialCommand exceptedCreateMaterial, string incorrectFullName)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterial);
        
        var incorrectUpdatedMaterial = new UpdateMaterialCommand(createdMaterialId, incorrectFullName,
            exceptedCreateMaterial.ShortName, exceptedCreateMaterial.Model!, exceptedCreateMaterial.Cost,
            new UpdateUnitMeasurementForUpdateMaterialCommand(
                exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand.Name));
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectUpdatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialFullNameIsEmpty(
        CreateMaterialCommand exceptedCreateMaterial, string incorrectFullName)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterial);
        
        var incorrectUpdatedMaterial = new UpdateMaterialCommand(createdMaterialId, incorrectFullName,
            exceptedCreateMaterial.ShortName, exceptedCreateMaterial.Model!, exceptedCreateMaterial.Cost,
            new UpdateUnitMeasurementForUpdateMaterialCommand(
                exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand.Name));
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectUpdatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialShortNameIsLargerThanNeed(
        CreateMaterialCommand exceptedCreateMaterial, string incorrectShortName)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterial);
        
        var incorrectUpdatedMaterial = new UpdateMaterialCommand(createdMaterialId, exceptedCreateMaterial.FullName,
            incorrectShortName, exceptedCreateMaterial.Model!, exceptedCreateMaterial.Cost,
            new UpdateUnitMeasurementForUpdateMaterialCommand(
                exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand.Name));
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectUpdatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndEmptyStringTestCaseSource))]
    public async Task ShouldThrowNullExceptionIfMaterialShortNameIsNull(CreateMaterialCommand exceptedCreateMaterial,
        string incorrectShortName)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterial);
        
        var incorrectUpdatedMaterial = new UpdateMaterialCommand(createdMaterialId, exceptedCreateMaterial.FullName,
            incorrectShortName, exceptedCreateMaterial.Model!, exceptedCreateMaterial.Cost,
            new UpdateUnitMeasurementForUpdateMaterialCommand(
                exceptedCreateMaterial.UnitMeasurementForCreateMaterialCommand.Name));
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectUpdatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [MemberData( nameof(s_randomMaterialAndLargerStringTestCaseSource))]
    public async Task ShouldThrowValidationExceptionIfMaterialModelIsLargerThanNeed(
        CreateMaterialCommand exceptedCreateMaterialCommand, string incorrectModel)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterialCommand);

        var incorrectUpdatedMaterial = new UpdateMaterialCommand(createdMaterialId, 
            exceptedCreateMaterialCommand.FullName, exceptedCreateMaterialCommand.ShortName, incorrectModel,
            exceptedCreateMaterialCommand.Cost, new UpdateUnitMeasurementForUpdateMaterialCommand(
                exceptedCreateMaterialCommand.UnitMeasurementForCreateMaterialCommand.Name));
        
        await FluentActions.Invoking(() =>
            _testing.SendAsync(incorrectUpdatedMaterial)).Should().ThrowAsync<ValidationException>();
    }
}