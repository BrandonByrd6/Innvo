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

        public ItemService(UserManager<UserEntity> userManager,
                             SignInManager<UserEntity> signInManager,
                             ApplicationDbContext dbContext)
        {
            var currentUser = signInManager.Context.User;
            var userIdClaim = userManager.GetUserId(currentUser);
            var hasValidId = int.TryParse(userIdClaim, out _userId);
            if (hasValidId == false)
                throw new Exception("Attempted to build RatingService without ID claim");

            _dbContext = dbContext;
        }

        public async Task<List<ItemListItem>> GetAll()
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
            var transaction = new TransactionEntity(){
                UserId = _userId,
                Action = "Item Created"
            };

            var entity = new ItemEntity()
            {
                Name = req.Name,
                Description = req.Description,
                Code = req.Code,
                ImgUrl = req.ImgUrl,
                BarCode = req.BarCode
            };

            _dbContext.Items.Add(entity);
            _dbContext.Transactions.Add(transaction);
        
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            if(numberOfChanges != 2) {
                return false;
            }

            _dbContext.Inventories.Add(new InventoryEntity(){
                ItemId = entity.Id,
                UnitOfMesureId = req.UOMId,
                Quantity = req.Quantity,
            });

            _dbContext.TransactionItemRecords.Add(new TransactionItemRecordEntity(){
                TransactionId = transaction.Id,
                ItemId = entity.Id
            });

            numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 2;
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

        public async Task<ItemDetail?> GetOne(int id)
        {
            ItemEntity? entity = await _dbContext.Items.FindAsync(id);

            if (entity == null)
                return null;

            return new ItemDetail()
            {
                Id = entity.Id,
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
            
            var transaction = new TransactionEntity(){
                UserId = _userId,
                Action = "Item Updated"
            };

            entity.Name = req.Name;
            entity.Description = req.Description;
            entity.Code = req.Code;
            entity.ImgUrl = req.ImgUrl;
            entity.BarCode = req.BarCode;

            _dbContext.Transactions.Add(transaction);

            int numberOfChanges = await _dbContext.SaveChangesAsync();
            if(numberOfChanges != 2) return false;

            _dbContext.TransactionItemRecords.Add(new TransactionItemRecordEntity(){
                TransactionId = transaction.Id,
                ItemId = entity.Id
            });

             numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}