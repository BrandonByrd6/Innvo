using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Innvo.Models.Inventory
{
    public class InventoryDetail
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        // ITEM NAME,
        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;
        
        // ITEM NAME,
        [JsonPropertyName("ItemId")]
        public int ItemId { get; set; }

        [JsonPropertyName("UOMId")]
        public int UOMId { get; set; }

        // ITEM DESCIPTION
        [JsonPropertyName("Description")]
        public string Description { get; set; } = string.Empty;

        // ITEM CODE
        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;

        // UOM Name
        [JsonPropertyName("UOMName")]
        public string UOMName { get; set; } = string.Empty;

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
    }
}