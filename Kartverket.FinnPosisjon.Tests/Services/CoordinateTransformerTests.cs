using System.Globalization;
using FluentAssertions;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests.Services
{
    public class CoordinateTransformerTests
    {
        [Fact]
        public void ShouldTransformNgo48Axis1Coordinates()
        {
            const int fromSys = 1; // NGO48 Axis 1

            var coords = new Coordinates {X = new Coordinate(48510.86), Y = new Coordinate(9411.102)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 5).Should().Be(6.874);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(58.082);
        }

        [Fact]
        public void ShouldTransformNgo48Axis2Coordinates()
        {
            const int fromSys = 2; // NGO48 Axis 2

            var coords = new Coordinates {X = new Coordinate(4682.041), Y = new Coordinate(116386.281)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 5).Should().Be(8.466);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(59.045);
        }

        [Fact]
        public void ShouldTransformNgo48Axis3Coordinates()
        {
            const int fromSys = 3; // NGO48 Axis 3

            var coords = new Coordinates {X = new Coordinate(3543.431), Y = new Coordinate(208365.517)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(10.781);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(59.871);
        }

        [Fact]
        public void ShouldTransformNgo48Axis4Coordinates()
        {
            const int fromSys = 4; // NGO48 Axis 4

            var coords = new Coordinates {X = new Coordinate(-6621.688), Y = new Coordinate(680359.286)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(13.081);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(64.106);
        }

        [Fact]
        public void ShouldTransformNgo48Axis5Coordinates()
        {
            const int fromSys = 5; // NGO48 Axis 5

            var coords = new Coordinates {X = new Coordinate(-65023.284), Y = new Coordinate(982844.292)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(15.403);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(66.812);
        }

        [Fact]
        public void ShouldTransformNgo48Axis6Coordinates()
        {
            const int fromSys = 6; // NGO48 Axis 6

            var coords = new Coordinates {X = new Coordinate(-47439.511), Y = new Coordinate(1201409.738)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(19.708);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(68.775);
        }

        [Fact]
        public void ShouldTransformNgo48Axis7Coordinates()
        {
            const int fromSys = 7; // NGO48 Axis 7

            var coords = new Coordinates {X = new Coordinate(-39261.983), Y = new Coordinate(1391910.869)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(23.828);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(70.484);
        }

        [Fact]
        public void ShouldTransformNgo48Axis8Coordinates()
        {
            const int fromSys = 8; // NGO48 Axis 8

            var coords = new Coordinates {X = new Coordinate(12303.924), Y = new Coordinate(1348932.315)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(29.371);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(70.102);
        }

        [Fact]
        public void ShouldTransformOsloLocalSystemCoordinates()
        {
            const int fromSys = 101; // Oslo local coordinate system

            var coords = new Coordinates {X = new Coordinate(1936.87), Y = new Coordinate(-597.994)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(10.752);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(59.907);
        }

        [Fact]
        public void ShouldTransformUtmZone32Coordinates()
        {
            const int fromSys = 22; // UTM Zone 32

            var coords = new Coordinates {X = new Coordinate(503337.18), Y = new Coordinate(6586036.109)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 5).Should().Be(9.058);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(59.412);
        }

        [Fact]
        public void ShouldTransformUtmZone33Coordinates()
        {
            const int fromSys = 23; // UTM Zone 33

            var coords = new Coordinates {X = new Coordinate(363414.116), Y = new Coordinate(7121778.269)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(12.187);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(64.195);
        }

        [Fact]
        public void ShouldTransformUtmZone35Coordinates()
        {
            const int fromSys = 25; // UTM Zone 35

            var coords = new Coordinates {X = new Coordinate(551933.623), Y = new Coordinate(7826965.416)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue,
                coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(28.397);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(70.542);
        }

        [Fact]
        public void ShouldTransformWgs84Coordinates()
        {
            const int fromSys = 84; // WGS84

            var coords = new Coordinates {X = new Coordinate(10.753), Y = new Coordinate(59.905)};

            var transCoords = CoordinateTransformer.Transform(coords.X.DecimalValue, coords.Y.DecimalValue, fromSys);

            ClipDecimals(transCoords.X.DecimalValue, 6).Should().Be(10.753);
            ClipDecimals(transCoords.Y.DecimalValue, 6).Should().Be(59.905);
        }

        private static double ClipDecimals(double coordinate, int length)
        {
            var coordinateString = coordinate.ToString(CultureInfo.InvariantCulture);

            coordinateString = coordinateString.Length >= length
                ? coordinateString.Substring(0, length)
                : coordinateString;

            return double.Parse(coordinateString, CultureInfo.InvariantCulture);
        }
    }
}
