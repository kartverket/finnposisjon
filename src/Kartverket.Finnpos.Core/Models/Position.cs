﻿namespace Kartverket.Finnpos.Core.Models;

public class Position
{
    public char Identifier { get; set; }
    public Coordinates Coordinates { get; set; }
    public CoordinateSystem CoordinateSystem { get; set; }
    public Coordinates ReferenceCoordinates { get; set; }
    public AddressData AddressData { get; set; }
}
