using Newtonsoft.Json;

namespace BatchUpdateStatus.Models
{
    public class BatchStatusResponse
    {
        [JsonProperty("href")]
        public string Href;
        [JsonProperty("ref")]
        public string Ref;
        [JsonProperty("status")]
        public StatusResponse Status;

    }
    public class StatusResponse
    {
        [JsonProperty("code")]
        public string Code;
        [JsonProperty("log")]
        public string Log;
    }
}
