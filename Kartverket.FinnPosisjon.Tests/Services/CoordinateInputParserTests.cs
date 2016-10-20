using FluentAssertions;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests.Services
{
    public class CoordinateInputParserTests
    {
        [Fact]
        public void GetCoordinatesTest()
        {
            CoordinateInputParser.GetCoordinates("50", "50").Count.Should().Be(6);

            CoordinateInputParser.GetCoordinates("1", "1", "1").Count.Should().Be(1);
        }
    }
}
