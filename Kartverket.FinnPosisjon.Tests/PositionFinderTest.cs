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
            var positionsResult = PositionFinder.Find("163067,449", "6601114,654", null);

            var position = positionsResult.Positions.FirstOrDefault();

            position.Should().NotBeNull();

            position?.CoordinateSystem.Name.Should().Be("EUREF89");
        }

        [Fact(Skip = "Test is unfinished")]
        public void ShouldFindOnePositionWithinBounderies()
        {
            var positionsResult = PositionFinder.Find("45", "15", null);

            positionsResult.Positions.Count.Should().Be(1);
        }
    }
}
