using Newtonsoft.Json;

namespace Apus
{
    public class InvoicePublicCoordination
    {
        [JsonProperty("isCoordinated")]
        public bool IsCoordinated { get; set; }

        [JsonProperty("organizationId")]
        public ulong OrganizationId { get; set; }

        [JsonProperty("personCode")]
        public string PersonCode { get; set; }
    }
}
