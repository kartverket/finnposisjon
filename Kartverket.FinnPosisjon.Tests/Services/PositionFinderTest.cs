using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests.Services
{
    public class PositionFinderTest
    {
        private static readonly PositionFinder PositionFinder = new PositionFinder
        {
            SupportedCoordinateSystems = CoordinateSystemsSetup.Get()
        };

        [Fact]
        public void ShouldFindPositionWithCoordSysEu89UtmZone33()
        {
            var coordinates = new Coordinates {X = new Coordinate(288889.7639), Y = new Coordinate(7231445.376)};

            var positions = PositionFinder.Find(new List<Coordinates> {coordinates});

            positions.Any(p => p.CoordinateSystem.Name == "EUREF89, UTM-sone 33").Should().BeTrue();
        }
    }
}
