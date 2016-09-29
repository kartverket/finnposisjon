using System.Collections.Generic;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public static class CoodinateSystemsSetup
    {
        public static List<CoordinateSystem> Get()
        {
            return new List<CoordinateSystem>
            {
                new CoordinateSystem
                {
                    Name = "EUREF89",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        // UTM zone 32
                        new BoundaryBox
                        {
                            MinEast = 229614.1053,
                            MaxEast = 751898.5673,
                            MinNorth = 6401682.026,
                            MaxNorth = 7231445.376
                        },
                        // UTM zone 33
                        new BoundaryBox
                        {
                            MinEast = 288889.7639,
                            MaxEast = 804809.936,
                            MinNorth = 7231445.376,
                            MaxNorth = 7866186.306
                        },
                        // UTM zone 35
                        new BoundaryBox
                        {
                            MinEast = 253177.3653,
                            MaxEast = 683621.7167,
                            MinNorth = 7866186.306,
                            MaxNorth = 7924929.221
                        }
                    }
                },
                new CoordinateSystem
                {
                    Name = "WGS84",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        new BoundaryBox
                        {
                            MinEast = 3.844925191,
                            MaxEast = 31.95907717,
                            MinNorth = 57.69458922,
                            MaxNorth = 71.45477563
                        }
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        // Axis 1-4
                        new BoundaryBox
                        {
                            MinEast = -368207.9294,
                            MaxEast = 172305.8,
                            MinNorth = -28995.15926,
                            MaxNorth = 808453.3338
                        },
                        // Axis 5-8
                        new BoundaryBox
                        {
                            MinEast = -312424.3471,
                            MaxEast = 410629.5171,
                            MinNorth = 1507978.752,
                            MaxNorth = 808453.3338
                        }
                    }
                },
                new CoordinateSystem
                {
                    Name = "Oslo kommune",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        new BoundaryBox
                        {
                            MinEast = -13231.52378,
                            MaxEast = 13557.59229,
                            MinNorth = -11742.49708,
                            MaxNorth = 25100.80578
                        }
                    }
                }
            };
        }
    }
}
