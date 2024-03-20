using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Innvo.Models.Item
{
    public class ItemDetail
    {

       [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;
        
        [JsonPropertyName("Description")]
        public string Description { get; set; } = string.Empty;
        
        [JsonPropertyName("ImgUrl")]
        public string? ImgUrl { get; set; } = string.Empty;

        [JsonPropertyName("BarCode")]
        public string? BarCode { get; set; } = string.Empty;
    }
}