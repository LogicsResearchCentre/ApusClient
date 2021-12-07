using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apus
{
    public class InvoicePublic
    {
        [JsonProperty("actions")]
        public string[] Actions { get; set; }

        [JsonProperty("blocks")]
        public string[] Blocks { get; set; }

        [JsonProperty("carriers")]
        public CarrierData[] Carriers { get; set; }

        [JsonProperty("content")]
        public InvoiceContentData[] Content { get; set; }

        [JsonProperty("coordinations")]
        public InvoicePublicCoordination[] Coordinations { get; set; }

        [JsonProperty("invoice")]
        public InvoiceData Invoice { get; set; }

        [JsonProperty("mediator")]
        public MediatorData Mediator { get; set; }

        [JsonProperty("receiver")]
        public ReceiverData Receiver { get; set; }

        [JsonProperty("sender")]
        public SenderData Sender { get; set; }
    }
}
