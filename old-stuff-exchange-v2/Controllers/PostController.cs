using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Post;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Model;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly PostService _service;
        public PostController(PostService service) { 
            _service = service;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get post by id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Post post = await _service.GetById(id);
                if (post == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = post
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

        [HttpGet("list")]
        [SwaggerOperation(Summary = "Get list post")]
        public async Task<IActionResult> GetList(Guid? apartmentId, Guid? categoryId, string filterWith, string filterValue, string sortBy, string sortType, int page = 1, int pageSize = 10)
        {
            try
            {
                PagingModel pagingModel = new PagingModel
                {
                    FilterWith = filterWith,
                    FilterValue = filterValue,
                    SortBy = sortBy,
                    SortType = sortType,
                    Page = page,
                    PageSize = pageSize
                };
                List<Post> posts = await _service.GetList(apartmentId, categoryId, pagingModel);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = posts
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


        [HttpGet("user-posts/{userId}")]
        [SwaggerOperation(Summary = "Get list post by user id")]
        public async Task<IActionResult> GetListByUserId(Guid userId, string status, int page = 1, int pageSize = 10)
        {
            try
            {
                List<Post> posts = await _service.GetListByUserId(userId, status, page, pageSize);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = posts
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

        [HttpPost("exchange/buy")]
        [SwaggerOperation(Summary = "Buy post")]
        public async Task<IActionResult> BuyPost(Guid userId, Guid postId, string walletType)
        {
            try
            {
                bool result = await _service.BuyPost(userId, postId, walletType);
                if (result == false) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = _service.GetById(postId)
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

        [HttpPost("exchange/dilivered")]
        [SwaggerOperation(Summary = "Dilivered post")]
        public async Task<IActionResult> DeliveredPost(Guid postId)
        {
            try
            {
                Post result = await _service.DeliveredPost(postId);
                if (result == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = result
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

        [HttpPost("exchange/accomplished")]
        [SwaggerOperation(Summary = "Accomplished post")]
        public async Task<IActionResult> AccomplishedPost(Guid postId)
        {
            try
            {
                Post result = await _service.AccomplishedPost(postId);
                if (result == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = result
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

        [HttpPost("exchange/failure")]
        [SwaggerOperation(Summary = "Failure post")]
        public async Task<IActionResult> FailurePost(Guid postId)
        {
            try
            {
                Post result = await _service.FailurePost(postId);
                if (result == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = result
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

        [HttpPut("accept-post")]
        [SwaggerOperation(Summary = "Accept post by id")]
        public async Task<IActionResult> AcceptPost(Guid id)
        {
            try
            {
                Post post = await _service.AccepPost(id);
                if (post == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Post status not waiting to accept"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = post
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

        [HttpPut("not-accept-post")]
        [SwaggerOperation(Summary = "Accept post by id")]
        public async Task<IActionResult> NotAcceptPost(Guid id)
        {
            try
            {
                Post post = await _service.NotAccepPost(id);
                if (post == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Post status not waiting to inactive"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = post
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
