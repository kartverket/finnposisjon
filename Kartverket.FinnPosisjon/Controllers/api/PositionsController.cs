using System.Web.Http;
using System.Web.Http.Results;

namespace Kartverket.FinnPosisjon.Controllers.api
{
    public class PositionsController : ApiController
    {
        public JsonResult<string> Get(string firstInputValue, string secondInputValue, string thirdInputValue)
        {
            return Json("Test");
        }
    }
}
