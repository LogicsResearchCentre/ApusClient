using Newtonsoft.Json;

namespace Apus
{
    public class ClassifierDataPublic
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
