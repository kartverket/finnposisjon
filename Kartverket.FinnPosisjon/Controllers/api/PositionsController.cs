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
            var positionFinder = new PositionFinder {SupportedCoordinateSystems = CoodinateSystemsSetup.Get()};

            var positionsResult = positionFinder.Find(firstInputValue, secondInputValue, thirdInputValue);

            return Json(positionsResult);
        }
    }
}
