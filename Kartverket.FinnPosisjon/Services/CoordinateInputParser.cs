using System.Collections.Generic;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateInputParser
    {
        public static List<Coordinates> GetCoordinates(string firstInputValue, string secondInputValue)
        {
            double firstCoordinate = 0;
            double secondCoordinate = 0;

            var coordinates = new List<Coordinates>();
            
            if (CoordinateOk(firstInputValue, ref firstCoordinate)
                && CoordinateOk(secondInputValue, ref secondCoordinate))
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
                    },
                    new Coordinates
                    {
                        // Normal order, negative X
                        X = 0 - firstCoordinate,
                        Y = secondCoordinate
                    },
                    new Coordinates
                    {
                        // Normal order, negative Y
                        X = firstCoordinate,
                        Y = 0 - secondCoordinate
                    },
                    new Coordinates
                    {
                        // Swapped order, negative X
                        X = 0 - secondCoordinate,
                        Y = firstCoordinate
                    },
                    new Coordinates
                    {
                        // Swapped order, negative Y
                        X = secondCoordinate,
                        Y = 0 - firstCoordinate
                    }
                });

            return coordinates;
        }

        public static List<Coordinates> GetCoordinates(string firstInputValue, string secondInputValue, string thirdInputValue)
        {
            double firstCoordinate = 0;
            double secondCoordinate = 0;
            double thirdCoordinate = 0;

            var coordinates = new List<Coordinates>();

            if (CoordinateOk(firstInputValue, ref firstCoordinate)
                && CoordinateOk(secondInputValue, ref secondCoordinate)
                && CoordinateOk(thirdInputValue, ref thirdCoordinate))
            {
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
            }
            return coordinates;
        }

        private static bool CoordinateOk(string inputValue, ref double coordinate)
        {
            return !string.IsNullOrWhiteSpace(inputValue) && double.TryParse(inputValue.Replace(".", ","), out coordinate);
        }
    }
}
