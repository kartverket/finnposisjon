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
            var possibleCoordinates = GetPossibleCoordinates(firstInputValue, secondInputValue, thirdInputValue);
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

        private static List<Coordinates> GetPossibleCoordinates(string firstInputValue, string secondInputValue, string thirdInputValue)
        {
            var possibleCoordinates = new List<Coordinates>();

            double parsedFirstNumber;
            double parsedSecondNumber;

            var firstValueWasParsed = double.TryParse(firstInputValue, out parsedFirstNumber);
            var secondValueWasParsed = double.TryParse(secondInputValue, out parsedSecondNumber);

            if (firstValueWasParsed && secondValueWasParsed)
                possibleCoordinates.AddRange(new[]
                {
                    new Coordinates
                    {
                        // Normal order
                        East = parsedFirstNumber,
                        North = parsedSecondNumber
                    },
                    new Coordinates
                    {
                        // Swapped order
                        East = parsedSecondNumber,
                        North = parsedFirstNumber
                    },
                    new Coordinates
                    {
                        // Normal order, negative east
                        East = 1 - parsedFirstNumber,
                        North = parsedSecondNumber
                    },
                    new Coordinates
                    {
                        // Swapped order, negative east
                        East = 1 - parsedSecondNumber,
                        North = parsedFirstNumber
                    },
                    new Coordinates
                    {
                        // Normal order, negative north
                        East = parsedFirstNumber,
                        North = 1 - parsedSecondNumber
                    },
                    new Coordinates
                    {
                        // Swapped order, negative north
                        East = parsedSecondNumber,
                        North = 1 - parsedFirstNumber
                    }
                });

            return possibleCoordinates;
        }
    }
}
