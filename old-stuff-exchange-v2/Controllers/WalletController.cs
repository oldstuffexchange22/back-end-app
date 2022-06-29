using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Attributes;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Model.Wallet;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Controllers
{
    public class WalletController : BaseApiController
    {
        private readonly WalletService _walletService;
        private readonly IAuthorizationService _authorizationService;
        private readonly CacheService _cacheService;
        public WalletController(WalletService service, IAuthorizationService authorizationService, CacheService cacheService)
        {
            _walletService = service;
            _authorizationService = authorizationService;
            _cacheService = cacheService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Find by id")]
        [AllowAnonymous]
        [Cache(1)]
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                Wallet wallet = await _walletService.FindById(id);
                if (wallet == null)
                {
                    return NotFound();
                }
                else {
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, wallet, Operations.Read)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = wallet
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

        [HttpGet("type/{type}")]
        [SwaggerOperation(Summary = "Find by type (use for system wallet and chairity wallet)")]
        public async Task<ActionResult> GetByType(string type)
        {
            try
            {
                Wallet wallet = await _walletService.FindByType(type);
                if (wallet == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = wallet
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
        [SwaggerOperation(Summary = "Find wallets of user")]
        public async Task<ActionResult> GetByType(Guid userId)
        {
            try
            {
                List<Wallet> wallets = await _walletService.FindByUserId(userId);
                if (wallets == null || wallets.Count == 0) {
                    return NotFound();
                }
                else
                {
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, wallets[0], Operations.Read)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = wallets
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

        [HttpPost()]
        [SwaggerOperation(Summary = "Create new wallet")]
        public async Task<ActionResult> Create(CreateWalletModel model)
        {
            try
            {
                Wallet wallets = await _walletService.Create(model);
                if (wallets == null) return BadRequest();
                string controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = wallets
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
        [SwaggerOperation(Summary = "Update wallet")]
        public async Task<ActionResult> Update(UpdateWalletModel model)
        {
            try
            {
                Wallet wallets = await _walletService.Update(model);
                if (wallets == null) return BadRequest();
                string controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = wallets
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

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete wallet")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await _walletService.Delete(id);
                if (!result) return BadRequest();
                string controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
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
    }
}
