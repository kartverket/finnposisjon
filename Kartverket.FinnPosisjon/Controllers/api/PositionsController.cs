using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Kartverket.FinnPosisjon.Models;
using Kartverket.FinnPosisjon.Services;

namespace Kartverket.FinnPosisjon.Controllers.api
{
    public class PositionsController : ApiController
    {
        public JsonResult<PositionsResult> Get(string firstInputValue, string secondInputValue, string thirdInputValue)
        {
            var positionFinder = InitPositionFinder();

            var positionsResult = positionFinder.Find(firstInputValue, secondInputValue, thirdInputValue);

            return Json(positionsResult);
        }

        /**
         * Temporary setup-method
         */
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
