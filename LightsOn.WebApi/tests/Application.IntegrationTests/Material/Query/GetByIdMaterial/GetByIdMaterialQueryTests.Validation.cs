// using LightsOn.Application.Common.Exceptions;
// using LightsOn.Application.Material.Queries.GetByIdMaterial;
//
// namespace LightsOn.Application.IntegrationTests.Material.Query.GetByIdMaterial;
//
// public partial class GetByIdMaterialQueryTests
// {
//     [Theory]
//     [MemberData(nameof(s_randomMaterialTestCaseSource))]
//     public async Task ShouldThrowNotFoundExceptionIfMaterialNull(Domain.Entities.Material incorrectMaterial)
//     {
//         var nonExistedMaterial = new GetByIdMaterialQuery(incorrectMaterial.Id);
//         
//         await FluentActions.Invoking(() => _testing.SendAsync(nonExistedMaterial)).Should().ThrowAsync<ValidationException>();
//     }
//
//     [Theory]
//     [InlineData(-1)]
//     public async Task ShouldThrowNotFoundExceptionIfClientIdNegative(int incorrectMaterialId)
//     {
//         var nonExistedMaterial = new GetByIdMaterialQuery(incorrectMaterialId);
//         
//         await FluentActions.Invoking(() => _testing.SendAsync(nonExistedMaterial)).Should().ThrowAsync<ValidationException>();
//     }
// }