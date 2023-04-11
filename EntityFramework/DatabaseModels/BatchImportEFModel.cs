namespace EntityFrameworkCore.DatabaseModels
{
    public class BatchImportEFModel
    {
        public string Batch_Id { get; set; }
        public bool Executed { get; set; }
        public string Status { get; set; }
        public string Log { get; set; }
    }
}
