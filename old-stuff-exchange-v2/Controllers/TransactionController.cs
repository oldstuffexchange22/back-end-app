using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Attributes;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly TransactionService _transactionService;
        private readonly IAuthorizationService _authorizationService;
        private readonly CacheService _cacheService;
        public TransactionController(TransactionService service, IAuthorizationService authorizationService, CacheService cacheService)
        {
            _transactionService = service;
            _authorizationService = authorizationService;
            _cacheService = cacheService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get transaction by Id")]
        [Cache(100)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Transaction transaction = await _transactionService.GetById(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                else {
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, transaction, Operations.Read)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = transaction
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("user/{userId}")]
        [SwaggerOperation(Summary = "Get transaction by user id")]
        public async Task<IActionResult> GetByUserId(Guid userId, int page = 1, int pageSize = 10)
        {
            try
            {
                List<Transaction> transactions = await _transactionService.GetByUserId(userId, page, pageSize);
                if (transactions == null || transactions.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, transactions[0], Operations.Read)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
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
