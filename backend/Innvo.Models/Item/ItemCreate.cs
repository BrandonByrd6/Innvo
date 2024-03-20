using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Innvo.Models.Item
{
    public class ItemCreate
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public string? ImgUrl { get; set; } = string.Empty;

        public string? BarCode { get; set; } = string.Empty;

        [Required]
        public int UOMId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}