using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apus
{
    public class LicenseData
    {
        [JsonProperty("regNumber")]
        public string RegNumber { get; set; }
        [JsonProperty("validFrom")]
        public string ValidFrom { get; set; }
        [JsonProperty("validTo")]
        public string ValidTo { get; set; }
    }
}
