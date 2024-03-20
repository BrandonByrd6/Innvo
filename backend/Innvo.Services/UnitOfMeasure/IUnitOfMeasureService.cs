using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.UnitOfMeasure;

namespace Innvo.Services.UnitOfMeasure
{
    public interface IUnitOfMeasureService 
    {
        public Task<bool> Create(UnitOfMeasureCreate req);
        public Task<List<UnitOfMeasureListItem>> GetAll();
        public Task<UnitOfMeasureDetail?> GetOne(int id);
        public Task<bool> Update(UnitOfMeasureUpdate req);
        public Task<bool> Delete(int id);
    }
}