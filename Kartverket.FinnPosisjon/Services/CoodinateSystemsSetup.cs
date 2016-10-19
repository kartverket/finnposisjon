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
                            MinX = 229614.1053,
                            MaxX = 751898.5673,
                            MinY = 6401682.026,
                            MaxY = 7231445.376
                        },
                        // UTM zone 33
                        new BoundaryBox
                        {
                            MinX = 288889.7639,
                            MaxX = 804809.936,
                            MinY = 7231445.376,
                            MaxY = 7866186.306
                        },
                        // UTM zone 35
                        new BoundaryBox
                        {
                            MinX = 253177.3653,
                            MaxX = 683621.7167,
                            MinY = 7866186.306,
                            MaxY = 7924929.221
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
                            MinX = 3.844925191,
                            MaxX = 31.95907717,
                            MinY = 57.69458922,
                            MaxY = 71.45477563
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
                            MinX = -368207.9294,
                            MaxX = 172305.8,
                            MinY = -28995.15926,
                            MaxY = 808453.3338
                        },
                        // Axis 5-8
                        new BoundaryBox
                        {
                            MinX = -312424.3471,
                            MaxX = 410629.5171,
                            MinY = 1507978.752,
                            MaxY = 808453.3338
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
                            MinX = -13231.52378,
                            MaxX = 13557.59229,
                            MinY = -11742.49708,
                            MaxY = 25100.80578
                        }
                    }
                }
            };
        }
    }
}
