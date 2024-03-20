using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.Item;

namespace Innvo.Services.Item
{
    public interface IItemService
    {
        //CRUD

        public Task<bool> Create(ItemCreate req);
        public Task<List<ItemListItem>> GetAll();
        public Task<ItemDetail?> GetOne(int id);
        public Task<bool> Update(ItemUpdate req);
        public Task<bool> Delete(int id);

    }
}