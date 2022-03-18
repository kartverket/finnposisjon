﻿namespace Kartverket.Finnpos.Core.Models;

public class BoundaryBox
{
    public double MinX { get; set; }
    public double MaxX { get; set; }
    public double MinY { get; set; }
    public double MaxY { get; set; }

    public double GetArea()
    {
        return (MaxX - MinX) * (MaxY - MinY);
    }
}
