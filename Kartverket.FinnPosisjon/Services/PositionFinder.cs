using System.Collections.Generic;
using System.Linq;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public class PositionFinder
    {
        public List<CoordinateSystem> SupportedCoordinateSystems { get; set; }

        public List<Position> Find(List<Coordinates> coordinatesCollection)
        {
            var positions = (from coordinates in coordinatesCollection
                                      from supportedCoordinateSystem in SupportedCoordinateSystems
                where !supportedCoordinateSystem.IsOutOfBounds(coordinates)
                select new Position
                    {Coordinates = coordinates, CoordinateSystem = supportedCoordinateSystem}).ToList();

            // Return an empty result if no coordinates were within the bounds for any of the coordinate systems.
            if (positions.Count == 0) return positions;

            // TODO: Transform candidate position coordinates to WGS84

            // TODO: Look up adresses for positions

            return positions;
        }
    }
}
