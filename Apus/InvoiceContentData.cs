using Newtonsoft.Json;

namespace Apus
{
    public class InvoiceContentData
    {
        [JsonProperty("amountReceived")]
        public decimal AmountReceived { get; set; }

        [JsonProperty("amountSend")]
        public decimal? AmountSend {get; set;}

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("insertDate")]
        public string InsertDate { get; set; }

        [JsonProperty("number")]
        public long? Number { get; set; }

        [JsonProperty("target")]
        public ClassifierDataPublic Target { get; set; }

        [JsonProperty("updateDate")]
        public string UpdateDate { get; set; }

        [JsonProperty("wasteClass")]
        public ClassifierDataPublic WasteClass { get; set; }
    }
}
