using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innvo.Models.Item
{
    public class ItemListItem
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? BarCode { get; set; } = string.Empty;
    }
}