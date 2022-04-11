using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kaede_Executor_API.Models.DataBase
{
    public class EveryData
    {
        [JsonProperty("AdminAccess")]
        public bool AdminAccess { get; set; }

        [JsonProperty("AdminAccessKey")]
        public string AdminAccessKey { get; set; }

        [JsonProperty("Keys")]
        public List<DataBase> Keys { get; set; }
    }

    public class DataBase
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("HWID")]
        public string HWID { get; set; }
    }
}
