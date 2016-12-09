using System.Collections.Generic;

namespace Kartverket.FinnPosisjon.Models
{
    public class PositionsResult
    {
        public List<Position> Positions { get; set; }
        public bool Comprehensive { get; set; }
        public bool ParseError { get; set; }
    }
}
