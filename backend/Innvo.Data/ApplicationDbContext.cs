using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Innvo.Data.Entities;

namespace Innvo.Data;

public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<UnitOfMesureEntity> UOMs { get; set; }
    public DbSet<InventoryEntity> Inventories { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<TransactionItemRecordEntity> TransactionItemRecords { get; set; }
}