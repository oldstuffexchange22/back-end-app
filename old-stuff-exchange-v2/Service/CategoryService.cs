using Old_stuff_exchange.Model.Category;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Service
{
    public class CategoryService
    {
        private readonly ICategoryRepository<Category> _repo;
        public CategoryService(ICategoryRepository<Category> repo)
        {
            _repo = repo;
        }

        public async Task<Category> Create(CreateCategoryModel model) {
            Category category = new Category
            {
                Name = model.Name,
            };
            if (model.ParentId != Guid.Empty) category.ParentId = model.ParentId;
            return await _repo.Create(category);
        }

        public async Task<Category> Update(UpdateCaregoryModel model) {
            Category category = new Category
            {
                Id = model.Id,
                Name = model.Name
            };
            if (model.ParentId != Guid.Empty) category.ParentId = model.ParentId;
            return await _repo.Update(category);
        }

        public async Task<bool> Delete(Guid id) {
            return await _repo.Delete(id);
        }

        public async Task<Category> GetById(Guid id){
            return await _repo.GetById(id);
        }

        public async Task<List<Category>> GetList(string filter, int page, int pageSize) { 
            return await _repo.GetList(filter, page, pageSize);
        }
    }
}
