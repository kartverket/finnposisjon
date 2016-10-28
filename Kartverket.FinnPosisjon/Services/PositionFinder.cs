using System;
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
                position.ReferenceCoordinates = CoordinateTransformer.Transform(position.Coordinates,
                    position.CoordinateSystem.SosiCode, 84);

            positions.RemoveAll(p => p.ReferenceCoordinates == null);

            foreach (var position in positions)
                AddressDataProvider.FetchAndSet(position);

            positions.RemoveAll(p => p.AddressData == null);

            var orderedPositions = positions.OrderBy(p => p.AddressData.DistanceFromPosition);

            var id = 'A';
            foreach (var position in orderedPositions)
            {
                position.Identifier = id;
                id = (char) (Convert.ToUInt16(id) + 1);
            }

            return positions;
        }
    }
}
