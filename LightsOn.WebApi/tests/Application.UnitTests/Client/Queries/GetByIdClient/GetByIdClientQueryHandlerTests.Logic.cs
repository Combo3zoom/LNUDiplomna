using System.Diagnostics;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using FluentAssertions;
using LightsOn.Application.Client.Queries.GetByIdClient;
using LightsOn.Application.UnitTests.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Queries.GetByIdClient;

public partial class GetByIdClientQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldGetByIdClient(Domain.Entities.Client inputClient)
    {
        // given
        var expectedClientBriefDto = _mapper.Map<ClientBriefDto>(inputClient);
        
        _getByIdClientStorageBroker
            .Setup(repo => repo.GetByIdClient(
                It.IsAny<Application.Client.Queries.GetByIdClient.GetByIdClient>(),
                It.IsAny<CancellationToken>()))
            .Returns(expectedClientBriefDto);

        // when
        var result = await _getByIdClientHandler.Handle(
            new Application.Client.Queries.GetByIdClient.GetByIdClient(inputClient.Id), CancellationToken.None);
                
        // then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedClientBriefDto);

        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public async Task ShouldGetByIdClientAndCompareIfNameNotChangedOnHandleAsync(Domain.Entities.Client inputClient)
    {
        // given
        var expectedClientBriefDto = _mapper.Map<ClientBriefDto>(inputClient);
        
        _getByIdClientStorageBroker
            .Setup(repo => repo.GetByIdClient(
                It.IsAny<Application.Client.Queries.GetByIdClient.GetByIdClient>(),
                It.IsAny<CancellationToken>()))
            .Returns(expectedClientBriefDto);
        
        var exceptedClientName = inputClient.Name;

        // when
        var result = await _getByIdClientHandler.Handle(
            new Application.Client.Queries.GetByIdClient.GetByIdClient(inputClient.Id), CancellationToken.None);
        
        // then
        result.Name.Should().BeEquivalentTo(exceptedClientName);
        
        this._mockContext.VerifyNoOtherCalls();
    }
}