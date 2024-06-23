using LightsOn.Application.Material.Commands.CreateMaterial;
using LightsOn.Application.Material.Commands.DeleteMaterial;

namespace LightsOn.Application.IntegrationTests.Material.Command.DeleteMaterial;

public partial class DeleteMaterialCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomMaterialCommandTestCaseSource))]
    public async Task ShouldDeleteMaterial(CreateMaterialCommand exceptedCreateMaterialCommand)
    {
        var createdMaterialId = await _testing.SendAsync(exceptedCreateMaterialCommand);

        await _testing.SendAsync(new DeleteMaterialCommand(createdMaterialId));

        var deletedMaterial = await _testing.FindAsync<Domain.Entities.Material>(createdMaterialId);
        
        deletedMaterial.Should().BeNull();
    }
}