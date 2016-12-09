using System.Web.Configuration;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.no.geonorge.ws;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateTransformer
    {
        private const int ToCoordSysDefault = 84;

        public static Coordinates Transform(double xCoordinate, double yCoordinate,
            int fromCoordSys, int toCoordSys = ToCoordSysDefault)  // TODO: Batch-transform?
        {
            if (fromCoordSys == 101) // Is Oslo local coord.sys.
            {
                // Converts to NGO1948 Axis 3 for transformation:
                fromCoordSys = 3;
                xCoordinate += 0.102;
                yCoordinate += 212979.333;
            }

            var transClient = new TranClient();

            var transInputCoords = new TranInpKoordSet
            {
                nord = yCoordinate,
                ost = xCoordinate,
                sosiKoordSys = fromCoordSys
            };

            var transInput = new TranInpData {inpKoordSets = new TranInpKoordSet[1]};
            transInput.inpKoordSets[0] = transInputCoords;
            transInput.resSosiKoordSys = toCoordSys;

            var username = WebConfigurationManager.AppSettings["SosiTransUsername"];
            var password = WebConfigurationManager.AppSettings["SosiTransPassword"];

            var transResult = transClient.sosiTrans(username, password, "", transInput);

            if (!transResult.ok)
            {
                // TODO: Log transResult.melding
                return null;
            }

            var yCoordinateTransformed = transResult.resKoordSets[0].ost;
            var xCoordinateTransformed = transResult.resKoordSets[0].nord;

            if (IsArcSeconds(transResult))
            {
                // Convert to degrees
                yCoordinateTransformed /= 3600;
                xCoordinateTransformed /= 3600;
            }

            return new Coordinates
            {
                X = new Coordinate(yCoordinateTransformed),
                Y = new Coordinate(xCoordinateTransformed)
            };
        }

        private static bool IsArcSeconds(TranRes transResult)
        {
            return transResult.resKoordSets[0].sosiKoordSys == 84 && transResult.inpKoordSets[0].sosiKoordSys != 84;
        }
    }
}
