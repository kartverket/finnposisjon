using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;

namespace Kartverket.FinnPosisjon.Controllers.api
{
    public class PositionsController : ApiController
    {
        public JsonResult<PositionsResult> Get(string firstInput, string secondInput, bool comprehensive = false)
        {
            var positionsResult = new PositionsResult {Positions = new List<Position>()};

            var coordinateInput = new CoordinateInput(firstInput, secondInput);

            if (!coordinateInput.IsParsable())
            {
                positionsResult.ParseError = true;
                return Json(positionsResult);
            }

            // Create coordinates from input values:
            var coordinates = CoordinateInputParser.GetCoordinates(coordinateInput);
            
            // Return an empty result if no coordinates could be made from the user input
            if (coordinates == null)
            {
                positionsResult.ParseError = true;
                return Json(positionsResult);
            }

            var coordinatesCollection = comprehensive ? CoordinateTweaker.AddSwapped(coordinates) : CoordinateTweaker.AddSwappedAndInverted(coordinates);

            // Use all supported coordinatesystems if search is comprehensive, only WGS84 otherwise:
            var supportedCoordinateSystems = comprehensive ? CoordinateSystemsSetup.Get() : CoordinateSystemsSetup.Find(84);

            // Try find positions for the coordinates, within supported coordinatesystems and defined limits:
            var positionFinder = new PositionFinder {SupportedCoordinateSystems = supportedCoordinateSystems};
            positionsResult.Positions = positionFinder.Find(coordinatesCollection);
            positionsResult.Comprehensive = comprehensive || coordinatesCollection[0].X.Format.StartsWith("Deg");

            if (positionsResult.Positions.Count > 0 || positionsResult.Comprehensive)
                return Json(positionsResult);

            // Use all coordinatesystems:
            positionFinder.SupportedCoordinateSystems = CoordinateSystemsSetup.Get();
            positionsResult.Positions = positionFinder.Find(coordinatesCollection);

            if (positionsResult.Positions.Count > 0)
                return Json(positionsResult);

            // Auto-comprehensive - include inverted coordinatevalues:
            coordinatesCollection = CoordinateInputParser.GetCoordinates(coordinateInput);
            positionFinder.SupportedCoordinateSystems = CoordinateSystemsSetup.Get();
            positionsResult.Positions = positionFinder.Find(coordinatesCollection);
            positionsResult.Comprehensive = true;

            return Json(positionsResult);
        }
    }
}
