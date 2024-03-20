using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Innvo.Models.Transaction
{
    public class TransactionListItem
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        // ITEM NAME,
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("Code")]
        // ITEM CODE,
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("Action")]
        public string Action { get; set; } = string.Empty;

        [JsonPropertyName("UserId")]
        public int UserId { get; set; }
    }
}