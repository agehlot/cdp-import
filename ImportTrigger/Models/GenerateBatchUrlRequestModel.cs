using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTrigger.Models
{
    public class GenerateBatchUrlRequestModel
    {
        public string checksum { get; set; }
        public long size { get; set; }
    }
}
