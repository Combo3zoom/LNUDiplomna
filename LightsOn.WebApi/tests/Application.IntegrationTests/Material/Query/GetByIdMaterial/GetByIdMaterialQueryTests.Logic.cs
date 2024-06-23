using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.Material.Queries.GetByIdMaterial;

namespace LightsOn.Application.IntegrationTests.Material.Query.GetByIdMaterial;

public partial class GetByIdMaterialQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomMaterialTestCaseSource))]
    public async Task ShouldGetByIdClient(Domain.Entities.Material exceptedMaterialCommand)
    {
        var exceptedCreateMaterialCommand = new CreateMaterialCommand(exceptedMaterialCommand.FullName,
            exceptedMaterialCommand.ShortName, exceptedMaterialCommand.Model!, exceptedMaterialCommand.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedMaterialCommand.UnitMeasurement.Name));
        
        var materialId = await _testing.SendAsync(exceptedCreateMaterialCommand);
        
        var material = await _testing.FindAsync<Domain.Entities.Material>(materialId);
        
        var getByIdMaterialQuery = new GetByIdMaterialQuery(materialId);
        var actualMaterial = await _testing.SendAsync(getByIdMaterialQuery);

        material.Should().NotBeNull();
        material!.FullName.Should().Be(actualMaterial.FullName);
        material!.ShortName.Should().Be(actualMaterial.ShortName);
        material!.Model.Should().Be(actualMaterial.Model);
        material!.Cost.Should().Be(actualMaterial.Cost);
        material!.UnitMeasurementId.Should().Be(actualMaterial.UnitMeasurement.Id);
    }
}