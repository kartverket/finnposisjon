using System.Globalization;
using System.Web.Helpers;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public static class AddressDataProvider
    {
        public static void FetchAndSet(Position position)
        {
            const string parameterizedWebServiceUrl =
                "https://ws.geonorge.no/adresser/v1/punktsok?lon={0}&lat={1}&radius={2}&treffPerSide={3}";

            var radius = 200; // meter
            const double maxRadius = 25000; // meter

            var x = position.ReferenceCoordinates.X.DecimalValue.ToString(CultureInfo.InvariantCulture);
            var y = position.ReferenceCoordinates.Y.DecimalValue.ToString(CultureInfo.InvariantCulture);

            const int hitLimit = 200;

            object[] addresses = new object[0];

            while (addresses.Length == 0 && (radius <= maxRadius))
            {
                var r = radius.ToString(CultureInfo.InvariantCulture);
                var callReadyUrl = string.Format(parameterizedWebServiceUrl, x, y, r, hitLimit);
                radius *= 5;
                var jsonResponseString = WebServiceCaller.GetJsonWebServiceResponse(callReadyUrl);
                if (string.IsNullOrEmpty(jsonResponseString)) break;

                dynamic jsonAddressDataResponse = Json.Decode(jsonResponseString);

                var jsonAddresses = jsonAddressDataResponse.adresser;

                if (jsonAddresses != null)
                {
                    addresses = jsonAddresses.GetType() == typeof(DynamicJsonArray)
                        ? jsonAddresses
                        : new object[] { jsonAddresses };
                }
                
            }

            if (addresses.Length == 0) return;

            var closestLocation = addresses[0]; // First hit on API is the closest

            var closestLocationAddressData = LocationTranslator.FromJson(closestLocation);

            position.AddressData = closestLocationAddressData;
        }
    }
}
