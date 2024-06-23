// using System.Linq.Expressions;
// using FluentAssertions;
// using LightsOn.Application.Engine.Commands.CreateEngine;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using Xunit;
//
// namespace LightsOn.Application.UnitTests.Engine.Command.CreateEngine;
//
// public partial class CreateEngineCommandHandlerTests
// {
//     [Theory]
//     [InlineData("", "d11")]
//     public async Task ShouldThrowValidationExceptionOnCreateIfEngineIsNullAsync(string invalidEngineName,
//         string engineSerialNumber)
//     {
//         var createEngineCommandValidation = new CreateEngineCommandValidation(_mockContext.Object);
//
//         // when
//         var validationResult =
//             await createEngineCommandValidation.ValidateAsync(new CreateEngineCommand(invalidEngineName,
//                 engineSerialNumber));
//
//         // then
//         validationResult.IsValid.Should().BeFalse();
//     }
//
//     [Fact]
//     public async Task ShouldThrowValidationExceptionOnCreateIfEngineNameIsLargerAsync()
//     {
//         // given
//         string invalidName = new string('a', 250);
//         string serialNumber = "d11";
//
//         var createEngineCommandValidation = new CreateEngineCommandValidation(_mockContext.Object);
//
//         // when
//         var validationResult =
//             await createEngineCommandValidation.ValidateAsync(new CreateEngineCommand(invalidName, serialNumber));
//
//         // then
//         validationResult.IsValid.Should().BeFalse();
//     }
// }