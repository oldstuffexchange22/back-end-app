
using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface IRoleRepository<T>
    {
        Task<Role> Create(Role role);
        Task<bool> Update(Role newRole);
        Task<Role> GetById(Guid id);
        Task<List<Role>> GetList();
        Task<bool> Delete(Guid id);

    }
}
