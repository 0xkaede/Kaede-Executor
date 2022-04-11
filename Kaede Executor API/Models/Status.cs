using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kaede_Executor_API.Models
{
    public class Status
    {
        public class EveryStatus
        {
            [JsonProperty("zip")]
            public string KEzip { get; set; }

            [JsonProperty("version")]
            public string version { get; set; }

            [JsonProperty("Scripts")]
            public List<Scripts> Scripts { get; set; }
        }

        public class Scripts
        {
            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Developer")]
            public string Developerame { get; set; }

            [JsonProperty("Code")]
            public string Code { get; set; }
        }
    }
}
