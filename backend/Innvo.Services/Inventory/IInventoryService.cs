using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.Inventory;

namespace Innvo.Services.Inventory
{
    public interface IInventoryService
    {
        
        public Task<bool> Update(InventroyUpdate request);
        public Task<List<InventoryListItem>> GetAll();
        public Task<InventoryDetail>? GetOne(int id);

    }
}