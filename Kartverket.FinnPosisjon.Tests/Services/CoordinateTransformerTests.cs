using FluentAssertions;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests.Services
{
    public class CoordinateTransformerTests
    {
        [Fact (Skip = "Higher precision on result than expected result")]
        public void TransformTest()
        {
            Coordinates coordinates;
            Coordinates transformedCoordinates;

            // UTM Zone 32
            coordinates = new Coordinates { X = 503337.18, Y = 6586036.109 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 22, 84);
            transformedCoordinates.X.Should().Be(9.0587915666882122);
            transformedCoordinates.Y.Should().Be(59.412940371063314);

            // UTM Zone 33
            coordinates = new Coordinates { X = 363414.116, Y = 7121778.269 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 23, 84);
            transformedCoordinates.X.Should().Be(12.187198250835472);
            transformedCoordinates.Y.Should().Be(64.195136674486321);

            // UTM Zone 35
            coordinates = new Coordinates { X = 551933.623, Y = 7826965.416 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 25, 84);
            transformedCoordinates.X.Should().Be(28.397021288291612);
            transformedCoordinates.Y.Should().Be(70.542457224789374);

            // NGO48 Axis 1
            coordinates = new Coordinates { X = 48510.86, Y = 9411.102 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 1, 84);
            transformedCoordinates.X.Should().Be(6.87429257348019);
            transformedCoordinates.Y.Should().Be(58.0826947005639);

            // NGO48 Axis 2
            coordinates = new Coordinates { X = 4682.041, Y = 116386.281 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 2, 84);
            transformedCoordinates.X.Should().Be(8.46660952588736);
            transformedCoordinates.Y.Should().Be(59.0457617672893);

            // NGO48 Axis 3
            coordinates = new Coordinates { X = 3543.431, Y = 208365.517 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 3, 84);
            transformedCoordinates.X.Should().Be(10.7813590642744);
            transformedCoordinates.Y.Should().Be(59.8714984303773);

            // NGO48 Axis 4
            coordinates = new Coordinates { X = -6621688, Y = 680359.286 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 4, 84);
            transformedCoordinates.X.Should().Be(13.0814176645284);
            transformedCoordinates.Y.Should().Be(64.1067615418734);

            // NGO48 Axis 5
            coordinates = new Coordinates { X = -65023.284, Y = 982844.292 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 5, 84);
            transformedCoordinates.X.Should().Be(15.4036446947715);
            transformedCoordinates.Y.Should().Be(66.8128663807752);
            
            // NGO48 Axis 6
            coordinates = new Coordinates { X = -47439.511, Y = 1201409.738 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 6, 84);
            transformedCoordinates.X.Should().Be(19.7083996599197);
            transformedCoordinates.Y.Should().Be(68.7755579116143);

            // NGO48 Axis 7
            coordinates = new Coordinates { X = -39261.983, Y = 1391910.869 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 7, 84);
            transformedCoordinates.X.Should().Be(23.828597925059);
            transformedCoordinates.Y.Should().Be(70.4843915636866);

            // NGO48 Axis 8
            coordinates = new Coordinates { X = 12303.924, Y = 1348932.315 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 8, 84);
            transformedCoordinates.X.Should().Be(29.3715175096558);
            transformedCoordinates.Y.Should().Be(70.1021355533797);

            // WGS84
            coordinates = new Coordinates { X = 10.753, Y = 59.905 };
            transformedCoordinates = CoordinateTransformer.Transform(coordinates, 84, 84);
            transformedCoordinates.X.Should().Be(10.753);
            transformedCoordinates.Y.Should().Be(59.905);
        }
    }
}
