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
            // (1,2) (2,1) Normal/swapped order
            CoordinateInputParser.GetCoordinates("1", "2").Count.Should().Be(2);

            // (1,2) (2,1) (-1,2) (1,-2) (-2,1) (2,-1) Normal/swapped order with/without inverted X/Y
            CoordinateInputParser.GetCoordinates("1", "1", true).Count.Should().Be(6);

            CoordinateInputParser.GetCoordinates("1", "1", "1").Count.Should().Be(1);
        }
    }
}
