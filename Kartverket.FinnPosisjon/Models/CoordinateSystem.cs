using System.Collections.Generic;

namespace Kartverket.FinnPosisjon.Models
{
    public class CoordinateSystem
    {
        public string Name { get; set; }

        public List<BoundaryBox> BoundaryBoxes { get; set; }

        public bool IsOutOfBounds(Coordinates coordinates)
        {
            return BoundaryBoxes.TrueForAll(boundaryBox =>
                (coordinates.East < boundaryBox.MinEast) || (coordinates.East > boundaryBox.MaxEast) ||
                (coordinates.North < boundaryBox.MinNorth) || (coordinates.North > boundaryBox.MaxNorth));
        }
    }
}
