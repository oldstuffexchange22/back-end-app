using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Post;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly PostService _service;
        public PostController(PostService service) { 
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new post")]
        public async Task<IActionResult> Create(CreatePostModel model)
        {
            try
            {
                Post post = await _service.Create(model);
                if (post == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = post
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
        [SwaggerOperation(Summary = "Update post")]
        public async Task<IActionResult> Update(UpdatePostModel model)
        {
            try
            {
                var post = await _service.Update(model);
                if (post == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = post
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

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete post")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await _service.Delete(id);
                return Ok(new ApiResponse
                {
                    Success = result,
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
