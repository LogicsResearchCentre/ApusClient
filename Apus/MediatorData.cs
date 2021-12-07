using Newtonsoft.Json;

namespace Apus
{
    public class MediatorData
    {
        [JsonProperty("organization")]
        public OrganizationData Organization { get; set; }

        [JsonProperty("personResponsibleForWasteMediationName")]
        public string PersonResponsibleForWasteMediationName { get; set; }

        [JsonProperty("personResponsibleForWasteMediationPhone")]
        public string PersonResponsibleForWasteMediationPhone { get; set; }

        [JsonProperty("vvdDecisionDate")]
        public string VvdDecisionDate { get; set; }

        [JsonProperty("vvdDecisionNumber")]
        public string vvdDecisionNumber { get; set; }
    }
}
