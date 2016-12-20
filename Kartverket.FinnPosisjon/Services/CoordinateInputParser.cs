using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateInputParser
    {
        public static Coordinates GetCoordinates(CoordinateInput coordinateInput)
        {
            var firstInput = coordinateInput.FirstInput;
            var secondInput = coordinateInput.SecondInput;

            var coordinates = new Coordinates();

            var firstInputNumberUnits = GetNumberUnits(firstInput);
            var secondInputNumberUnits = GetNumberUnits(secondInput);

            if ((firstInputNumberUnits.Count > 3) || (secondInputNumberUnits.Count > 3))
                return null; // Invalid format

            if (firstInputNumberUnits.Count != secondInputNumberUnits.Count)
                return null; // Invalid format

            if (firstInputNumberUnits.Count == 1 && secondInputNumberUnits.Count == 1) // Single number unit
            {
                coordinates.X = new Coordinate(firstInputNumberUnits[0]);
                coordinates.Y = new Coordinate(secondInputNumberUnits[0]);
            }
            else if (IsDegreesMinutes(firstInputNumberUnits) && IsDegreesMinutes(secondInputNumberUnits))
            {
                var degrees = firstInputNumberUnits[0];
                var minutes = firstInputNumberUnits[1];
                const int seconds = 0;

                coordinates.X = new Coordinate(degrees, minutes, seconds);
            
                 degrees = secondInputNumberUnits[0];
                 minutes = secondInputNumberUnits[1];

                coordinates.Y = new Coordinate(degrees, minutes, seconds);
            }
            else if (IsDegreesMinutesSeconds(firstInputNumberUnits) && IsDegreesMinutesSeconds(secondInputNumberUnits))
            {
                var degrees = firstInputNumberUnits[0];
                var minutes = firstInputNumberUnits[1];
                var seconds = firstInputNumberUnits[2];

                coordinates.X = new Coordinate(degrees, minutes, seconds);

                degrees = secondInputNumberUnits[0];
                minutes = secondInputNumberUnits[1];
                seconds = secondInputNumberUnits[2];

                coordinates.Y = new Coordinate(degrees, minutes, seconds);
            }
            
            return coordinates;
        }

        public static bool IsDecimalDegrees(List<double> numberUnits)
        {
            return numberUnits.Count == 1;
        }

        public static bool IsDegreesMinutes(List<double> numberUnits)
        {
            return numberUnits.Count == 2;

            // TODO: Ensure that 1. numberunit is a whole number
            // TODO: Ensure that 2. numberunit <= 60
        }

        public static bool IsDegreesMinutesSeconds(List<double> numberUnits)
        {
            return numberUnits.Count == 3;

            // TODO: Ensure that 1. and 2. numberunit is a whole number
            // TODO: Ensure that 2. and 3. numberunit <= 60
        }

        public static List<double> GetNumberUnits(string input)
        {
            input = input.Replace(",", "."); // Do as part of regex?

            const string findNumberUnitsPattern = "(\\-?\\d*\\.?)\\d+";

            var numberUnits = new List<double>();

            foreach (Match match in Regex.Matches(input, findNumberUnitsPattern))
            {
                var numberUnit = double.Parse(match.Value, CultureInfo.InvariantCulture);
                numberUnits.Add(numberUnit);
            }

            return numberUnits;
        }
    }
}
