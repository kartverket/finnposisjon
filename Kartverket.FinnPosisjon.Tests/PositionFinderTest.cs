using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests
{
    public class PositionFinderTest
    {
        private static readonly PositionFinder PositionFinder = new PositionFinder
        {
            SupportedCoordinateSystems = CoordinateSystemsSetup.Get()
        };

        [Fact(Skip = "Test is unfinished")]
        public void ShouldFindPositionWithCoordSysEu89UtmZone33()
        {
            var coordinates = new Coordinates {X = 163067.449, Y = 6601114.654};

            var positions = PositionFinder.Find(new List<Coordinates> {coordinates});

            positions.First().CoordinateSystem.Name.Should().Be("EUREF89, sone 33");
        }
    }
}
