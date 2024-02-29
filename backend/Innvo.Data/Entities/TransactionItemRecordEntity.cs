using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Innvo.Data.Entities
{
    public class TransactionItemRecordEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Transaction))]    
        public int TransactionId { get; set; }
        public TransactionEntity Transaction { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; } = null!;

        public int Change { get; set; }
        
    }
}