using Microsoft.EntityFrameworkCore;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Role;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Implement
{
    public class ApartmentRepository : IApartmentRepository<Apartment>
    {
        private readonly AppDbContext _context;
        public ApartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Apartment> Create(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        public async Task<Apartment> Update(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        public async Task<bool> Delete(Guid Id)
        {
            Apartment apartment = _context.Apartments.Include(a => a.Buildings).ThenInclude(b => b.Users).SingleOrDefault(a => a.Id == Id);
            _context.Apartments.Remove(apartment);
            int result = await _context.SaveChangesAsync();
            /*List<User> users = _context.Users.Include(u => u.Role).Where(u => u.BuildingId == null && u.Role.Name != RoleNames.ADMIN).ToList();
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();*/
            return result > 0;
        }

        public async Task<List<Apartment>> GetList()
        {
            var apartments = _context.Apartments.Include(a => a.Buildings).Where(a => a.Buildings.Count > 0).ToList();
            return await Task.FromResult(apartments);
        }

        public async Task<Apartment> GetById(Guid Id)
        {
            return await Task.FromResult(_context.Apartments.SingleOrDefault(a => a.Id == Id));
        }
    }
}
