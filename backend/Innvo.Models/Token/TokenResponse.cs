using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Innvo.Models.Token
{
    public class TokenResponse
    {
        [JsonPropertyName("Token")]
        public string Token {get; set;} = null!;
        [JsonPropertyName("IssuedAt")]
        public DateTime IssuedAt {get; set;}
        [JsonPropertyName("Expires")]
        public DateTime Expires {get; set;}
    }
}