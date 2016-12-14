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

            RemoveRedundantPositions(positions);

            foreach (var position in positions)
                position.ReferenceCoordinates = CoordinateTransformer.Transform(
                    position.Coordinates.X.DecimalValue, position.Coordinates.Y.DecimalValue,
                    position.CoordinateSystem.SosiCode
                );

            positions.RemoveAll(p => p.ReferenceCoordinates == null);

            foreach (var position in positions)
                AddressDataProvider.FetchAndSet(position);

            positions.RemoveAll(p => p.AddressData == null);

            var orderedPositions = positions.OrderBy(p => p.AddressData.DistanceFromPosition);

            var id = 'A';
            foreach (var position in orderedPositions)
                if(id <= 90) position.Identifier = id ++;

            return orderedPositions.ToList();
        }

        public static void RemoveRedundantPositions(List<Position> positions)
        {
            var groupsOfSimilarPositions = positions.GroupBy(p => new
            {
                p.CoordinateSystem.SosiCode,
                X = p.Coordinates.X.DecimalValue,
                Y = p.Coordinates.Y.DecimalValue
            }, (key, group) => new {Positions = group});

            foreach (var groupOfSimilarPositions in groupsOfSimilarPositions)
            {
                var redundantPositions = // All but the one with the smallest coordinatesystem boundary box.
                    groupOfSimilarPositions.Positions.OrderBy(p => p.CoordinateSystem.BoundaryBox.GetArea()).Skip(1);

                foreach (var redundantPosition in redundantPositions)
                    positions.Remove(redundantPosition);
            }
        }
    }
}
