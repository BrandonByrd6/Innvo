using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Data;
using Innvo.Data.Entities;
using Innvo.Models.Item;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Innvo.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly int _userId;

        public ItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ItemListItem>> All()
        {
            return await _dbContext.Items.Select(e => new ItemListItem()
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                BarCode = e.BarCode
            }).ToListAsync();
        }

        public async Task<bool> Create(ItemCreate req)
        {
            var entity = new ItemEntity()
            {
                Name = req.Name,
                Description = req.Description,
                Code = req.Code,
                ImgUrl = req.ImgUrl,
                BarCode = req.BarCode
            };

            _dbContext.Items.Add(entity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> Delete(int id)
        {
            ItemEntity? entity = await _dbContext.Items.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.Items.Remove(entity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<ItemDetail?> Get(int id)
        {
            ItemEntity? entity = await _dbContext.Items.FindAsync(id);

            if (entity == null)
                return null;

            return new ItemDetail()
            {
                Name = entity.Name,
                Description = entity.Description,
                Code = entity.Code,
                ImgUrl = entity.ImgUrl,
                BarCode = entity.BarCode
            };
        }

        public async Task<bool> Update(ItemUpdate req)
        {
            ItemEntity? entity = await _dbContext.Items.FindAsync(req.Id);
            if (entity == null)
                return false;

            entity.Name = req.Name;
            entity.Description = req.Description;
            entity.Code = req.Code;
            entity.ImgUrl = req.ImgUrl;
            entity.BarCode = req.BarCode;

            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    }
}