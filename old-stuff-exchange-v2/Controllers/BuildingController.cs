using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Building;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Attributes;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Model.Building;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    [Authorize(Policy = PolicyName.ADMIN)]
    public class BuildingController : BaseApiController
    {
        private readonly BuildingService _buildingService;
        // private readonly CacheService _cacheService;
        
        public BuildingController(BuildingService service) {
            _buildingService = service;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get building with ID")]
        // [Cache(1)]
        public async Task<IActionResult> GetBuildingById(Guid id)
        {
            try
            {
                Building building = await _buildingService.GetById(id);
                if (building == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = building
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
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get list of building")]
        // [Cache(1)]
        public async Task<IActionResult> GetList(Guid? apartmentId,string name, int page = 1, int pageSize = 10 )
        {
            try
            {
                List<ResponseBuildingModel> buildings = await _buildingService.GetList(apartmentId, name, page, pageSize);
                if (buildings.Count == 0) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get list building",
                    Data = buildings
                });
            }
            catch (Exception ex) { 
                return BadRequest(new { 
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new building")]
        public async Task<IActionResult> Create(CreateBuildingModel newBuilding) {
            try
            {
                Building building = new Building
                {
                    Name = newBuilding.Name,
                    NumberFloor = newBuilding.NumberFloor,
                    NumberRoom = newBuilding.NumberRoom,
                    ApartmentId = newBuilding.ApartmentId,
                    Description = newBuilding.Description,
                };
                await _buildingService.Create(building);
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                // await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = building
                });
            }
            catch (Exception ex) {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpPut()]
        [SwaggerOperation(Summary = "Update building")]
        public async Task<IActionResult> Update(UpdateBuildingModel buildingModel) {
            try
            {
                Building building = new Building
                {
                    Id = buildingModel.Id,
                    Name = buildingModel.Name,
                    NumberFloor = buildingModel.NumberFloor,
                    NumberRoom = buildingModel.NumberRoom,
                    Description = buildingModel.Description,
                    ApartmentId = buildingModel.ApartmentId,
                };
                Building result = await _buildingService.Update(building);
                if (result == null) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                // await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex) {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete building")]
        public async Task<IActionResult> Delete(Guid id) {
            try
            {
                if (id == Guid.Empty) return BadRequest();

                var result = await _buildingService.Delete(id);
                if (result == false) return BadRequest();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                // await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Delete success"
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
    }
}
