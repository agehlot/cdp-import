using System;
using System.IO;
using System.Linq;
using BatchUpdateStatus.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace BatchUpdateStatus
{
    public static class BatchUpdateStatus
    {
        private static string BaseUrl = "https://api-us.boxever.com";
        private static string ClientId = "sndbxus06p9cxhoqoiowkr1sbq5casz3";
        private static string ClientSecret = "1kQUasq3Xl7XKeL7Jlm9Vf7kXSMGDvWh";
        [FunctionName("BatchUpdateStatus")]
        public static void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            var currentDirectory = context.FunctionAppDirectory;
            var batchErrorDirPath = Path.Combine(currentDirectory, "ErrorBatches");
            if (!Directory.Exists(batchErrorDirPath))
            {
                Directory.CreateDirectory(batchErrorDirPath);
            }

            var pendingBatches = DbContextHelper.GetPendingCDPBatches();
            if (pendingBatches != null && pendingBatches.Any())
            {
                foreach (var pendingBatch in pendingBatches)
                {
                    HttpClientHelper.GetBatchStatus(BaseUrl, ClientId, ClientSecret, pendingBatch, currentDirectory);
                }
            }
        }
    }
}
