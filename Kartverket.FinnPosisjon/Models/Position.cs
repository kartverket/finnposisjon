namespace Kartverket.FinnPosisjon.Models
{
    public class Position
    {
        public Coordinates Coordinates { get; set; }
        public CoordinateSystem CoordinateSystem { get; set; }
        public Coordinates ReferenceCoordinates { get; set; }
        public AddressData AddressData { get; set; }
    }
}
