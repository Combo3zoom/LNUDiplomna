using FluentAssertions;
using LightsOn.Application.Client.Queries.GetByIdClient;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Queries.GetByIdClient;

public partial class GetByIdClientQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public Task ShouldMappingClientToClientBriefDto(Domain.Entities.Client inputClient)
    {
        // given
        var expectedClientBriefDto = _mapper.Map<ClientBriefDto>(inputClient);
        
        // when
        var mappedClientBriefDto = this._mapper.Map<ClientBriefDto>(inputClient);
        
        // then
        mappedClientBriefDto.Should().BeEquivalentTo(expectedClientBriefDto);
        return Task.CompletedTask;
    }
    
    [Theory]
    [MemberData(nameof(s_randomClientTestCaseSource))]
    public Task ShouldMappingClientToClientBriefDtoAndCompareClientNameWithClientBriefDtoName(Domain.Entities.Client inputClient)
    {
        // given
        var inputClientName = inputClient.Name;
        
        // when
        var mappedClientBriefDto = this._mapper.Map<ClientBriefDto>(inputClient);
        
        // then
        mappedClientBriefDto.Name.Should().Be(inputClientName);

        return Task.CompletedTask;
    }
}