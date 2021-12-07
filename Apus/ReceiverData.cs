using Newtonsoft.Json;

namespace Apus
{
    public class ReceiverData
    {
        [JsonProperty("facility")]
        public FacilityPublic Facility  { get; set; }

        [JsonProperty("legalType")]
        public string LegalType { get; set; }

        [JsonProperty("managementLicense")]
        public LicenseData ManagementLicense { get; set; }

        [JsonProperty("organization")]
        public OrganizationData Organization { get; set; }

        [JsonProperty("person")]
        public PersonData Person { get; set; }

        [JsonProperty("personResponsibleForWasteReceivingName")]
        public string PersonResponsibleForWasteReceivingName { get; set; }

        [JsonProperty("personResponsibleForWasteReceivingPhone")]
        public string PersonResponsibleForWasteReceivingPhone { get; set; }

        [JsonProperty("receivedDate")]
        public string ReceivedDate { get; set; }

        [JsonProperty("wastePreservation")]
        public ClassifierDataPublic wastePreservation { get; set; }

        [JsonProperty("WasteRecycling")]
        public ClassifierDataPublic WasteRecycling { get; set; }
    }
}
