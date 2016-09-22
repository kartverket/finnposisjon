using System.Linq;
using FluentAssertions;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests
{
    public class PositionFinderTest
    {
        [Fact (Skip = "PositionFinder.Find not yet implemented")]
        public void ShouldFindPositionWithCoordSysEu89UtmZone33()
        {
            var userInputCoordinates = new[] {"163067.449", "6601114.654"};

            var possiblePositions = new PositionFinder().Find(userInputCoordinates);

            var position = possiblePositions.First();

            position.CoordinateSystem.Should().Be("EU89, UTM-sone 33");
        }
    }
}
