using System.Collections.Generic;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class CoordinateTweaker
    {
        public static List<Coordinates> AddSwapped(Coordinates coordinates)
        {
            return new List<Coordinates>
            {
                // Normal order
                coordinates,
                // Swapped order
                Swap(coordinates)
            };
        }

        public static List<Coordinates> AddSwappedAndInverted(Coordinates coordinates)
        {
            return new List<Coordinates>
            {
                // Normal order
                coordinates,
                // Swapped order
                Swap(coordinates),
                // Normal order, inverted X
                InvertX(coordinates),
                // Normal order, inverted Y
                InvertY(coordinates),
                // Swapped order, inverted X
                Swap(InvertX(coordinates)),
                // Swapped order, inverted Y
                Swap(InvertY(coordinates))
            };
        }

        private static Coordinates Swap(Coordinates coordinates)
        {
            return new Coordinates
            {
                X = coordinates.Y,
                Y = coordinates.X
            };
        }

        private static Coordinates InvertX(Coordinates coordinates)
        {
            return new Coordinates
            {
                X = new Coordinate(0 - coordinates.X.DecimalValue),
                Y = coordinates.Y
            };
        }

        private static Coordinates InvertY(Coordinates coordinates)
        {
            return new Coordinates
            {
                X = coordinates.X,
                Y = new Coordinate(0 - coordinates.Y.DecimalValue)
            };
        }
    }
}