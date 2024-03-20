using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Data;
using Innvo.Data.Entities;
using Innvo.Models.Transaction;
using Microsoft.EntityFrameworkCore;

namespace Innvo.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly int _userId;

        public TransactionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<TransactionListItem>> GetAll()
        {
            List<TransactionEntity> transactionsEntities = await _dbContext.Transactions.ToListAsync();
            
            List<TransactionListItem> res = new List<TransactionListItem>();
            foreach(var te in transactionsEntities) {
                TransactionItemRecordEntity? recordEntity = await _dbContext.TransactionItemRecords.FirstOrDefaultAsync(entity => entity.TransactionId == te.Id);
                if(recordEntity == null) {
                    continue;
                }
                ItemEntity? itemEntity = _dbContext.Items.Find(recordEntity.ItemId);
                if(itemEntity == null) {
                    continue;
                }
                
                TransactionListItem listItem = new(){
                    Id = te.Id,
                    Name = itemEntity.Name,
                    Code = itemEntity.Code,
                    Action = te.Action!,
                    UserId = te.UserId,
                };

                res.Add(listItem);
            } 

            return res; 
        }

        public async Task<TransactionDetail>? GetOne(int id)
        {
                TransactionEntity? transactionEntity = _dbContext.Transactions.Find(id);
                TransactionItemRecordEntity? recordEntity = await _dbContext.TransactionItemRecords.FirstOrDefaultAsync(entity => entity.TransactionId == id);
                if(recordEntity == null || transactionEntity == null) {
                    return null;
                }
                ItemEntity? itemEntity = _dbContext.Items.Find(recordEntity.ItemId);
                if(itemEntity == null) {
                    return null;
                }
                
                TransactionDetail detail = new(){
                    Id = transactionEntity.Id,
                    Name = itemEntity.Name,
                    Code = itemEntity.Code,
                    Action = transactionEntity.Action!,
                    UserId = transactionEntity.UserId,
                };
            
                return detail;
        }
    }
}