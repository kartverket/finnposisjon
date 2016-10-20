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

            foreach (var position in positions)
                position.Coordinates = CoordinateTransformer.Transform(position.Coordinates,
                    position.CoordinateSystem.SosiCode, 84);

            positions.RemoveAll(p => p.Coordinates == null);
            
            // TODO: Look up adresses for positions.

            return positions;
        }
    }
}
