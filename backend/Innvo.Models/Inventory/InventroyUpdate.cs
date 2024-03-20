using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innvo.Models.Inventory
{
    public class InventroyUpdate
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UnitOfMesureId { get; set; }
        public int Quantity { get; set; }
    }
}