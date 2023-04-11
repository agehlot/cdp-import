using BatchRetry.Models;
using EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchRetry.Helpers
{
    public static class DbContextHelper
    {
        public static List<BatchError> GetErrorCDPBatches()
        {
            try
            {
                List<BatchError> errorBatches = new List<BatchError>();
                using (var dbContext = new BatchImportContext())
                {
                    var errorBatchRecords = dbContext.BatchImport.Where(x => x.Executed == true && x.Status == "error");
                    if (errorBatchRecords != null)
                    {
                        foreach (var errorBatchRecord in errorBatchRecords)
                        {
                            var errorBatch = new BatchError()
                            {
                                Batch_Id = errorBatchRecord.Batch_Id,
                                Log = errorBatchRecord.Log
                            };
                            errorBatches.Add(errorBatch);
                        }
                    }
                    return errorBatches;
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