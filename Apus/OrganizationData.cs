using Newtonsoft.Json;

namespace Apus
{
    public class OrganizationData
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("fax")]
        public string Fax { get; set; }
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("isForeign")]
        public bool IsForeign { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("registrationNumber")]
        public string RegistrationNumber { get; set; }
        [JsonProperty("organizationType")]
        public ClassifierDataPublic OrganizationType { get; set; }
    }
}
