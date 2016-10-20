namespace Kartverket.FinnPosisjon.Models
{
    public class CoordinateSystem
    {
        public string Name { get; set; }
        public int SosiCode { get; set; }

        public BoundaryBox BoundaryBox { get; set; }

        public bool IsOutOfBounds(Coordinates coordinates)
        {
            return (coordinates.X < BoundaryBox.MinX) || (coordinates.X > BoundaryBox.MaxX)
                   || (coordinates.Y < BoundaryBox.MinY) || (coordinates.Y > BoundaryBox.MaxY);
        }
    }
}
