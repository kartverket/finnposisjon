using Xunit;
using FluentAssertions;

namespace Kartverket.Finnpos.Core.Tests;

public class ApiTests
{
    [Fact]
    [Trait("Category", "Integration")]
    [Trait("Dependency", "network-service")]
    public void GetPositionsTest()
    {
        var positionsResult = Api.GetPositions("60", "10");

        positionsResult.Positions.Count.Should().BeGreaterThan(0);
    }
}
