using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
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

        public DepositController(DepositService depositService)
        {
            _depositService = depositService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get deposit by Id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Deposit deposit = await _depositService.GetById(id);
                if (deposit == null) return BadRequest();
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

        [HttpGet("user-deposits/{userId}")]
        [SwaggerOperation(Summary = "Get deposit by user id")]
        public async Task<IActionResult> GetByUserId(Guid userId, int page = 1, int pageSize = 10)
        {
            try
            {
                List<Deposit> deposits = await _depositService.GetListByUserId(userId, page, pageSize);
                if (deposits == null) return BadRequest();
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
