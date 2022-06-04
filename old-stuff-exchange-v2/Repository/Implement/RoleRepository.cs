using Microsoft.EntityFrameworkCore;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Implement
{
    public class RoleRepository : IRoleRepository<Role>
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Role> Create(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task<bool> Update(Role updateRole)
        {
            Role role = _context.Roles.FirstOrDefault(x => x.Id == updateRole.Id);
            if (role == null)
            {
                return false;
            }
            role.Name = updateRole.Name;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Role> GetById(Guid id)
        {
            Role role = await _context.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (role == null)
            {
                return null;
            }
            return role;
        }
        public async Task<List<Role>> GetList()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<bool> Delete(Guid id)
        {
            Role role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
            {
                return false;
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
