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
            var firstNumber = Convert.ToSingle(firstInputValue);
            var secondNumber = Convert.ToSingle(secondInputValue);

            // TODO: Handle possible Z-value

            return new List<Coordinates>(new[]
            {
                new Coordinates
                {
                    // Normal order
                    East = firstNumber,
                    North = secondNumber
                },
                new Coordinates
                {
                    // Swapped order
                    East = secondNumber,
                    North = firstNumber
                },
                new Coordinates
                {
                    // Normal order, negative east
                    East = 1 - firstNumber,
                    North = secondNumber
                },
                new Coordinates
                {
                    // Swapped order, negative east
                    East = 1 - secondNumber,
                    North = firstNumber
                },
                new Coordinates
                {
                    // Normal order, negative north
                    East = firstNumber,
                    North = 1 - secondNumber
                },
                new Coordinates
                {
                    // Swapped order, negative north
                    East = secondNumber,
                    North = 1 - firstNumber
                }
            });
        }
    }
}
