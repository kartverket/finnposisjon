﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;

namespace Kartverket.FinnPosisjon.Controllers.api
{
    public class PositionsController : ApiController
    {
        public JsonResult<PositionsResult> Get(string firstInput, string secondInput, string thirdInput)
        {
            var positionsResult = new PositionsResult {Positions = new List<Position>()};

            // Return an empty result if user input is invalid
            if (string.IsNullOrWhiteSpace(firstInput) || string.IsNullOrWhiteSpace(secondInput))
                return Json(positionsResult);

            // Create possible coordinates from 2 or 3 input values
            var coordinates = string.IsNullOrWhiteSpace(thirdInput)
                ? CoordinateInputParser.GetCoordinates(firstInput, secondInput)
                : CoordinateInputParser.GetCoordinates(firstInput, secondInput, thirdInput);

            // Return an empty result if no coordinates could be made from the user input
            if (coordinates.Count == 0) return Json(positionsResult);

            // Try find positions for the coordinates, within supported coordinatesystems and defined limits
            var positionFinder = new PositionFinder {SupportedCoordinateSystems = CoordinateSystemsSetup.Get()};
            positionsResult.Positions = positionFinder.Find(coordinates);

            return Json(positionsResult);
        }
    }
}
