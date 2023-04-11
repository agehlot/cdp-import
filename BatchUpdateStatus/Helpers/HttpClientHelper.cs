using BatchUpdateStatus.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BatchUpdateStatus.Helpers
{
    public static class HttpClientHelper
    {
        public static void GetBatchStatus(string baseurl, string clientid, string clientsecret, string batchguid, string currentDirectory)
        {
            try
            {
                HttpClient client = new HttpClient();
                var apibaseurl = $"{baseurl}/v2/batches/{batchguid}";
                var authenticationString = $"{clientid}:{clientsecret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apibaseurl);
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                var response = client.SendAsync(httpRequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    var batchResponse = JsonConvert.DeserializeObject<BatchStatusResponse>(response.Content.ReadAsStringAsync().Result);
                    if (batchResponse.Status.Code.ToLower() != "uploading" && batchResponse.Status.Code.ToLower() != "processing")
                    {
                        DbContextHelper.UpdateBatchStatus(batchguid, batchResponse.Status.Code, batchResponse.Status.Log);
                        if (batchResponse.Status.Log != null)
                        {
                            var errorResponse = HttpClientHelper.GetErrorResponse(batchResponse.Status.Log);
                            var bytesStream = BatchHelper.ReadFully(errorResponse);
                            BatchHelper.Decompress(currentDirectory, batchguid, bytesStream);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static Stream GetErrorResponse(string logurl)
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
                    var result = response.Content.ReadAsStreamAsync().Result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}