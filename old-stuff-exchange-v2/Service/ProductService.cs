using Old_stuff_exchange.Model.Product;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Service
{
    public class ProductService
    {
        private readonly IProductRepository<Product> _repo;
        public ProductService(IProductRepository<Product> repo)
        {
            _repo = repo;
        }

        public async Task<Product> Create(CreateProductModel model) {
            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                PostId = model.PostId,
            };
            if(model.CategoryId != Guid.Empty) product.CategoryId = model.CategoryId;
            return await _repo.Create(product);
        }

        public async Task<Product> Update(UpdateProductModel model) {
            Product product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Description = model.Description,
            };
            return await _repo.Update(product);
        }

        public async Task<bool> Delete(Guid id) { 
            return await _repo.Delete(id);
        }

        public async Task<List<Product>> GetListByPostId(Guid postId) { 
            return await _repo.GetListByPostId(postId);
        }
    }
}
