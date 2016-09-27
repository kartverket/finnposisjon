using System;
using System.Collections.Generic;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class PositionFinder
    {
        public List<Position> Find(string[] coordinateSet)
        {
            var possibleCoordinates = GetPossibleCoordinates(coordinateSet);
            
            // TODO: Create positions from relevant coordinates ...

            var positions = new List<Position>();
            
            return positions;
        }

        private static IEnumerable<Coordinates> GetPossibleCoordinates(IReadOnlyList<string> coordinateSet)
        {
            var firstNumber = Convert.ToSingle(coordinateSet[0]);
            var secondNumber = Convert.ToSingle(coordinateSet[1]);

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
                }
            });
        }
    }
}
