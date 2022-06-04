using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface IBuildingRepository<T>
    {
        Task<Building> Create(Building building);
        Task<Building> Update(Building building);
        Task<bool> Delete(Guid ID);
        List<Building> GetList(Guid? apartmentId ,int page, int pageSize);
        Task<Building> GetById(Guid ID);
    }
}
