using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Category;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Attributes;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    [Route("api/v1.0/categories")]
    [ApiController]
    [Authorize(Policy = PolicyName.ADMIN)]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ResponseCacheService _cacheService;
        public CategoryController(CategoryService service, ResponseCacheService cacheService)
        {
            _categoryService = service;
            _cacheService = cacheService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get category with id")]
        [Cache(100)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Category category = await _categoryService.GetById(id);
                if (category == null) return NotFound();
                return Ok(new ApiResponse {
                    Success = true,
                    Data = category
                });
            }
            catch(Exception ex) {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpGet()]
        [SwaggerOperation(Summary = "Get list categories")]
        [Cache(100)]
        public async Task<IActionResult> GetList(string name, int page = 1, int pageSize = 10) {
            try
            {
                List<Category> categories = await _categoryService.GetList(name, page, pageSize);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = categories
                });
            }
            catch(Exception ex) {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpPut()]
        [SwaggerOperation(Summary = "Update category")]
        public async Task<IActionResult> Update(UpdateCaregoryModel category) {
            try
            {
                Category result = await _categoryService.Update(category);
                if (result == null) return BadRequest();
                await _cacheService.RemoveCacheResponseAsync("categories");
                return Ok(new ApiResponse {
                    Success = true,
                    Data = result
                });
            }
            catch(Exception ex) {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create category")]
        public async Task<IActionResult> Create(CreateCategoryModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name)) return BadRequest();
                Category result = await _categoryService.Create(model);
                if (result == null) return BadRequest();
                await _cacheService.RemoveCacheResponseAsync("categories");
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = result
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete category")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await _categoryService.Delete(id);
                await _cacheService.RemoveCacheResponseAsync("categories");
                return Ok(new ApiResponse
                {
                    Success = result
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }
    }

}
