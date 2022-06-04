using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.Role;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly RoleService _service;
        public RoleController(RoleService service)
        {
            _service = service;
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
                await _service.Create(role);
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
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update role")]
        public async Task<IActionResult> Update(Guid id, UpdateRoleModel updateRoleModel)
        {
            try
            {
                if (id != updateRoleModel.Id)
                {
                    return BadRequest();
                }
                Role updateRole = new Role
                {
                    Id = updateRoleModel.Id,
                    Name = updateRoleModel.Name
                };
                bool check = await _service.Update(updateRole);
                if (!check)
                {
                    return NotFound();
                }
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
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get role by Id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Role role = await _service.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse { 
                Data = role
            });
        }
        [HttpGet("list")]
        [SwaggerOperation(Summary = "Get list role")]
        public async Task<IActionResult> GetList()
        {
            List<Role> listRoles = await _service.GetList();

            return Ok(new ApiResponse
            {
                Data = listRoles
            }) ;
        }
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete role by Id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                bool check = await _service.Delete(id);
                if (!check)
                {
                    return NotFound();
                }
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
