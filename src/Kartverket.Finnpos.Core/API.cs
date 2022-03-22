using Kartverket.Finnpos.Core.Models;
using Kartverket.Finnpos.Core.Services;

namespace Kartverket.Finnpos.Core;

public static class Api
{
    public static PositionsResult GetPositions(string firstInput, string secondInput, bool comprehensive = false)
    {
        var positionsResult = new PositionsResult { Positions = new List<Position>() };

        var coordinateInput = new CoordinateInput(firstInput, secondInput);

        if (!coordinateInput.IsParsable())
        {
            positionsResult.ParseError = true;
            return positionsResult;
        }

        // Create possible coordinates from input values:
        var coordinates = CoordinateInputParser.GetCoordinates(coordinateInput, comprehensive: comprehensive);

        // Return an empty result if no coordinates could be made from the user input
        if (coordinates.Count == 0)
        {
            positionsResult.ParseError = true;
            return positionsResult;
        }

        // Use all supported coordinatesystems if search is comprehensive, only WGS84 otherwise:
        var supportedCoordinateSystems = comprehensive
            ? CoordinateSystemsSetup.Get()
            : CoordinateSystemsSetup.Find(84);

        // Try find positions for the coordinates, within supported coordinatesystems and defined limits:
        var positionFinder = new PositionFinder { SupportedCoordinateSystems = supportedCoordinateSystems };
        positionsResult.Positions = positionFinder.Find(coordinates);
        positionsResult.Comprehensive = comprehensive || coordinates[0].X.Format.StartsWith("Deg");

        if (positionsResult.Positions.Count > 0 || positionsResult.Comprehensive)
            return positionsResult;

        // Use all coordinatesystems:
        positionFinder.SupportedCoordinateSystems = CoordinateSystemsSetup.Get();
        positionsResult.Positions = positionFinder.Find(coordinates);

        if (positionsResult.Positions.Count > 0)
            return positionsResult;

        // Auto-comprehensive - include inverted coordinatevalues:
        coordinates = CoordinateInputParser.GetCoordinates(coordinateInput, comprehensive: true);
        positionFinder.SupportedCoordinateSystems = CoordinateSystemsSetup.Get();
        positionsResult.Positions = positionFinder.Find(coordinates);
        positionsResult.Comprehensive = true;

        return positionsResult;
    }
}
