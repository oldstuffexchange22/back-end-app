using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Attributes;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Model.Deposit;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Controllers
{
    public class DepositController : BaseApiController
    {
        private readonly DepositService _depositService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ResponseCacheService _responseCacheService;

        public DepositController(DepositService depositService, IAuthorizationService authorizationService, ResponseCacheService responseCacheService)
        {
            _depositService = depositService;
            _authorizationService = authorizationService;
            _responseCacheService = responseCacheService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get deposit by Id")]
        [Cache(100)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Deposit deposit = await _depositService.GetById(id);
                if (deposit == null)
                {
                    return NotFound();
                }
                else {
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, deposit, Operations.Read)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = deposit
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("user/{userId}")]
        [SwaggerOperation(Summary = "Get deposit by user id")]
        public async Task<IActionResult> GetByUserId(Guid userId, int page = 1, int pageSize = 10)
        {
            try
            {
                List<Deposit> deposits = await _depositService.GetListByUserId(userId, page, pageSize);
                if (deposits == null || deposits.Count == 0) { 
                    return NotFound();
                }
                else{
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, deposits[0], Operations.Read)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = deposits
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create deposit")]
        public async Task<IActionResult> Create(CreateDepositModel model) {
            try
            {
                Deposit deposit =await _depositService.Create(model);
                if (deposit == null) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _responseCacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = deposit
                });
            }
            catch (Exception ex) { 
                return BadRequest(ex);
            }
        }
    }
}
