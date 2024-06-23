using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.Material.Commands.UpdateMaterial;
using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.Material.Command.UpdateMaterial;

public partial class UpdateMaterialCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCreateAndUpdateMaterialTestCaseSource))]
    public async Task ShouldUpdateMaterial(CreateMaterialCommand exceptedCreateMaterialCommand,
        UpdateMaterialCommand updatedMaterialMaterial)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterialCommand);

        await _testing.SendAsync(new UpdateMaterialCommand(createdMaterialId,
            updatedMaterialMaterial.FullName, updatedMaterialMaterial.ShortName,
            updatedMaterialMaterial.Model,
            updatedMaterialMaterial.Cost, 
            updatedMaterialMaterial.UnitMeasurementForUpdateMaterialCommand));
        
        var updatedMaterial = await _testing.GetMaterialWithIncludesAsync<Domain.Entities.Material>(createdMaterialId);

        updatedMaterial.Should().NotBeNull();
        updatedMaterial!.FullName.Should().Be(updatedMaterialMaterial.FullName);
        updatedMaterial!.ShortName.Should().Be(updatedMaterialMaterial.ShortName);
        updatedMaterial!.Model.Should().Be(updatedMaterialMaterial.Model);
        updatedMaterial!.Cost.Should().BeApproximately(updatedMaterialMaterial.Cost,
            Convert.ToDecimal(Math.Pow(10, -9)));
        updatedMaterial.UnitMeasurement.Name.Should()
            .Be(updatedMaterialMaterial.UnitMeasurementForUpdateMaterialCommand.Name);
    }
}