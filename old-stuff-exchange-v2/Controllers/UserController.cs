﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Old_stuff_exchange.Model.User;
using FirebaseAdmin.Auth;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
using System.Collections.Generic;
using old_stuff_exchange_v2.Enum.User;
using Microsoft.AspNetCore.Authorization;
using old_stuff_exchange_v2.Enum.Authorize;

namespace Old_stuff_exchange.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserService _userService;
        private readonly IAuthorizationService _authorizationService;
        public UserController(UserService service, IAuthorizationService authorizationService)
        {
            _userService = service;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get information user by id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                User user = await _userService.GetById(id);
                if (!(await _authorizationService.AuthorizeAsync(User, user, Operations.Read)).Succeeded)
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = user
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

        [HttpGet("email/{email}")]
        [SwaggerOperation(Summary = "Get information user by email")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            try
            {
                User user = await _userService.GetByEmail(email);
                if (!(await _authorizationService.AuthorizeAsync(User, user, Operations.Read)).Succeeded)
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = user
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
        [SwaggerOperation(Summary = "Get information user by email, by roleId and pagination")]
        [Authorize(Policy =PolicyName.ADMIN)]
        public async Task<ActionResult> GetList(string email, Guid? roleId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                List<ResponseUserModel> users = await _userService.GetList(email, roleId, pageNumber, pageSize);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = users
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

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register new user with role ADMIN")]
        public async Task<IActionResult> Register(RegisterUserModel newUser)
        {
            try
            {
                User user = new User
                {
                    FullName = newUser.FullName,
                    Email = newUser.Email,
                    UserName = newUser.UserName,
                    Password = newUser.Password,
                    Phone = newUser.Phone,
                    Status = UserStatus.ACTIVE,
                    Gender = newUser.Gender,
                };
                var tempUser = await _userService.Create(user);
                if (tempUser == null)
                {
                    return BadRequest(new { message = "Email has exist" });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Register success",
                    Object = user
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

        

        [HttpPost("update-address")]
        [SwaggerOperation(Summary = "Update address user")]
        public async Task<ActionResult> Update(Guid userId, Guid buildingId)
        {
            try
            {
                User userAuthorize = await _userService.GetById(userId);
                if (userAuthorize == null)
                {
                    return BadRequest();
                }
                else {
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, userAuthorize, Operations.Update)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }
                User user = await _userService.UpdateUserAddress(userId, buildingId);
                if (user == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update user success",
                    Data = user
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
        [SwaggerOperation(Summary = "Update user")]
        public async Task<ActionResult> Update(UpdateUserModel updateUser)
        {
            try
            {
                User user = new User
                {
                    Id = updateUser.Id,
                    FullName = updateUser.Name,
                    Email = updateUser.Email,
                    BuildingId = updateUser.BuildingId,
                    ImagesUrl = updateUser.Image,
                    RoleId = updateUser.RoleId,
                    Status = updateUser.Status,
                    Phone = updateUser.Phone,
                    Gender = updateUser.Gender
                };
                // authorize
                bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, user, Operations.Update)).Succeeded;
                if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);

                bool check = await _userService.Update(user);
                if (!check)
                {
                    return BadRequest();
                }
                return Ok(new ApiResponse { 
                    Success = true,
                    Message = "Update user success",
                    Data = user
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
        [SwaggerOperation(Summary = "Delete user by Id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                User userAuthorize = await _userService.GetById(id);
                if (userAuthorize == null)
                {
                    return BadRequest();
                }
                else
                {
                    bool verifyAuth = (await _authorizationService.AuthorizeAsync(User, userAuthorize, Operations.Update)).Succeeded;
                    if (verifyAuth == false) return StatusCode(StatusCodes.Status403Forbidden);
                }

                bool check = await _userService.Delete(id);
                if (!check)
                {
                    return NotFound();
                }
                return Ok(new ApiResponse
                {
                    Success = check,
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

        [Route("login-get-token")]
        [HttpGet]
        public IActionResult GetToken(string email) {
            return Ok(new ApiResponse { 
                Data = _userService.Login(email)
            });
        }
    }
}
