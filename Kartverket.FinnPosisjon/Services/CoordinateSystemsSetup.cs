using System.Collections.Generic;
using Kartverket.FinnPosisjon.Models;

namespace Kartverket.FinnPosisjon.Services
{
    public static class CoordinateSystemsSetup
    {
        public static List<CoordinateSystem> Find(int sosiCode)
        {
            return new List<CoordinateSystem> {Get().Find(c => c.SosiCode == sosiCode)};
        }

        public static List<CoordinateSystem> Get()
        {
            return new List<CoordinateSystem>
            {
                new CoordinateSystem
                {
                    Name = "EUREF89, UTM-sone 32",
                    SosiCode = 22,
                    BoundaryBox = new BoundaryBox
                    {
                        // UTM zone 32
                        MinX = 229614.1053,
                        MaxX = 751898.5673,
                        MinY = 6401682.026, // Norway
                        MaxY = 7231445.376 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "EUREF89, UTM-sone 33",
                    SosiCode = 23,
                    BoundaryBox = new BoundaryBox
                    {
                        // UTM zone 33
                        MinX = 288889.7639,
                        MaxX = 804809.936,
                        MinY = 7211211.98, // Norway
                        MaxY = 7866186.306 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "EUREF89, UTM-sone 34",
                    SosiCode = 24,
                    BoundaryBox = new BoundaryBox
                    {
                        // UTM zone 34
                        MinX = 389363.4613,
                        MaxX = 624301.8048,
                        MinY = 7565200.998, // Norway
                        MaxY = 7930309.032 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "EUREF89, UTM-sone 35",
                    SosiCode = 25,
                    BoundaryBox = new BoundaryBox
                    {
                        // UTM zone 35
                        MinX = 253177.3653,
                        MaxX = 683621.7167,
                        MinY = 7603094.00, // Norway
                        MaxY = 7924929.221 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "EUREF89, UTM-sone 33 for hele Norge",
                    SosiCode = 23,
                    BoundaryBox = new BoundaryBox
                    {
                        // UTM zone 33 for all of Norway
                        MinX = -128551.4542,
                        MaxX = 1148218.099,
                        MinY = 6404024.705, // Norway
                        MaxY = 8010780.591 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "WGS84",
                    SosiCode = 84,
                    BoundaryBox = new BoundaryBox
                    {
                        // WGS84
                        MinX = 3.844925191,
                        MaxX = 31.95907717,
                        MinY = 57.69458922, // Norway
                        MaxY = 71.45477563 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 1",
                    SosiCode = 1,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 1-4
                        MinX = -368207.9294,
                        MaxX = 172305.8,
                        MinY = -28995.15926, // Norway
                        MaxY = 808453.3338 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 2",
                    SosiCode = 2,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 1-4
                        MinX = -368207.9294,
                        MaxX = 172305.8,
                        MinY = -28995.15926, // Norway
                        MaxY = 808453.3338 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 3",
                    SosiCode = 3,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 1-4
                        MinX = -368207.9294,
                        MaxX = 172305.8,
                        MinY = -28995.15926, // Norway
                        MaxY = 808453.3338 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 4",
                    SosiCode = 4,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 1-4
                        MinX = -368207.9294,
                        MaxX = 172305.8,
                        MinY = -28995.15926, // Norway
                        MaxY = 808453.3338 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 5",
                    SosiCode = 5,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 5-8
                        MinX = -312424.3471,
                        MaxX = 410629.5171,
                        MinY = 808453.3338, // Norway
                        MaxY = 1507978.752 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 6",
                    SosiCode = 6,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 5-8
                        MinX = -312424.3471,
                        MaxX = 410629.5171,
                        MinY = 808453.3338, // Norway
                        MaxY = 1507978.752 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 7",
                    SosiCode = 7,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 5-8
                        MinX = -312424.3471,
                        MaxX = 410629.5171,
                        MinY = 808453.3338, // Norway
                        MaxY = 1507978.752 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "NGO1948, Akse 8",
                    SosiCode = 8,
                    BoundaryBox = new BoundaryBox
                    {
                        // NGO1948 Axis 5-8
                        MinX = -312424.3471,
                        MaxX = 410629.5171,
                        MinY = 808453.3338, // Norway
                        MaxY = 1507978.752 // Norway
                    }
                },
                new CoordinateSystem
                {
                    Name = "Oslo kommune",
                    SosiCode = 101,
                    BoundaryBox = new BoundaryBox
                    {
                        MinX = -13231.52378,
                        MaxX = 13557.59229,
                        MinY = -11742.49708,
                        MaxY = 25100.80578
                    }
                }
            };
        }
    }
}
