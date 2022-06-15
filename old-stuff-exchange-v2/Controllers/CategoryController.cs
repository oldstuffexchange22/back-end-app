using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Category;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize(Policy = PolicyName.ADMIN)]
    public class CategoryController : ControllerBase
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

        [HttpGet("list")]
        [SwaggerOperation(Summary = "Get list categories")]
        public async Task<IActionResult> GetList(string name, int page = 1, int pageSize = 10) {
            try
            {
                List<Category> categories = await _service.GetList(name, page, pageSize);
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
