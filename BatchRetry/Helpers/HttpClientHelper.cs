using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BatchRetry.Helpers
{
    public static class HttpClientHelper
    {
        public static void GetErrorResponse(string clientid, string clientsecret, string logurl)
        {
            try
            {
                HttpClient client = new HttpClient();
                //var authenticationString = $"{clientid}:{clientsecret}";
                //var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, logurl);
                //httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                var response = client.SendAsync(httpRequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
