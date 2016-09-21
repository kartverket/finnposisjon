using System.Web;
using System.Web.Mvc;

namespace Kartverket.FinnPosisjon
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
