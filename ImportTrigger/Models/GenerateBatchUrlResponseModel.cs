using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTrigger.Models
{
    public class GenerateBatchUrlResponseModel
    {
        public string href { get; set; }
        public string checksum { get; set; }
        public long size { get; set; }
        public GenerateBatchUrlLocationModel location { get; set; }
    }
}
