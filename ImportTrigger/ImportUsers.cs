using System;
using System.IO;
using ImportTrigger.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace ImportTrigger
{
    public static class ImportUsers
    {
        private static string BaseUrl = "https://api-us.boxever.com";
        private static string ClientId = "sndbxus06p9cxhoqoiowkr1sbq5casz3";
        private static string ClientSecret = "1kQUasq3Xl7XKeL7Jlm9Vf7kXSMGDvWh";

        [FunctionName("ImportUsers")]
        public static void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                var currentDirectory = context.FunctionAppDirectory;
                //BatchImportHelper.GzipFile(currentDirectory);
                string batchID = BatchImportHelper.GetGuid().ToString();
                var batchDirPath = Path.Combine(currentDirectory, "Batches");
                if (!Directory.Exists(batchDirPath))
                {
                    Directory.CreateDirectory(batchDirPath);
                }
                CreateBatch.CreateBatchImportModel(currentDirectory, batchID);
                DbContextHelper.CreateBatch(batchID);
                var fileSize = BatchImportHelper.GetFileSize(currentDirectory, batchID);
                bool checkFileSize = BatchImportHelper.CheckFileSize(fileSize);
                if (!checkFileSize)
                {
                    log.Info($"Batch file size is greater than max file size");
                    return;
                }
                var checkSum = BatchImportHelper.GetMD5Checksum(currentDirectory, batchID);

                log.Info($"Checksum is : {checkSum}");
                log.Info($"FileSize is : {fileSize}");
                log.Info($"Guid is : {batchID}");

                var batchUrl = HttpClientHelper.GenerateBatchUrl(BaseUrl, ClientId, ClientSecret, batchID, checkSum, fileSize);
                log.Info($"Batch Url is : {batchUrl}");

                var byteString = BatchImportHelper.HexStringToBytes(checkSum);
                HttpClientHelper.UploadBatchFileUrl(batchUrl, byteString, currentDirectory, batchID);
            }
            catch (Exception ex)
            {
                log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            }

        }
    }
}
