using Microsoft.EntityFrameworkCore;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Implement
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) { 
            _context = context;
        }
        public async Task<Product> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? product : null;
        }

        public async Task<bool> Delete(Guid id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;
            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Product> Update(Product product)
        {
            Product proDb = _context.Products.AsNoTracking().SingleOrDefault(p => p.Id == product.Id);
            product.PostId = proDb.PostId;
            _context.Products.Update(product);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? product : null;
        }
    }
}
