using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.Material.Queries.GetMaterials;

namespace LightsOn.Application.IntegrationTests.Material.Query.GetMaterials;

public partial class GetMaterialsQueryTests
{
    [Theory]
    [MemberData(nameof(s_randomMaterialTestCaseSource))]
    public async Task ShouldGetMaterials(Domain.Entities.Material exceptedFirstMaterial,
        Domain.Entities.Material exceptedSecondMaterial)
    {
        var exceptedFirstCreateMaterialCommand = new CreateMaterialCommand(exceptedFirstMaterial.FullName,
            exceptedFirstMaterial.ShortName, exceptedFirstMaterial.Model!, exceptedFirstMaterial.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedFirstMaterial.UnitMeasurement.Name));
        var exceptedSecondCreateMaterialCommand = new CreateMaterialCommand(exceptedSecondMaterial.FullName,
            exceptedSecondMaterial.ShortName, exceptedSecondMaterial.Model!, exceptedSecondMaterial.Cost,
            new CreateUnitMeasurementForCreateMaterialCommand(exceptedSecondMaterial.UnitMeasurement.Name));
        
        await _testing.SendAsync(exceptedFirstCreateMaterialCommand);
        await _testing.SendAsync(exceptedSecondCreateMaterialCommand);
        
        var getByIdMaterialQuery = new GetMaterialsQuery();
        var actualMaterial = await _testing.SendAsync(getByIdMaterialQuery);

        actualMaterial.Should().NotBeNull();
        actualMaterial.Count.Should().Be(2); 
    }
}