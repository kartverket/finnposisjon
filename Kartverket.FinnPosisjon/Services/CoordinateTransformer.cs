using System.Globalization;
using System.Web.Helpers;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateTransformer
    {
        private const string UrlTemplate =
            "http://www.norgeskart.no/ws/trans.py?ost={0}&nord={1}&sosiKoordSys={2}&resSosiKoordSys={3}";

        private const int ToSosiCodeDefault = 84;

        public static Coordinates Transform(double xCoordinate, double yCoordinate,
            int fromSosiCode, int toSosiCode = ToSosiCodeDefault)
        {
            if (fromSosiCode == 101) // Is Oslo local coord.sys.
            {
                // Converts to NGO1948 Axis 3 for transformation:
                fromSosiCode = 3;
                xCoordinate += 0.102;
                yCoordinate += 212979.333;
            }

            var east = xCoordinate.ToString(CultureInfo.InvariantCulture);
            var north = yCoordinate.ToString(CultureInfo.InvariantCulture);

            var url = string.Format(UrlTemplate, east, north, fromSosiCode, toSosiCode);

            var json = WebServiceCaller.GetJsonWebServiceResponse(url);

            if (string.IsNullOrEmpty(json)) return null;

            var transformationResponse = Json.Decode<TransformationResponse>(json);

            return transformationResponse.ErrKode == 0
                ? new Coordinates
                {
                    X = transformationResponse.Ost,
                    Y = transformationResponse.Nord
                }
                : null;
        }
    }

    public class TransformationResponse
    {
        public int ErrKode { get; set; }
        public int SosiKoordSys { get; set; }
        public double Ost { get; set; }
        public double Hoyde { get; set; }
        public double Nord { get; set; }
        public string ErrTekst { get; set; }
    }
}
