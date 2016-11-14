using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Helpers;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateTransformer
    {
        public static Coordinates Transform(Coordinates coordinates, int coordinateSystemSosiCode,
            int resultCoordinateSystemSosiCode)
        {
            const string parameterizedWebServiceUrl =
                "http://www.norgeskart.no/ws/trans.py?ost={0}&nord={1}&sosiKoordSys={2}&resSosiKoordSys={3}";

            var x = coordinates.X.ToString(CultureInfo.InvariantCulture);
            var y = coordinates.Y.ToString(CultureInfo.InvariantCulture);

            var callReadyUrl = string.Format(parameterizedWebServiceUrl, x, y, coordinateSystemSosiCode, resultCoordinateSystemSosiCode);

            var json = WebServiceCaller.GetJsonWebServiceResponse(callReadyUrl);

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
