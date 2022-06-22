using Old_stuff_exchange.Repository.Implement;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Entities.Extentions;
using old_stuff_exchange_v2.Model.Building;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Service
{
    public class BuildingService
    {
        private readonly IBuildingRepository<Building> _repo;
        public BuildingService(IBuildingRepository<Building> repo)
        {
            _repo = repo;
        }
        public async Task<Building> Create(Building building) { 
            return await _repo.Create(building);
        }

        public async Task<Building> Update(Building building) {
            return await _repo.Update(building);
        }

        public Task<bool> Delete(Guid id) {
            return _repo.Delete(id);
        }

        public async Task<List<ResponseBuildingModel>> GetList(Guid? apartmentId,string name, int page, int pageSize) {
            List<Building> buildings = await _repo.GetList(apartmentId, name, page, pageSize);
            List<ResponseBuildingModel> response = buildings.ConvertAll<ResponseBuildingModel>(building => building.ToResponseModel());
            return response;
        }

        public async Task<Building> GetById(Guid Id) {
            return await _repo.GetById(Id);
        }
    }
}
