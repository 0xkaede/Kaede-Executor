using Newtonsoft.Json;

namespace KaedeInstaller
{
    public class Api
    {
        [JsonProperty("zip")]
        public string KEzip { get; set; }

        [JsonProperty("version")]
        public string version { get; set; }
    }
}
