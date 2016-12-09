using System.Web.Configuration;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.no.geonorge.ws;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateTransformer
    {
        private const int ToSosiCodeDefault = 84;

        public static Coordinates Transform(double xCoordinate, double yCoordinate,
            int fromSosiCode, int toSosiCode = ToSosiCodeDefault)  // TODO: Batch-transform?
        {
            if (fromSosiCode == 101) // Is Oslo local coord.sys.
            {
                // Converts to NGO1948 Axis 3 for transformation:
                fromSosiCode = 3;
                xCoordinate += 0.102;
                yCoordinate += 212979.333;
            }

            var transClient = new TranClient();

            var transInputCoords = new TranInpKoordSet
            {
                kommuneNr = 0,
                hoyde = 0.0,
                nord = yCoordinate,
                ost = xCoordinate,
                sosiKoordSys = fromSosiCode
            };

            var transInput = new TranInpData {inpKoordSets = new TranInpKoordSet[1]};
            transInput.inpKoordSets[0] = transInputCoords;
            transInput.resSosiKoordSys = toSosiCode;

            var username = WebConfigurationManager.AppSettings["SosiTransUsername"];
            var password = WebConfigurationManager.AppSettings["SosiTransPassword"];

            var transResult = transClient.sosiTrans(username, password, "", transInput);

            var transformedCoords = transResult.resKoordSets[0];

            var transformedX = transformedCoords.ost;
            var transformedY = transformedCoords.nord;

            if (transformedCoords.sosiKoordSys == 84 && transResult.inpKoordSets[0].sosiKoordSys != 84)
            {
                transformedX = transformedX/3600;
                transformedY = transformedY/3600;
            }

            return new Coordinates
            {
                X = new Coordinate(transformedX),
                Y = new Coordinate(transformedY)
            };
        }
    }
}
