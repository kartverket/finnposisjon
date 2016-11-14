using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Helpers;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public static class AddressDataProvider
    {
        public static void FetchAndSet(Position position)
        {
            const string parameterizedWebServiceUrl =
                "http://ws.geonorge.no/AdresseWS/adresse/radius?aust={0}&nord={1}&radius={2}&antPerSide={3}";

            var radius = 0.2; // km
            const double maxRadius = 25; // km

            var x = position.ReferenceCoordinates.X.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
            var y = position.ReferenceCoordinates.Y.ToString(CultureInfo.CreateSpecificCulture("en-GB"));

            const int hitLimit = 200;

            object[] addresses = null;

            while ((addresses == null) && (radius <= maxRadius))
            {
                var r = radius.ToString(CultureInfo.InvariantCulture);
                var callReadyUrl = string.Format(parameterizedWebServiceUrl, x, y, r, hitLimit);
                var jsonResponseString = WebServiceCaller.GetJsonWebServiceResponse(callReadyUrl);
                dynamic jsonAddressDataResponse = Json.Decode(jsonResponseString);

                var jsonAddresses = jsonAddressDataResponse.adresser;

                if (jsonAddresses != null)
                {
                    addresses = jsonAddresses.GetType() == typeof(DynamicJsonArray)
                        ? jsonAddresses
                        : new object[] { jsonAddresses };
                }
                radius *= 5;
            }

            if (addresses == null) return;

            var addressDataList = new List<AddressData>();

            foreach (var jsonLocation in addresses)
                addressDataList.Add(LocationTranslator.FromJson(jsonLocation));

            var closetsLocation = GetClosestLocation(addressDataList, position.ReferenceCoordinates);

            position.AddressData = closetsLocation;
        }

        private static AddressData GetClosestLocation(List<AddressData> addressDataList, Coordinates refCoordinates)
        {
            foreach (var addressData in addressDataList)
                addressData.DistanceFromPosition = GetDistance(addressData.Coordinates, refCoordinates);

            return addressDataList.OrderBy(a => a.DistanceFromPosition).ToList().First();
        }

        private static double GetDistance(Coordinates addressDataCoordinates, Coordinates refCoordinates)
        {
            const double r = 6387497.792; // metres
            var φ1 = ToRadians(refCoordinates.X);
            var φ2 = ToRadians(addressDataCoordinates.X);
            var Δφ = ToRadians(addressDataCoordinates.X - refCoordinates.X);
            var Δλ = ToRadians(addressDataCoordinates.Y - refCoordinates.Y);

            var a = Math.Sin(Δφ/2)*Math.Sin(Δφ/2) +
                    Math.Cos(φ1)*Math.Cos(φ2)*
                    Math.Sin(Δλ/2)*Math.Sin(Δλ/2);
            var c = 2*Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = r*c;
            return Math.Round(d, 2);
        }

        private static double ToRadians(double number)
        {
            return number*Math.PI/180;
        }
    }
}