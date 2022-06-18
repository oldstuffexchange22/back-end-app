
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
    public class CategoryRepository : ICategoryRepository<Category>
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            var result = await _context.SaveChangesAsync();
            if (result <= 0) return null;
            return category;
        }

        public async Task<bool> Delete(Guid id)
        {
            Category category = _context.Categories.Include(c => c.CategoriesChildren)
                                        .ThenInclude(c => c.CategoriesChildren)
                                        .ThenInclude(c => c.CategoriesChildren)
                                        .ThenInclude(c => c.CategoriesChildren).FirstOrDefault(cat => cat.Id == id);
            if (category == null) return false;
            _context.Remove(category);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Category> GetById(Guid id)
        {
            Category category = _context.Categories.Include(c => c.CategoriesChildren)
                            .Include(c => c.CategoriesChildren)
                            .Include(c => c.CategoriesChildren)
                            .Include(c => c.CategoriesChildren).FirstOrDefault(cat => cat.Id == id);
            if (category == null) return null;
            return await Task.FromResult(category);
        }

        public async Task<List<Category>> GetList(string filter, int page, int pageSize)
        {
            var allCategories = _context.Categories.AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(filter))
                allCategories = allCategories.Where(cat => cat.Name.Contains(filter));
            #endregion
            #region Paging
            var result = PaginatedList<Category>.Create(allCategories, page, pageSize);
            #endregion
            return await Task.FromResult(result.ToList());
        }

        public async Task<Category> Update(Category category)
        {
            _context.Categories.Update(category);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? category : null;
        }

    }
}
