using LightsOn.Application.Common.Interfaces;
using LightsOn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace LightsOn.Application.UnitTests.Common.Extensions;

public static class MockExtensions
{
    public static void SetupClientDbSet(this Mock<IApplicationDbContext> mockContext,
        IReadOnlyCollection<Domain.Entities.Client> сlients)
    {
        mockContext.Setup(c => c.Clients)
            .Returns(сlients.AsQueryable().BuildMockDbSet().Object);
    }
}