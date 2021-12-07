using Newtonsoft.Json;

namespace Apus
{
    public class CarrierData
    {
        [JsonProperty("cargoInvoiceNumber")]
        public string CargoInvoiceNumber { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("license")]
        public LicenseData License { get; set; }

        [JsonProperty("organization")]
        public OrganizationData Organization { get; set; }

        [JsonProperty("personResponsibleForWasteTransportingName")]
        public string PersonResponsibleForWasteTransportingName { get; set; }

        [JsonProperty("personResponsibleForWasteTransportingPhone")]
        public string PersonResponsibleForWasteTransportingPhone { get; set; }

        [JsonProperty("trailerRegistrationNumber")]
        public string TrailerRegistrationNumber { get; set; }

        [JsonProperty("typeOfShipment")]
        public ClassifierDataPublic TypeOfShipment { get; set; }

        [JsonProperty("typeOfTransport")]
        public ClassifierDataPublic TypeOfTransport { get; set; }

        [JsonProperty("vehicleRegistrationNumber")]
        public string VehicleRegistrationNumber { get; set; }
    }
}
