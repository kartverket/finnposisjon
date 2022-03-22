using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kartverket.Finnpos.Core.Models;
using Kartverket.Finnpos.Core.Services;
using Xunit;

namespace Kartverket.Finnpos.Core.Tests.Services;

public class PositionFinderTests
{
    private static readonly PositionFinder PositionFinder = new PositionFinder
    {
        SupportedCoordinateSystems = CoordinateSystemsSetup.Get()
    };

    [Fact]
    public void ShouldFindPositionWithCoordSysEu89UtmZone33()
    {
        var coordinates = new Coordinates { X = new Coordinate(288889.7639), Y = new Coordinate(7231445.376) };

        var positions = PositionFinder.Find(new List<Coordinates> { coordinates });

        positions.Any(p => p.CoordinateSystem.Name == "EUREF89, UTM-sone 33").Should().BeTrue();
    }

    /*
     * If more than one position has the same coordinatesystem (sosi-code) and the same coordinates,
     * only the position with the smallest coordinatesystem boundarybox is kept.
    */

    [Fact]
    public void ShouldRemoveRedundantPositions()
    {
        var position1 = new Position // Should be kept
        {
            Coordinates = new Coordinates { X = new Coordinate(70), Y = new Coordinate(80) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System A - natural boundaries",
                    SosiCode = 1000,
                    BoundaryBox = new BoundaryBox { MinX = 50, MaxX = 100, MinY = 50, MaxY = 100 }
                }
        };
        var position2 = new Position // Should be excluded
        {
            Coordinates = new Coordinates { X = new Coordinate(70), Y = new Coordinate(80) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System A - extended boundaries",
                    SosiCode = 1000,
                    BoundaryBox = new BoundaryBox { MinX = 0, MaxX = 150, MinY = 0, MaxY = 150 }
                }
        };
        var position3 = new Position // Should be kept
        {
            Coordinates = new Coordinates { X = new Coordinate(80), Y = new Coordinate(70) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System A - natural boundaries",
                    SosiCode = 1000,
                    BoundaryBox = new BoundaryBox { MinX = 50, MaxX = 100, MinY = 50, MaxY = 100 }
                }
        };
        var position4 = new Position // Should be excluded
        {
            Coordinates = new Coordinates { X = new Coordinate(80), Y = new Coordinate(70) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System A - extended boundaries",
                    SosiCode = 1000,
                    BoundaryBox = new BoundaryBox { MinX = 0, MaxX = 150, MinY = 0, MaxY = 150 }
                }
        };
        var position5 = new Position // Should be kept
        {
            Coordinates = new Coordinates { X = new Coordinate(70), Y = new Coordinate(80) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System B - natural boundaries",
                    SosiCode = 1001,
                    BoundaryBox = new BoundaryBox { MinX = 50, MaxX = 100, MinY = 50, MaxY = 100 }
                }
        };
        var position6 = new Position // Should be excluded
        {
            Coordinates = new Coordinates { X = new Coordinate(70), Y = new Coordinate(80) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System B - extended boundaries",
                    SosiCode = 1001,
                    BoundaryBox = new BoundaryBox { MinX = 0, MaxX = 150, MinY = 0, MaxY = 150 }
                }
        };
        var position7 = new Position // Should be kept
        {
            Coordinates = new Coordinates { X = new Coordinate(80), Y = new Coordinate(70) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System B - natural boundaries",
                    SosiCode = 1001,
                    BoundaryBox = new BoundaryBox { MinX = 50, MaxX = 100, MinY = 50, MaxY = 100 }
                }
        };
        var position8 = new Position // Should be excluded
        {
            Coordinates = new Coordinates { X = new Coordinate(80), Y = new Coordinate(70) },
            CoordinateSystem =
                new CoordinateSystem
                {
                    Name = "System B - extended boundaries",
                    SosiCode = 1001,
                    BoundaryBox = new BoundaryBox { MinX = 0, MaxX = 150, MinY = 0, MaxY = 150 }
                }
        };

        var positions = new List<Position>
        {
            position1,
            position2,
            position3,
            position4,
            position5,
            position6,
            position7,
            position8
        };

        var expectedPositions = new List<Position>
        {
            position1,
            position3,
            position5,
            position7
        };

        PositionFinder.RemoveRedundantPositions(positions);

        PositionListsAreEqual(positions, expectedPositions).Should().BeTrue();
    }

    private static bool PositionListsAreEqual(List<Position> positionsA, List<Position> positionsB)
    {
        if (positionsA.Count != positionsB.Count)
            return false;

        for (var i = 0; i < positionsB.Count; i++)
            if (!PositionsAreEqual(positionsA[i], positionsB[i]))
                return false;

        return true;
    }

    private static bool PositionsAreEqual(Position positionA, Position positionB)
    {
        return positionA.CoordinateSystem.Name.Equals(positionB.CoordinateSystem.Name) &&
               positionA.CoordinateSystem.SosiCode == positionB.CoordinateSystem.SosiCode &&
               positionA.Coordinates.X.DecimalValue.Equals(positionB.Coordinates.X.DecimalValue) &&
               positionA.Coordinates.Y.DecimalValue.Equals(positionB.Coordinates.Y.DecimalValue);
    }
}
