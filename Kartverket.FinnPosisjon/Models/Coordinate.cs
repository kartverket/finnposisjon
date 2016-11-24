namespace Kartverket.FinnPosisjon.Models
{
    public class Coordinate
    {
        public Coordinate(double decimalValue)
        {
            DecimalValue = decimalValue;
            Format = "Decimal";
        }

        public Coordinate(double degrees, double minutes, double seconds)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;

            DecimalValue = degrees + minutes/60.0 + seconds/3600.0;

            Format = seconds.Equals(0) ? "DegMin" : "DegMinSec";
        }

        public double DecimalValue { get; }
        public double Degrees { get; }
        public double Minutes { get; }
        public double Seconds { get; }
        public string Format { get; }
    }
}
