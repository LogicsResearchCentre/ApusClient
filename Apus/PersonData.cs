using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apus
{
    public class PersonData
    {
        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }
        [JsonProperty("birthPlace")]
        public string BirthPlace { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("personCode")]
        public string PersonCode { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}
