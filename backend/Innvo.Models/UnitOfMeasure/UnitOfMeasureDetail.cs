using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Innvo.Models.UnitOfMeasure
{
    public class UnitOfMeasureDetail
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("Description")]
        public string? Description { get; set; } = string.Empty;

        [JsonPropertyName("Abbreviation")]
        public string Abbreviation { get; set; } = string.Empty;
    }
}