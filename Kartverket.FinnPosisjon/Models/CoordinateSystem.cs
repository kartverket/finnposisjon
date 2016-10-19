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
                (coordinates.X < boundaryBox.MinX) || (coordinates.X > boundaryBox.MaxX) ||
                (coordinates.Y < boundaryBox.MinY) || (coordinates.Y > boundaryBox.MaxY));
        }
    }
}
