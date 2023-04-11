using ImportTrigger.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ImportTrigger.Helpers
{
    public static class HttpClientHelper
    {
        public static string GenerateBatchUrl(string baseurl, string clientid, string clientsecret, string guid, string checksum, long size)
        {
            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(baseurl)
                };
                var authenticationString = $"{clientid}:{clientsecret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                string path = Constants.APIConstants.BatchUrl;
                string apiUrl = $"{path}/{guid}";

                var batchModel = new GenerateBatchUrlRequestModel
                {
                    checksum = checksum,
                    size = size
                };

                var content = new StringContent(JsonConvert.SerializeObject(batchModel), Encoding.UTF8, "application/json");
                var response = client.PutAsync(apiUrl, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var batchResponse = JsonConvert.DeserializeObject<GenerateBatchUrlResponseModel>(response.Content.ReadAsStringAsync().Result);
                    return batchResponse.location.href;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
        public static void UploadBatchFileUrl(string baseurl, byte[] byteString, string currentDirectory,string batchID)
        {
            try
            {
                var path = Path.Combine(currentDirectory, "Batches", batchID);
                byte[] byteContent = File.ReadAllBytes(string.Concat(path, ".gz"));

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, baseurl);
                request.Headers.Add("x-amz-server-side-encryption", "AES256");
                var byteArrayContent = new ByteArrayContent(byteContent);
                byteArrayContent.Headers.ContentMD5 = byteString;
                request.Content = byteArrayContent;
                var response = client.SendAsync(request).Result;
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
