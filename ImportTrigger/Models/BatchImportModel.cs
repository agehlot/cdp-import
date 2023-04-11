using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTrigger.Models
{
    public class BatchImportModel
    {
        [JsonProperty("ref")]
        public string ReferenceId { get; set; }

        [JsonProperty("schema")]
        public string Schema { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("value")]
        public GuestModel Guest { get; set; }
    }
}
