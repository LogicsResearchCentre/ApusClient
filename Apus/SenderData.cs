using Newtonsoft.Json;

namespace Apus
{
    public class SenderData
    {
        [JsonProperty("facility")]
        public FacilityPublic Facility { get; set; }

        [JsonProperty("legalType")]
        public string LegalType { get; set; }

        [JsonProperty("organization")]
        public OrganizationData Organization { get; set; }

        [JsonProperty("person")]
        public PersonData Person { get; set; }

        [JsonProperty("personResponsibleForWasteShipmentName")]
        public string PersonResponsibleForWasteShipmentName { get; set; }

        [JsonProperty("personResponsibleForWasteShipmentPhone")]
        public string PersonResponsibleForWasteShipmentPhone { get; set; }

        [JsonProperty("shipmentDate")]
        public string ShipmentDate { get; set; }
    }
}
