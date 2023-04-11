using EntityFrameworkCore.Configurations;
using EntityFrameworkCore.DatabaseModels;
using System.Data.Entity;

namespace EntityFrameworkCore
{
    public class BatchImportContext : DbContext
    {
        public BatchImportContext() : base("name=cdpbatch")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new BatchImportConfiguration());
        }
        public DbSet<BatchImportEFModel> BatchImport
        {
            get;
            set;
        }
    }
}
