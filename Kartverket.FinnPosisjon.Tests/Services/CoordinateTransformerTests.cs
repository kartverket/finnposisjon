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
        public void TransformTest()
        {
            Coordinates originalCoordinates;
            Coordinates transformedCoordinates;

            // UTM Zone 32
            originalCoordinates = new Coordinates {X = 503337.18, Y = 6586036.109};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 22, 84);
            ClipDecimals(transformedCoordinates.X, 5).Should().Be(9.058);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(59.412);

            // UTM Zone 33
            originalCoordinates = new Coordinates {X = 363414.116, Y = 7121778.269};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 23, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(12.187);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(64.195);

            // UTM Zone 35
            originalCoordinates = new Coordinates {X = 551933.623, Y = 7826965.416};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 25, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(28.397);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(70.542);

            // NGO48 Axis 1
            originalCoordinates = new Coordinates {X = 48510.86, Y = 9411.102};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 1, 84);
            ClipDecimals(transformedCoordinates.X, 5).Should().Be(6.874);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(58.082);

            // NGO48 Axis 2
            originalCoordinates = new Coordinates {X = 4682.041, Y = 116386.281};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 2, 84);
            ClipDecimals(transformedCoordinates.X, 5).Should().Be(8.466);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(59.045);

            // NGO48 Axis 3
            originalCoordinates = new Coordinates {X = 3543.431, Y = 208365.517};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 3, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(10.781);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(59.871);

            // NGO48 Axis 4
            originalCoordinates = new Coordinates {X = -6621.688, Y = 680359.286};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 4, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(13.081);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(64.106);

            // NGO48 Axis 5
            originalCoordinates = new Coordinates {X = -65023.284, Y = 982844.292};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 5, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(15.403);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(66.812);

            // NGO48 Axis 6
            originalCoordinates = new Coordinates {X = -47439.511, Y = 1201409.738};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 6, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(19.708);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(68.775);

            // NGO48 Axis 7
            originalCoordinates = new Coordinates {X = -39261.983, Y = 1391910.869};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 7, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(23.828);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(70.484);

            // NGO48 Axis 8
            originalCoordinates = new Coordinates {X = 12303.924, Y = 1348932.315};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 8, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(29.371);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(70.102);

            // WGS84
            originalCoordinates = new Coordinates {X = 10.753, Y = 59.905};
            transformedCoordinates = CoordinateTransformer.Transform(originalCoordinates, 84, 84);
            ClipDecimals(transformedCoordinates.X, 6).Should().Be(10.753);
            ClipDecimals(transformedCoordinates.Y, 6).Should().Be(59.905);


            // Oslo local coordinate system
            /*
            coordinates = new Coordinates { X = 1936.87, Y = -597.994 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 101, 84);
            ClipDecimals(transformedCoordinates.X, ).Should().Be();
            ClipDecimals(transformedCoordinates.Y, ).Should().Be();
            */
        }
        private static double ClipDecimals(double coordinate, int length)
        {
            return double.Parse(coordinate.ToString(CultureInfo.InvariantCulture).Substring(0, length),
                CultureInfo.InvariantCulture);
        }
    }
}
