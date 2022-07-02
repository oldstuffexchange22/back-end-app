using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Interface
{
    public interface IApartmentRepository<T>
    {
        Task<Apartment> Create(Apartment apartment);
        Task<Apartment> Update(Apartment apartment);
        Task<bool> Delete(Guid Id);
        Task<List<Apartment>> GetList(bool isBuildingsNull);
        Task<Apartment> GetById(Guid Id);
    }
}
