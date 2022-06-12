using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
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
        private readonly WalletService _service;
        public WalletController(WalletService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Find by id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                Wallet wallet = await _service.FindById(id);
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

        [HttpGet("type/{type}")]
        [SwaggerOperation(Summary = "Find by type (use for system wallet and chairity wallet)")]
        public async Task<ActionResult> GetByType(string type)
        {
            try
            {
                Wallet wallet = await _service.FindByType(type);
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

        [HttpGet("user-wallets/{userId}")]
        [SwaggerOperation(Summary = "Find wallets of user")]
        public async Task<ActionResult> GetByType(Guid userId)
        {
            try
            {
                List<Wallet> wallets = await _service.FindByUserId(userId);
                if (wallets == null) return BadRequest();
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
                Wallet wallets = await _service.Create(model);
                if (wallets == null) return BadRequest();
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
                Wallet wallets = await _service.Update(model);
                if (wallets == null) return BadRequest();
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
                bool result = await _service.Delete(id);
                if (!result) return BadRequest();
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
