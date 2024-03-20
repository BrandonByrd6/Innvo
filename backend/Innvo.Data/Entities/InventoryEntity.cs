using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Innvo.Data.Entities
{
    public class InventoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UnitOfMesure))]
        public int UnitOfMesureId { get; set; }
        public UnitOfMesureEntity UnitOfMesure { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
    }
}