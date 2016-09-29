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
                    // TODO: Setup for EUREF89
                    Name = "System 1",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        },
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        },
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        }
                    }
                },
                new CoordinateSystem
                {
                    // TODO: Setup for ITRF/WGS84
                    Name = "System 2",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        },
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        },
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        }
                    }
                },
                new CoordinateSystem
                {
                    // TODO: Setup for NGO1948
                    Name = "System 3",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        },
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        }
                    }
                },
                new CoordinateSystem
                {
                    // TODO: Setup for 'Oslo kommunes lokale koordinatsystem'
                    Name = "System 4",
                    BoundaryBoxes = new List<BoundaryBox>
                    {
                        new BoundaryBox
                        {
                            MinEast = 30,
                            MaxEast = 60,
                            MinNorth = 10,
                            MaxNorth = 20
                        }
                    }
                }
            };
        }
    }
}