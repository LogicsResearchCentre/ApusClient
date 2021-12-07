using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apus
{
    public class InvoiceData
    {
        [JsonProperty("insertDate")]
        public string InsertDate { get; set; }
        [JsonProperty("insertUser")]
        public string InsertUser { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("responsibleOrganization")]
        public OrganizationData ResponsibleOrganization { get; set; }
        [JsonProperty("status")]
        public ClassifierDataPublic Status { get; set; }
        [JsonProperty("type")]
        public ClassifierDataPublic Type { get; set; }
        [JsonProperty("updateDate")]
        public string UpdateDate { get; set; }
        [JsonProperty("updateUser")]
        public string UpdateUser { get; set; }
    }
}
