using System.Globalization;
using FluentAssertions;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests.Services
{
    public class CoordinateTransformerTests
    {
        private static double ClipDecimals(double coordinate, int length)
        {
            var coordinateString = coordinate.ToString(CultureInfo.InvariantCulture);

            coordinateString = coordinateString.Length >= length
                ? coordinateString.Substring(0, length)
                : coordinateString;

            return double.Parse(coordinateString, CultureInfo.InvariantCulture);
        }

        [Fact]
        public void TransformTest()
        {
            Coordinates originalCoordinates;
            Coordinates transformedCoordinates;

            // UTM Zone 32
            originalCoordinates = new Coordinates {X = new Coordinate(503337.18), Y = new Coordinate(6586036.109)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 22);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 5).Should().Be(9.058);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(59.412);

            // UTM Zone 33
            originalCoordinates = new Coordinates {X = new Coordinate(363414.116), Y = new Coordinate(7121778.269)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 23);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(12.187);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(64.195);

            // UTM Zone 35
            originalCoordinates = new Coordinates {X = new Coordinate(551933.623), Y = new Coordinate(7826965.416)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 25);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(28.397);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(70.542);

            // NGO48 Axis 1
            originalCoordinates = new Coordinates {X = new Coordinate(48510.86), Y = new Coordinate(9411.102)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 1);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 5).Should().Be(6.874);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(58.082);

            // NGO48 Axis 2
            originalCoordinates = new Coordinates {X = new Coordinate(4682.041), Y = new Coordinate(116386.281)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 2);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 5).Should().Be(8.466);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(59.045);

            // NGO48 Axis 3
            originalCoordinates = new Coordinates {X = new Coordinate(3543.431), Y = new Coordinate(208365.517)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 3);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(10.781);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(59.871);

            // NGO48 Axis 4
            originalCoordinates = new Coordinates {X = new Coordinate(-6621.688), Y = new Coordinate(680359.286)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 4);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(13.081);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(64.106);

            // NGO48 Axis 5
            originalCoordinates = new Coordinates {X = new Coordinate(-65023.284), Y = new Coordinate(982844.292)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 5);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(15.403);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(66.812);

            // NGO48 Axis 6
            originalCoordinates = new Coordinates {X = new Coordinate(-47439.511), Y = new Coordinate(1201409.738)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 6);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(19.708);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(68.775);

            // NGO48 Axis 7
            originalCoordinates = new Coordinates {X = new Coordinate(-39261.983), Y = new Coordinate(1391910.869)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 7);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(23.828);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(70.484);

            // NGO48 Axis 8
            originalCoordinates = new Coordinates {X = new Coordinate(12303.924), Y = new Coordinate(1348932.315)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 8);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(29.371);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(70.102);

            // WGS84
            originalCoordinates = new Coordinates {X = new Coordinate(10.753), Y = new Coordinate(59.905)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 84);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(10.753);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(59.905);

            // Oslo local coordinate system
            originalCoordinates = new Coordinates { X = new Coordinate(1936.87), Y = new Coordinate(-597.994)};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates.X.DecimalValue, originalCoordinates.Y.DecimalValue, 101);
            ClipDecimals(transformedCoordinates.X.DecimalValue, 6).Should().Be(10.752);
            ClipDecimals(transformedCoordinates.Y.DecimalValue, 6).Should().Be(59.907);
        }
    }
}
