using FluentAssertions;
using FluentValidation;
using LightsOn.Application.Client.Queries.GetByIdClient;

namespace LightsOn.Application.UnitTests.Client.Queries.GetByIdClient;

public partial class GetByIdClientQueryHandlerTests
{
    // [Test]
    // [TestCase(null)]
    // public async Task ShouldThrowValidationExceptionOnGetByIdClientIfClientIdNull(Domain.Entities.Client inputClient)
    // {
    //     // given
    //     var invalidInputClient = new Application.Client.Queries.GetByIdClient.GetByIdClient(inputClient.Id);
    //     var getByIdClientQueryValidator = new GetByIdClientQueryValidator();
    //
    //     // when
    //     var validateResult =
    //         await getByIdClientQueryValidator.ValidateAsync(invalidInputClient, CancellationToken.None);
    //     
    //     // then
    //     validateResult.IsValid.Should().BeFalse();
    //     
    //     this._mockContext.VerifyNoOtherCalls();
    // }
}