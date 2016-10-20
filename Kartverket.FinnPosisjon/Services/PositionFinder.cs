using System;
using System.Collections.Generic;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class PositionFinder
    {
        public List<CoordinateSystem> SupportedCoordinateSystems { get; set; }

        public PositionsResult Find(string firstInputValue, string secondInputValue, string thirdInputValue)
        {
            var possibleCoordinates = CoordinateInputParser.GetCoordinates(firstInputValue, secondInputValue, thirdInputValue);
            var candidateCoordinates = new List<Coordinates>();

            // Keep coordinates within bounds only:
            foreach (var coordinates in possibleCoordinates)
                if (!IsOutOfBounds(coordinates))
                    candidateCoordinates.Add(coordinates);

            var positionsResult = new PositionsResult { Positions = new List<Position>() };

            // Return the empty list of positions if no coordinates could be made from the user input
            // or if no coordinates were within the bounds for any of the coordinate systems. Sad situation ...
            if (candidateCoordinates.Count == 0)
                return positionsResult;

            // TODO: Try create positions from the remaining possible coordinates ...
            
            // TESTING:

            foreach (var candidateCoordinate in candidateCoordinates)
            {
                positionsResult.Positions.Add(new Position()
                {
                    CoordinateSystem = new CoordinateSystem() { Name = "Noko" },
                    Coordinates = new Coordinates()
                    {
                        X = candidateCoordinate.X,
                        Y = candidateCoordinate.Y
                    }
                });
            }
            
            return positionsResult;
    }

        /**
         * Returns true if the coordinates is out of boundary for
         * every one of the supported coordinatesystems.
         */

        private bool IsOutOfBounds(Coordinates coordinates)
        {
            return SupportedCoordinateSystems.TrueForAll(
                coordinateSystem => coordinateSystem.IsOutOfBounds(coordinates));
        }
    }
}
