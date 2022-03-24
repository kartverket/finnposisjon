namespace Kartverket.Finnpos.Core.Models;

public class CoordinateSystem
{
    public string Name { get; set; }
    public int SosiCode { get; set; }
    public int EpsgCode { get; set; }

    public BoundaryBox BoundaryBox { get; set; }

    public bool IsOutOfBounds(Coordinates coordinates)
    {
        return coordinates.X.DecimalValue < BoundaryBox.MinX || coordinates.X.DecimalValue > BoundaryBox.MaxX ||
               coordinates.Y.DecimalValue < BoundaryBox.MinY || coordinates.Y.DecimalValue > BoundaryBox.MaxY;
    }
}
