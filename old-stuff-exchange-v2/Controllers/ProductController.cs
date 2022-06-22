using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Product;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly ProductService _productService;
        private readonly ResponseCacheService _cacheService;
        private readonly IAuthorizationService _authorizeService;
        public ProductController(ProductService service, ResponseCacheService cacheService, IAuthorizationService authorizationService)
        {
            _productService = service;
            _cacheService = cacheService;
            _authorizeService = authorizationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get list product by post id")]
        public async Task<IActionResult> GetListByPostId(Guid postId)
        {
            try
            {
                List<Product> products = await _productService.GetListByPostId(postId);
                if (products == null) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = products
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new product")]
        public async Task<IActionResult> Create(CreateProductModel model)
        {
            try
            {
                Product product = await _productService.Create(model);
                if (product == null) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
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

        [HttpPut()]
        [SwaggerOperation(Summary = "Update product")]
        public async Task<IActionResult> Update(UpdateProductModel model)
        {
            try
            {
                Product product = await _productService.Update(model);
                if (product == null) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
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
                bool result = await _productService.Delete(id);
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
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
