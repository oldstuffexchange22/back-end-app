using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly TransactionService _service;
        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get transaction by Id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Transaction deposit = await _service.GetById(id);
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

        [HttpGet("user-transactions/{userId}")]
        [SwaggerOperation(Summary = "Get transaction by user id")]
        public async Task<IActionResult> GetByUserId(Guid userId, int page = 1, int pageSize = 10)
        {
            try
            {
                List<Transaction> transactions = await _service.GetByUserId(userId, page, pageSize);
                if (transactions == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = transactions
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
