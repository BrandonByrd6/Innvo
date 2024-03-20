using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Innvo.Models.Item
{
    public class ItemListItem
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;
        
        [JsonPropertyName("BarCode")]
        public string? BarCode { get; set; } = string.Empty;
    }
}