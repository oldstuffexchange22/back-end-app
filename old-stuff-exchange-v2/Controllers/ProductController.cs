using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Product;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly ProductService _service;
        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new product")]
        public async Task<IActionResult> Create(CreateProductModel model)
        {
            try
            {
                Product product = await _service.Create(model);
                if (product == null) return BadRequest();
                return Ok(new ApiResponse {
                    Success = true,
                    Data = product
                });
            }
            catch (Exception ex){
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update product")]
        public async Task<IActionResult> Update(Guid id, UpdateProductModel model)
        {
            try
            {
                Product product = await _service.Update(model);
                if (product == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = product
                });
            }
            catch (Exception ex){
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await _service.Delete(id);
                return Ok(new ApiResponse { 
                    Success = result
                });
            }
            catch (Exception ex){
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }
    }
}
