using Microsoft.EntityFrameworkCore;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Implement
{
    public class BuildingRepository : IBuildingRepository<Building>
    {
        private readonly AppDbContext _context;
        public BuildingRepository(AppDbContext context) { 
            _context = context;
        }
        public async Task<Building> Create(Building building)
        {
            await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();
            return building;
        }

        public async Task<bool> Delete(Guid ID)
        {
            Building building = _context.Buildings.FirstOrDefault(building => building.Id == ID);
            if (building == null) return false;
            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Building> GetById(Guid ID)
        {
            Building building = await _context.Buildings.SingleOrDefaultAsync(building => building.Id == ID);
            return building;    
        }

        public List<Building> GetList(Guid? apartmentId ,string name,int page, int pageSize)
        {
            var allBuilding = _context.Buildings.Include(b => b.Apartment).AsQueryable();
            #region Filtering
            if (apartmentId != null)
                allBuilding = allBuilding.Where(building => building.ApartmentId == apartmentId);
            if (!string.IsNullOrEmpty(name))
                allBuilding = allBuilding.Where(building => building.Name.ToLower().Contains(name.ToLower()));
            #endregion
            #region Paging
            var result = PaginatedList<Building>.Create(allBuilding, page, pageSize);
            #endregion
            return result.ToList();
        }

        public async Task<Building> Update(Building building)
        {
            Building dbDatabase = _context.Buildings.AsNoTracking().FirstOrDefault(b => b.Id == building.Id);
            if (dbDatabase == null) return null;
            _context.Buildings.Update(building);
            await _context.SaveChangesAsync();
            return building;
        }

        
    }
}
