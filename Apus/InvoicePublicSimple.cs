using System;
using Newtonsoft.Json;

namespace Apus
{
    public class InvoicePublicSimple
    {
        [JsonProperty("amountReceived")]
        public decimal AmountReceived { get; set; }

        [JsonProperty("amountSent")]
        public decimal AmountSent { get; set; }

        [JsonProperty("carrierName")]
        public string CarrierName { get; set; }

        [JsonProperty("insertDate")]
        public DateTime InsertDate { get; set; }

        [JsonProperty("invoiceNumber")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("invoiceStatusName")]
        public string InvoiceStatusName { get; set; }

        [JsonProperty("receiverFacilityName")]
        public string ReceiverFacilityName { get; set; }

        [JsonProperty("receiverName")]
        public string ReceiverName { get; set; }

        [JsonProperty("senderFacilityName")]
        public string SenderFacilityName { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }

        [JsonProperty("shipmentDate")]
        public string ShipmentDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
