using System.Globalization;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public static class LocationTranslator
    {
        private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public static AddressData FromJson(dynamic jsonLocation)
        {
            var addressData = new AddressData
            {
                Address = GetAddress(jsonLocation),
                ZipCode = jsonLocation.postnummer ?? "",
                Place = jsonLocation.poststed ?? "",
                Municipality = jsonLocation.kommunenavn ?? "",
                Coordinates = GetCoordinates(jsonLocation),
                DistanceFromPosition = DistanceFromPosition(jsonLocation)
            };
            return addressData;
        }
        
        private static double DistanceFromPosition(dynamic jsonLocation)
        {
            string distanceFromPositionString = jsonLocation.meterDistanseTilPunkt.ToString(NumberFormat);

            return double.Parse(distanceFromPositionString, CultureInfo.InvariantCulture);
        }

        public static string GetAddress(dynamic jsonLocation)
        {
            var address = "";

            if (jsonLocation.adressetekst != null) address = jsonLocation.adressetekst;
            else if (jsonLocation.adressetekstutentilleggsnavn != null) address = jsonLocation.adressetekstutentilleggsnavn;

            return address;
        }

        private static Coordinates GetCoordinates(dynamic jsonLocation)
        {
            string lon = jsonLocation.representasjonspunkt.lon.ToString(NumberFormat);
            string lat = jsonLocation.representasjonspunkt.lat.ToString(NumberFormat);

            var jsonLocationX = double.Parse(lon, CultureInfo.InvariantCulture);
            var jsonLoactionY = double.Parse(lat, CultureInfo.InvariantCulture);

            return new Coordinates {X = new Coordinate(jsonLocationX), Y = new Coordinate(jsonLoactionY)};
        }
    }
}
