using LightsOn.Application.Material.Commands.DeleteMaterial;

namespace LightsOn.Application.IntegrationTests.Material.Command.DeleteMaterial;

public partial class DeleteMaterialCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomMaterialTestCaseSource))]
    public async Task ShouldRequireValidMaterialId(Domain.Entities.Material exceptedMaterialCommand)
    {
        var nonExistedMaterial = new DeleteMaterialCommand(exceptedMaterialCommand.Id);
        
        await FluentActions.Invoking(() 
            => _testing.SendAsync(nonExistedMaterial)).Should().ThrowAsync<NotFoundException>();
    }
}