using EntityFrameworkCore.DatabaseModels;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkCore.Configurations
{
    public class BatchImportConfiguration : EntityTypeConfiguration<BatchImportEFModel>
    {
        public BatchImportConfiguration()
        {
            ToTable("BatchImport");
            HasKey(x => x.Batch_Id);
            Property(x => x.Batch_Id);
            Property(x => x.Executed);
            Property(x => x.Status);
            Property(x => x.Log);
        }
    }
}
