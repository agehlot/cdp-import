using EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchUpdateStatus.Helpers
{
    public static class DbContextHelper
    {
        public static List<string> GetPendingCDPBatches()
        {
            try
            {
                List<string> pendingBatches = new List<string>();
                using (var dbContext = new BatchImportContext())
                {
                    var pendingBatchRecords = dbContext.BatchImport.Where(x => x.Executed == false);
                    if (pendingBatchRecords != null)
                    {
                        foreach (var pendingbatch in pendingBatchRecords)
                        {
                            pendingBatches.Add(pendingbatch.Batch_Id);
                        }
                    }
                    return pendingBatches;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public static void UpdateBatchStatus(string batchID, string status, string log)
        {
            try
            {
                using (var dbContext = new BatchImportContext())
                {
                    var batchItem = dbContext.BatchImport.Find(batchID);
                    if (batchItem != null)
                    {
                        batchItem.Executed = true;
                        batchItem.Status = status;
                        batchItem.Log = log;
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
