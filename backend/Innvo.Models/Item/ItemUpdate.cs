using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Innvo.Models.Item
{
    public class ItemUpdate
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ImgUrl { get; set; } = string.Empty;

        public string? BarCode { get; set; } = string.Empty;
    }
}