﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apus
{
    public class AuthenticationResult
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
