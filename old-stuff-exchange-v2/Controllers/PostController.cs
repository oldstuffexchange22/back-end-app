using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Post;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Authorize;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Model;
using old_stuff_exchange_v2.Model.Post;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly PostService _postService;
        private readonly IAuthorizationService _authorizeService;
        public PostController(PostService service, IAuthorizationService authorizationService) { 
            _postService = service;
            _authorizeService = authorizationService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get post by id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Post post = await _postService.GetById(id);
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
                List<Post> posts = await _postService.GetList(apartmentId, categoryId, pagingModel);
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
                List<Post> posts = await _postService.GetListByUserId(userId, status, page, pageSize);
                if (posts.Count > 0) {
                    bool verifyAuth = (await _authorizeService.AuthorizeAsync(User, posts[0], Operations.Read)).Succeeded;
                    if (!verifyAuth) return StatusCode(StatusCodes.Status403Forbidden);
                }
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
                Post post = await _postService.Create(model);
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

        [HttpPut("exchange/buy")]
        [SwaggerOperation(Summary = "Buy post")]
        public async Task<IActionResult> BuyPost(BuyPostModel model)
        {
            try
            {
                bool result = await _postService.BuyPost(model.UserId, model.PostId, model.WalletType);
                if (result == false) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = _postService.GetById(model.PostId)
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

        [HttpPut("exchange/dilivered")]
        [SwaggerOperation(Summary = "Dilivered post")]
        public async Task<IActionResult> DeliveredPost(IdPostModel model)
        {
            try
            {
                Post postAuthorize = await _postService.GetById(model.PostId);
                if (postAuthorize == null)
                {
                    return NotFound();
                }
                else
                {
                    bool verifyAuth = (await _authorizeService.AuthorizeAsync(User, postAuthorize, Operations.Update)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                Post result = await _postService.DeliveredPost(model.PostId);
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

        [HttpPut("exchange/accomplished")]
        [SwaggerOperation(Summary = "Accomplished post")]
        public async Task<IActionResult> AccomplishedPost(IdPostModel model)
        {
            try
            {
                Post postAuthorize = await _postService.GetById(model.PostId);
                if (postAuthorize == null)
                {
                    return NotFound();
                }
                else
                {
                    bool verifyAuth = (await _authorizeService.AuthorizeAsync(User, postAuthorize, Operations.Dilivered)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                Post result = await _postService.AccomplishedPost(model.PostId);
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

        [HttpPut("exchange/failure")]
        [SwaggerOperation(Summary = "Failure post")]
        public async Task<IActionResult> FailurePost(IdPostModel model)
        {
            try
            {
                Post postAuthorize = await _postService.GetById(model.PostId);
                if (postAuthorize == null)
                {
                    return NotFound();
                }
                else
                {
                    bool verifyAuth = (await _authorizeService.AuthorizeAsync(User, postAuthorize, Operations.Dilivered)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                Post result = await _postService.FailurePost(model.PostId);
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
                Post postAuthorize = await _postService.GetById(model.Id);
                if (postAuthorize == null)
                {
                    return NotFound();
                }
                else
                {
                    bool verifyAuth = (await _authorizeService.AuthorizeAsync(User, postAuthorize, Operations.Update)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                var post = await _postService.Update(model);
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
        [Authorize(Policy = PolicyName.ADMIN)]
        public async Task<IActionResult> AcceptPost(IdPostModel model)
        {
            try
            {
                Post post = await _postService.AccepPost(model.PostId);
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
        [Authorize(Policy = PolicyName.ADMIN)]
        public async Task<IActionResult> NotAcceptPost(IdPostModel model)
        {
            try
            {
                Post post = await _postService.NotAccepPost(model.PostId);
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
                Post postAuthorize = await _postService.GetById(id);
                if (postAuthorize == null)
                {
                    return NotFound();
                }
                else
                {
                    bool verifyAuth = (await _authorizeService.AuthorizeAsync(User, postAuthorize, Operations.Delete)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                bool result = await _postService.Delete(id);
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
