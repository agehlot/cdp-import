using System;
using BatchRetry.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace BatchRetry
{
    public static class BatchRetryTrigger
    {
        private static string BaseUrl = "https://api-us.boxever.com";
        private static string ClientId = "sndbxus06p9cxhoqoiowkr1sbq5casz3";
        private static string ClientSecret = "1kQUasq3Xl7XKeL7Jlm9Vf7kXSMGDvWh";
        [FunctionName("BatchRetryTrigger")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            var errorBatches = DbContextHelper.GetErrorCDPBatches();
            if (errorBatches != null)
            {
                foreach (var errorBatch in errorBatches)
                {
                    HttpClientHelper.GetErrorResponse(ClientId, ClientSecret, errorBatch.Log);
                }
            }
        }
    }
}
