using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Innvo.Models.UnitOfMeasure
{
    public class UnitOfMeasureCreate
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; } = string.Empty;

        [Required]
        public string Abbreviation { get; set; } = string.Empty;
    }
}