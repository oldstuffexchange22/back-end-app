using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Model.Apartment;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Service
{
    public class ApartmentService
    {
        private readonly IApartmentRepository<Apartment> _repo;
        public ApartmentService(IApartmentRepository<Apartment> repo)
        {
            _repo = repo;
        }

        public async Task<Apartment> Create(CreateApartmentModel model) { 
            Apartment apartment = new Apartment { 
                Name =  model.Name,
                Description = model.Description,
                Address = model.Address
            };
            return await _repo.Create(apartment);
        }

        public async Task<Apartment> Update(UpdateApartmentModel model) {
            Apartment apartment = new Apartment
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Address = model.Address
            };
            return await _repo.Update(apartment);
        }

        public async Task<bool> Delete(Guid id) { 
            return await _repo.Delete(id);
        }

        public async Task<List<Apartment>> GetList() {
            return await _repo.GetList();
        }

        public async Task<Apartment> GetById(Guid id) {
            return await _repo.GetById(id);
        }
    }
}
