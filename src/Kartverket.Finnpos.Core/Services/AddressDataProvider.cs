using System.Globalization;
using Kartverket.Finnpos.Core.Models;
using Newtonsoft.Json.Linq;

namespace Kartverket.Finnpos.Core.Services;

public static class AddressDataProvider
{
    public static void FetchAndSet(Position position)
    {
        const string parameterizedWebServiceUrl =
            "https://ws.geonorge.no/adresser/v1/punktsok?lon={0}&lat={1}&radius={2}&treffPerSide={3}";

        var radius = 200; // meter
        const double maxRadius = 50000; // meter

        var x = position.ReferenceCoordinates.X.DecimalValue.ToString(CultureInfo.InvariantCulture);
        var y = position.ReferenceCoordinates.Y.DecimalValue.ToString(CultureInfo.InvariantCulture);

        const int hitLimit = 200;

        var addresses = new JArray();

        while (addresses.Count == 0 && (radius <= maxRadius))
        {
            var r = radius.ToString(CultureInfo.InvariantCulture);
            var callReadyUrl = string.Format(parameterizedWebServiceUrl, x, y, r, hitLimit);
            radius *= radius < 25000 ? 5 : 2; // 200, 1000, 5000, 25000, 50000
            var jsonResponseString = WebServiceCaller.GetJsonWebServiceResponse(callReadyUrl);
            if (string.IsNullOrEmpty(jsonResponseString)) break;

            dynamic jsonAddressDataResponse = JObject.Parse(jsonResponseString);

            var jsonAddresses = jsonAddressDataResponse.adresser;

            if (jsonAddresses != null)
            {
                addresses = jsonAddresses;
            }
        }

        if (addresses.Count == 0) return;

        var closestLocation = addresses[0]; // First hit on API is the closest

        var closestLocationAddressData = LocationTranslator.FromJson(closestLocation);

        position.AddressData = closestLocationAddressData;
    }
}