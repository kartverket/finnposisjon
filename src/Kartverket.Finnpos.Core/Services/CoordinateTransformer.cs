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

        var restClient = new RestClient("https://ws.geonorge.no/transformering/v1/");

        var requestString = "transformer?"
                            + "&fra=" + CoordinateSystemsSetup.Find(fromCoordSys).First().EpsgCode
                            + "&til=" + CoordinateSystemsSetup.Find(toCoordSys).First().EpsgCode
                            + "&x=" + xCoordinate.ToString(NumberFormat)
                            + "&y=" + yCoordinate.ToString(NumberFormat);

        var skTransRequest = new RestRequest(requestString);

        var serviceResponse = restClient.ExecuteAsync<TransformationServiceResponse>(skTransRequest).Result;

        return serviceResponse.Data == null
            ? null
            : new Coordinates
            {
                X = new Coordinate(serviceResponse.Data.X), Y = new Coordinate(serviceResponse.Data.Y)
            };
    }
}

public class TransformationServiceResponse
{
    public double X { get; set; }
    public double Y { get; set; }
}
