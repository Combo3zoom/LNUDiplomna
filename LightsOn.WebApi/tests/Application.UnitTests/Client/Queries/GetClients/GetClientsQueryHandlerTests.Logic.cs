using FluentAssertions;
using LightsOn.Application.Client.Queries.GetClients;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LightsOn.Application.UnitTests.Client.Queries.GetClients;

public partial class GetServiceDescriptionsQueryHandlerTests
{
    [Fact]
    public async Task ShouldGetClientsOnHandleAsync()
    {
        // given
        var expectedClients = this.CreateRandomClients(5);
        
        // when
        var clientsHandle = await this._getCompanyPhoneNumbersQueryHandler
            .Handle(new GetClientsQuery(), CancellationToken.None);
        
        // then
        for(var i=0; i<clientsHandle.Count; i++)
            clientsHandle[i].Name.Should().Be(expectedClients[i].Name);

        this._mockContext.VerifyNoOtherCalls();
    }
    
    [Fact]
    public async Task ShouldGetClientsIfClientsEmptyOnHandleAsync()
    {
        // given
        List<Domain.Entities.Client> expectedClients = new List<Domain.Entities.Client>();
        
        // when
        var clientsHandle = await this._getCompanyPhoneNumbersQueryHandler
            .Handle(new GetClientsQuery(), CancellationToken.None);
        
        // then
        clientsHandle.Should().BeNull();
        
        //this._mockContext.VerifyNoOtherCalls();
    }
}