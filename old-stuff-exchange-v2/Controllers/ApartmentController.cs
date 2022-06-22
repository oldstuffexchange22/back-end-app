using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using old_stuff_exchange_v2.Model.Apartment;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using old_stuff_exchange_v2.Entities;
using Old_stuff_exchange.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Attributes;

namespace old_stuff_exchange_v2.Controllers
{
    [Authorize(Policy = PolicyName.ADMIN)]
    public class ApartmentController : BaseApiController
    {
        private readonly ApartmentService _service;
        private readonly IAuthorizationService _authorizationService;
        private readonly ResponseCacheService _responseCacheService;
        public ApartmentController(ApartmentService service, IAuthorizationService authorizationService, ResponseCacheService responseCacheService)
        {
            _service = service;
            _authorizationService = authorizationService;
            _responseCacheService = responseCacheService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get apartment by id")]
        [Cache(100)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Apartment apartment = await _service.GetById(id);
                if (apartment == null) return BadRequest();
                return Ok(new ApiResponse { 
                    Success = true,
                    Data = apartment
                });
            }
            catch (Exception ex) {
                return BadRequest(new { 
                    code =StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpGet()]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Getlist apartment")]
        [Cache(100)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Apartment> apartments = await _service.GetList();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = apartments
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
        [SwaggerOperation(Summary = "Create new apartment")]
        public async Task<IActionResult> Create(CreateApartmentModel model)
        {
            try
            {
                Apartment apartment = await _service.Create(model);
                if (apartment == null) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _responseCacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = apartment
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
        [SwaggerOperation(Summary = "Update apartment")]
        public async Task<IActionResult> Update(UpdateApartmentModel model)
        {
            try
            {
                Apartment apartment = await _service.Update(model);
                if (apartment == null) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _responseCacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = apartment
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
        [SwaggerOperation(Summary = "Delete apartment")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await _service.Delete(id);
                if (result) {
                    var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                    await _responseCacheService.RemoveCacheResponseAsync(controllerName);
                }
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

        
    }
}
