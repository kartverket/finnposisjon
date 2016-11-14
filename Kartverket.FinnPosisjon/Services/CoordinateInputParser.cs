using System.Collections.Generic;
using System.Globalization;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateInputParser
    {
        public static List<Coordinates> GetCoordinates(string firstInput, string secondInput, bool comprehensive = false)
        {
            double firstCoordinate;
            double secondCoordinate;

            var coordinates = new List<Coordinates>();

            firstInput = firstInput.Replace(",", ".");
            secondInput = secondInput.Replace(",", ".");

            if (!double.TryParse(firstInput, NumberStyles.Any, CultureInfo.InvariantCulture, out firstCoordinate) ||
                !double.TryParse(secondInput, NumberStyles.Any, CultureInfo.InvariantCulture, out secondCoordinate))
                return coordinates;

            coordinates.AddRange(new[]
            {
                new Coordinates
                {
                    // Normal order
                    X = firstCoordinate,
                    Y = secondCoordinate
                },
                new Coordinates
                {
                    // Swapped order
                    X = secondCoordinate,
                    Y = firstCoordinate
                }
            });

            if (comprehensive)
                coordinates.AddRange(new[]
                {
                    new Coordinates
                    {
                        // Normal order, inverted X
                        X = 0 - firstCoordinate,
                        Y = secondCoordinate
                    },
                    new Coordinates
                    {
                        // Normal order, inverted Y
                        X = firstCoordinate,
                        Y = 0 - secondCoordinate
                    },
                    new Coordinates
                    {
                        // Swapped order, inverted X
                        X = 0 - secondCoordinate,
                        Y = firstCoordinate
                    },
                    new Coordinates
                    {
                        // Swapped order, inverted Y
                        X = secondCoordinate,
                        Y = 0 - firstCoordinate
                    }
                });

            return coordinates;
        }

        public static List<Coordinates> GetCoordinates(string firstInput, string secondInput, string thirdInput)
        {
            double firstCoordinate;
            double secondCoordinate;
            double thirdCoordinate;

            var coordinates = new List<Coordinates>();

            if (double.TryParse(firstInput.Replace(".", ","), out firstCoordinate) &&
                double.TryParse(secondInput.Replace(".", ","), out secondCoordinate) &&
                double.TryParse(thirdInput.Replace(".", ","), out thirdCoordinate))
                coordinates.AddRange(new[]
                {
                    new Coordinates
                    {
                        // Normal order
                        X = firstCoordinate,
                        Y = secondCoordinate,
                        Z = thirdCoordinate
                    }
                });

            return coordinates;
        }
    }
}
