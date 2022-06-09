using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Building;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class BuildingController : BaseApiController
    {
        private readonly BuildingService _service;
        public BuildingController(BuildingService service) {
            _service = service;
        }
        [HttpGet("list")]
        [SwaggerOperation(Summary = "Get list of building")]
        public IActionResult GetList(Guid? apartmentId, int page = 1, int pageSize = 10 )
        {
            try
            {
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get list building",
                    Data = _service.GetList(apartmentId, page, pageSize)
                });
            }
            catch (Exception ex) { 
                return BadRequest(new { 
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary ="Get building with ID")]
        public async Task<IActionResult> GetBuildingById(Guid id) {
            try
            {
                Building building = await _service.GetById(id);
                if (building == null) return BadRequest();
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
                await _service.Create(building);
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
                Building result = await _service.Update(building);
                if (result == null) return BadRequest();
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

                var result = await _service.Delete(id);
                if (result == false) return BadRequest();
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
