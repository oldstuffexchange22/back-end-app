using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Role;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Attributes;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    [Authorize(Policy = PolicyName.ADMIN)]
    public class RoleController : BaseApiController
    {
        private readonly RoleService _roleService;
        private readonly ResponseCacheService _cacheService;
        public RoleController(RoleService service, ResponseCacheService cacheService)
        {
            _roleService = service;
            _cacheService = cacheService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get role by Id")]
        [Cache(100)]
        public async Task<IActionResult> GetById(Guid id)
        {
            Role role = await _roleService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse
            {
                Data = role
            });
        }
        [HttpGet()]
        [SwaggerOperation(Summary = "Get list role")]
        [Cache(100)]
        public async Task<IActionResult> GetList()
        {
            List<Role> listRoles = await _roleService.GetList();

            return Ok(new ApiResponse
            {
                Data = listRoles
            });
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new role")]
        public async Task<IActionResult> Create(CreateRoleModel newRole)
        {
            try
            {
                Role role = new Role
                {
                    Name = newRole.Name,
                    Description = newRole.Description
                };
                await _roleService.Create(role);
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Create role success",
                    Data = role
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
        [SwaggerOperation(Summary = "Update role")]
        public async Task<IActionResult> Update(UpdateRoleModel updateRoleModel)
        {
            try
            {
                Role updateRole = new Role
                {
                    Id = updateRoleModel.Id,
                    Name = updateRoleModel.Name
                };
                bool check = await _roleService.Update(updateRole);
                if (!check)
                {
                    return NotFound();
                }
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update role success",
                    Data = updateRole
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
        [SwaggerOperation(Summary = "Delete role by Id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                bool check = await _roleService.Delete(id);
                if (!check)
                {
                    return NotFound();
                }
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                await _cacheService.RemoveCacheResponseAsync(controllerName);
                return NoContent();
            }
            catch (Exception ex) {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }
    }
}
