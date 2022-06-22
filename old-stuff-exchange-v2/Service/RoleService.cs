using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Service
{
    public class RoleService
    {
        private readonly IRoleRepository<Role> _repo;
        public RoleService(IRoleRepository<Role> repo)
        {
            _repo = repo;
        }
        public async Task<Role> Create(Role role)
        {
            return await _repo.Create(role);
        }
        public async Task<bool> Update(Role updateRole)
        {
            return await _repo.Update(updateRole);
        }
        public async Task<Role> GetById(Guid id)
        {
            return await _repo.GetById(id);
        }
        public async Task<List<Role>> GetList()
        {
            return await _repo.GetList();
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repo.Delete(id);
        }
    }
}
