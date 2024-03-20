using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Innvo.Models.Inventory
{
    public class InventoryListItem
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }


        // ITEM NAME,
        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;

        // ITEM CODE,
        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;

        // UOM Abbrivation
        [JsonPropertyName("Abbrivation")]
        public string Abbrivation { get; set; } = string.Empty;

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

    }
}