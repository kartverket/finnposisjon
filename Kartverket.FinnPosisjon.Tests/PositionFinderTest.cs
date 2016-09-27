using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;
using Xunit;

namespace Kartverket.FinnPosisjon.Tests
{
    public class PositionFinderTest
    {
        
        [Fact(Skip = "PositionFinder.Find not yet implemented")]
        public void ShouldFindPositionWithCoordSysEu89UtmZone33()
        {
            var userInputCoordinates = new[] {"163067.449", "6601114.654"};

            var positionFinder = InitPositionFinder();

            var possiblePositions = positionFinder.Find(userInputCoordinates);

            var position = possiblePositions.First();

            position.CoordinateSystem.Should().Be("EU89, UTM-sone 33");
        }

        [Fact(Skip = "Test is unfinished")]
        public void ShouldFindOnePositionWithinBounderies()
        {
            var userInputCoordinates = new[] { "45", "15" };

            var positionFinder = InitPositionFinder();

            var possiblePositions = positionFinder.Find(userInputCoordinates);

            possiblePositions.Count.Should().Be(1);
        }

        private static PositionFinder InitPositionFinder()
        {
            return new PositionFinder
            {
                SupportedCoordinateSystems = new List<CoordinateSystem>
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
                }
            };
        }
    }
}