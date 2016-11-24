using FluentAssertions;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests.Services
{
    public class LocationDataProviderTests
    {
        [Fact]
        public void FetchAndSetTest()
        {
            var position = new Position
            {
                ReferenceCoordinates = new Coordinates {X = new Coordinate(9.05871164), Y = new Coordinate(59.41283416)}
            };

            AddressDataProvider.FetchAndSet(position);

            //position.AddressData.Address.Should().Be("Kyrkjevegen");

            position.AddressData.Place.Should().Be("BØ I TELEMARK");

            position.AddressData.ZipCode.Should().Be("3800");

            position.AddressData.DistanceFromPosition.Should().BeLessOrEqualTo(100);
        }
    }
}
