using System.IO;
using System.Net;
using System.Text;

namespace Kartverket.FinnPosisjon.Services
{
    public class WebServiceCaller
    {
        public static string GetJsonWebServiceResponse(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);

            try
            {
                var response = request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var errorResponse = ex.Response;

                using (var responseStream = errorResponse.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    var errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
