using System.Linq;
using FluentAssertions;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests
{
    public class PositionFinderTest
    {
        private static readonly PositionFinder PositionFinder = new PositionFinder
        {
            SupportedCoordinateSystems = CoodinateSystemsSetup.Get()
        };

        [Fact(Skip = "PositionFinder.Find not yet implemented")]
        public void ShouldFindPositionWithCoordSysEu89UtmZone33()
        {
            var positionsResult = PositionFinder.Find("163067.449", "6601114.654", null);

            var position = positionsResult.Positions.First();

            position.CoordinateSystem.Name.Should().Be("EU89, UTM-sone 33");
        }

        [Fact(Skip = "Test is unfinished")]
        public void ShouldFindOnePositionWithinBounderies()
        {
            var positionsResult = PositionFinder.Find("45", "15", null);

            positionsResult.Positions.Count.Should().Be(1);
        }
    }
}
