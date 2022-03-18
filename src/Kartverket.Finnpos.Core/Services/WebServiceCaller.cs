using System.Net;
using System.Text;

namespace Kartverket.Finnpos.Core.Services;

public class WebServiceCaller
{
    public static string GetJsonWebServiceResponse(string url)
    {
        var request = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            var response = request.GetResponse();

            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
        catch (WebException exception)
        {
            return ""; // TODO Log error
        }
    }
}
