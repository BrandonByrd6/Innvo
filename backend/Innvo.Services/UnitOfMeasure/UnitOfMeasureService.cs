using Innvo.Data;
using Innvo.Data.Entities;
using Innvo.Models.UnitOfMeasure;
using Microsoft.EntityFrameworkCore;

namespace Innvo.Services.UnitOfMeasure
{

    public class UnitOfMeasureService : IUnitOfMeasureService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly int _userId;

        public UnitOfMeasureService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(UnitOfMeasureCreate req)
        {
            var entity = new UnitOfMesureEntity()
            {
                Name = req.Name,
                Description = req.Description,
                Abbreviation = req.Abbreviation
            };

            _dbContext.UOMs.Add(entity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<List<UnitOfMeasureListItem>> GetAll()
        {
            return await _dbContext.UOMs.Select(e => new UnitOfMeasureListItem()
            {
                Id = e.Id,
                Name = e.Name,
                Abbreviation = e.Abbreviation
            }).ToListAsync();
        }

        public async Task<UnitOfMeasureDetail?> GetOne(int id)
        {
            UnitOfMesureEntity? entity = await _dbContext.UOMs.FindAsync(id);

            if (entity == null)
                return null;

            return new UnitOfMeasureDetail()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Abbreviation = entity.Abbreviation
            };
        }

        public async Task<bool> Update(UnitOfMeasureUpdate req)
        {
            UnitOfMesureEntity? entity = await _dbContext.UOMs.FindAsync(req.Id);
            if (entity == null)
                return false;

            entity.Name = req.Name;
            entity.Description = req.Description;
            entity.Abbreviation = req.Abbreviation;

            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> Delete(int id)
        {
            UnitOfMesureEntity? entity = await _dbContext.UOMs.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.UOMs.Remove(entity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}