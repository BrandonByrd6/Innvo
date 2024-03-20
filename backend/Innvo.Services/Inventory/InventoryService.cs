using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Data;
using Innvo.Data.Entities;
using Innvo.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Innvo.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly int _userId;

        public InventoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<InventoryListItem>> GetAll()
        {
            List<InventoryEntity> inventoryEntities = await _dbContext.Inventories.ToListAsync();
            
            List<InventoryListItem> res = new List<InventoryListItem>();
            foreach(var ie in inventoryEntities) {
                ItemEntity? itemEntity = _dbContext.Items.Find(ie.ItemId);
                UnitOfMesureEntity? UOMEntity = _dbContext.UOMs.Find(ie.UnitOfMesureId);
                if(itemEntity == null|| UOMEntity == null) {
                    continue;
                }
                
                InventoryListItem listItem = new(){
                    Id = ie.Id,
                    Name = itemEntity.Name,
                    Code = itemEntity.Code,
                    Abbrivation = UOMEntity.Abbreviation,
                    Quantity = ie.Quantity
                };

                res.Add(listItem);
            } 

            return res; 
        }

        public async Task<InventoryDetail>? GetOne(int id)
        {
            InventoryEntity? inventoryEntity = await _dbContext.Inventories.FindAsync(id);
            
            if(inventoryEntity == null)
                return null;
            
            ItemEntity? itemEntity = await _dbContext.Items.FindAsync(inventoryEntity.ItemId);
            UnitOfMesureEntity? UOMEntity = await _dbContext.UOMs.FindAsync(inventoryEntity.UnitOfMesureId);
            
            if(itemEntity == null|| UOMEntity == null) {
                return null;
            }
                
            InventoryDetail detail = new(){
                Id = inventoryEntity.Id,
                ItemId = itemEntity.Id,
                UOMId = UOMEntity.Id,
                Name = itemEntity.Name,
                Code = itemEntity.Code,
                UOMName = UOMEntity.Name,
                Quantity = inventoryEntity.Quantity
            };

            return detail;
        }

        //TODO: Create a Transaction during this portion.
        public async Task<bool> Update(InventroyUpdate request)
        {
            
            InventoryEntity? entity = await _dbContext.Inventories.FindAsync(request.Id);
            if (entity == null)
                return false;

            entity.ItemId = request.ItemId;
            entity.UnitOfMesureId = request.UnitOfMesureId;
            entity.Quantity = request.Quantity;

            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    }
}