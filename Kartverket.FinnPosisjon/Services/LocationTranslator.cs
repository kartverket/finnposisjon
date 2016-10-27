using System.Globalization;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public static class LocationTranslator
    {
        public static AddressData FromJson(dynamic jsonLocation)
        {
            var addressData = new AddressData
            {
                Address = GetAddress(jsonLocation),
                ZipCode = jsonLocation.postnr ?? "",
                Place = jsonLocation.poststed ?? "",
                Municipality = jsonLocation.kommunenavn ?? "",
                Coordinates = GetCoordinates(jsonLocation)
            };
            return addressData;
        }
        
        public static string GetAddress(dynamic jsonLocation)
        {
            var address = "";

            if (jsonLocation.adressenavn != null) address = jsonLocation.adressenavn;
            else if (jsonLocation.kortadressenavn != null) address = jsonLocation.kortadressenavn;
            else if (jsonLocation.adressetilleggsnavn != null) address = jsonLocation.adressetilleggsnavn;
            else if (jsonLocation.kortnavn != null) address = jsonLocation.kortnavn;

            return address;
        }

        private static Coordinates GetCoordinates(dynamic jsonLocation)
        {
            var jsonLocationX = double.Parse(jsonLocation.aust, CultureInfo.InvariantCulture);
            var jsonLoactionY = double.Parse(jsonLocation.nord, CultureInfo.InvariantCulture);

            return new Coordinates {X = jsonLocationX, Y = jsonLoactionY};
        }
    }
}
