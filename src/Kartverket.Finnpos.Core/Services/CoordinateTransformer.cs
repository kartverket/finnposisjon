using System.Globalization;
using Kartverket.Finnpos.Core.Models;
using RestSharp;

namespace Kartverket.Finnpos.Core.Services;

public class CoordinateTransformer
{
    private const int ToCoordSysDefault = 84;
    private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };

    public static Coordinates Transform(double xCoordinate, double yCoordinate,
        int fromCoordSys, int toCoordSys = ToCoordSysDefault) // TODO: Batch-transform?
    {
        if (fromCoordSys == 101) // Is Oslo local coord.sys.
            return Transform(xCoordinate + 0.102, yCoordinate + 212979.333, 3, toCoordSys); // From NGO1948 Axis 3

        var restClient = new RestClient("https://ws.geonorge.no/SkTransRestWS/");

        var requestString = "transformer?system=koordsys"
                            + "&frasys=" + fromCoordSys
                            + "&tilsys=" + toCoordSys
                            + "&lengde=" + xCoordinate.ToString(NumberFormat)
                            + "&bredde=" + yCoordinate.ToString(NumberFormat);

        var skTransRequest = new RestRequest(requestString);

        var skTransResponse = restClient.ExecuteAsync<SKTransResponse>(skTransRequest).Result;

        return (int)skTransResponse.Data.TransErr != 0
            ? null
            : new Coordinates
            {
                X = new Coordinate(skTransResponse.Data.Tily), Y = new Coordinate(skTransResponse.Data.Tilx)
            };
    }
}

public class SKTransResponse
{
    public double Tilx { get; set; }
    public double Tily { get; set; }
    public double TransErr { get; set; }
}
