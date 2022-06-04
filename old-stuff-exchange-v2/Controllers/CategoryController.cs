using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Category;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly CategoryService _service;
        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get category with id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Category category = await _service.GetById(id);
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

        [Route("list")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get list categories")]
        public async Task<IActionResult> GetList(string filter, int page = 1, int pageSize = 10) {
            // filter by name
            try
            {
                List<Category> categories = await _service.GetList(filter, page, pageSize);
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

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update category")]
        public async Task<IActionResult> Update(Guid id, UpdateCaregoryModel category) {
            try
            {
                if (id != category.Id) return BadRequest();
                Category result = await _service.Update(category);
                if (result == null) return BadRequest();
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
                Category result = await _service.Create(model);
                if (result == null) return BadRequest();
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
                bool result = await _service.Delete(id);
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
