using System.Net.Http;

namespace Sitecore.SingleSignOn.Bridge.API
{
    internal static class APIHelper
    {
        private static readonly HttpClient _client = new HttpClient();

        public static HttpResponseMessage Post(string URL, string data)
        {
            HttpResponseMessage response = _client.PostAsJsonAsync(URL, data).Result;
            return response;
        }
    }
}