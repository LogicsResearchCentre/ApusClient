using Newtonsoft.Json;

namespace Apus
{
    public class FacilityPublic
    {
        [JsonProperty("addressRegistryCode")]
        public string AddressRegistryCode { get; set; }

        [JsonProperty("atvkCode")]
        public string AtvkCode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullAddress")]
        public string FullAddress { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("organization")]
        public OrganizationData Organization { get; set; }

        [JsonProperty("person")]
        public PersonData Person { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("typeCode")]
        public ClassifierDataPublic TypeCode { get; set; }
    }
}
