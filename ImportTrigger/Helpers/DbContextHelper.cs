using EntityFrameworkCore;
using EntityFrameworkCore.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTrigger.Helpers
{
    public static class DbContextHelper
    {
        public static void CreateBatch(string batchID)
        {
            try
            {
                var dbContext = new BatchImportContext();
                var items = dbContext.BatchImport;
                var batchImportEFModel = new BatchImportEFModel()
                {
                    Batch_Id = batchID,
                    Executed = false
                };
                dbContext.BatchImport.Add(batchImportEFModel);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
