using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Post;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Attributes;
using old_stuff_exchange_v2.Authorize;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Enum.Post;
using old_stuff_exchange_v2.Model;
using old_stuff_exchange_v2.Model.Post;
using old_stuff_exchange_v2.Service;
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
        private readonly CacheService _cacheService;
        public PostController(PostService service, IAuthorizationService authorizationService, CacheService cacheService) { 
            _postService = service;
            _authorizeService = authorizationService;
            _cacheService = cacheService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get post by id")]
        [Cache(1)]
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

        [HttpGet()]
        [SwaggerOperation(Summary = "Get list post")]
        [Cache(1)]
        public async Task<IActionResult> GetList(Guid? exceptAuthorId,Guid? apartmentId, Guid? categoryId,string status, string filterWith, string filterValue, string sortBy, string sortType, int page = 1, int pageSize = 10)
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
                    PageSize = pageSize,
                    Status = status
                };
                List<Post> posts = await _postService.GetList(exceptAuthorId,apartmentId, categoryId, pagingModel);
                
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


        [HttpGet("user/{userId}")]
        [SwaggerOperation(Summary = "Get list post by user id")]
        [Cache(1)]
        public async Task<IActionResult> GetListByUserId(Guid userId, string status, int page = 1, int pageSize = 10, bool isOrderLastUpdate = false)
        {
            try
            {
                List<Post> posts = await _postService.GetListByUserId(userId, status, page, pageSize, isOrderLastUpdate);
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
        [HttpGet("userBought/{userId}")]
        [SwaggerOperation(Summary = "Get list post by user bought")]
        [Cache(1)]
        public async Task<IActionResult> GetListByUserBought(Guid userId, string status, int page = 1, int pageSize = 10)
        {
            try
            {
                List<Post> posts = await _postService.GetListByUserBought(userId, status, page, pageSize);
                /*if (posts.Count > 0)
                {
                    bool verifyAuth = (await _authorizeService.AuthorizeAsync(User, posts[0], Operations.Read)).Succeeded;
                    if (!verifyAuth) return StatusCode(StatusCodes.Status403Forbidden);
                }*/
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
                string controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
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
                string controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
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
        [HttpPut("status")]
        [SwaggerOperation(Summary = "Action to change status post")]
        public async Task<IActionResult> ChangeStatus(PostStatusModel model)
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
                    if (verifyAuth == false && !model.Status.ToUpper().Equals(PostStatus.DELIVERY)) return StatusCode(StatusCodes.Status403Forbidden);
                }
                string statusUpdate = model.Status.ToUpper();
                Guid userId = Guid.Parse(User.FindFirst("id").ToString().Replace("id: ", ""));
                switch (statusUpdate)
                {
                    case PostStatus.ACTIVE:
                        if (!postAuthorize.Status.Equals(PostStatus.WAITING)) return BadRequest("Post is not waiting to active");
                        await _postService.AccepPost(model.PostId);
                        break;
                    case PostStatus.INACTIVE:
                        if (!postAuthorize.Status.Equals(PostStatus.WAITING)) return BadRequest("Post is not waiting to inactive");
                        await _postService.NotAccepPost(model.PostId);
                        break;
                    case PostStatus.DELIVERY:
                        if (!postAuthorize.Status.Equals(PostStatus.ACTIVE)) return BadRequest("Post is not active to buy");
                        await _postService.BuyPost(userId, model.PostId, model.WalletType);
                        break;
                    case PostStatus.DELIVERED:
                        if (!postAuthorize.Status.Equals(PostStatus.DELIVERY)) return BadRequest("Post is not delivery to change delivered");
                        await _postService.DeliveredPost(model.PostId);
                        break;
                    case PostStatus.ACCOMPLISHED:
                        if (!postAuthorize.Status.Equals(PostStatus.DELIVERED)) return BadRequest("Post is not delivered to accomplished");
                        await _postService.AccomplishedPost(model.PostId);
                        break;
                    case PostStatus.FAILURE:
                        if (!postAuthorize.Status.Equals(PostStatus.DELIVERED)) return BadRequest("Post is not delivered to failure");
                        await _postService.FailurePost(model.PostId);
                        break;
                    default: return StatusCode(StatusCodes.Status400BadRequest);
                }
                string controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = await _postService.GetById(model.PostId)
                }); ;
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
                if (!result) return BadRequest();
                string controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
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
