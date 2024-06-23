using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.UnitMeasurement.Commands.CreateUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Queries.GetByIdUnitMeasurement;
using LightsOn.Application.UnitMeasurement.Queries.GetByNameUnitMeasurement;

namespace LightsOn.Application.IntegrationTests.Material.Command.CreateMaterial;


public partial class CreateMaterialCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomCreateMaterialTestCaseSource))]
    public async Task ShouldCreateMaterialExpense(CreateMaterialCommand exceptedCreateMaterialCommand)
    {
        var materialId = await _testing.SendAsync(exceptedCreateMaterialCommand);
        
        var actualMaterial = await _testing.GetMaterialWithIncludesAsync<Domain.Entities.Material>(materialId);

        actualMaterial.Should().NotBeNull();
        
        actualMaterial!.FullName.Should().Be(exceptedCreateMaterialCommand.FullName);
        actualMaterial!.ShortName.Should().Be(exceptedCreateMaterialCommand.ShortName);
        actualMaterial!.Cost.Should().BeApproximately(exceptedCreateMaterialCommand.Cost,
            Convert.ToDecimal(Math.Pow(10, -9)));
        actualMaterial.UnitMeasurement!.Name.Should()
            .Be(exceptedCreateMaterialCommand.UnitMeasurementForCreateMaterialCommand.Name);
    }
}