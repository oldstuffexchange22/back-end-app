using Microsoft.AspNetCore.Http;
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
using old_stuff_exchange_v2.Enum;
using System.Collections.Generic;

namespace Old_stuff_exchange.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserService _userService;
        public UserController(UserService service)
        {
            _userService = service;
        }

        [Route("register")]
        [HttpPost]
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
                    Status = UserStatus.ACTIVE
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

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(loginModel.Token);
                string uid = decodedToken.Uid;
                UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

                var response = _userService.Login(user.Email);
                if (response == null)
                {
                    return BadRequest(new { message = "Token is invalid" });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = new { token = response }
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
                bool check = await _userService.Delete(id);
                if (!check)
                {
                    return NotFound();
                }
                return Ok(new ApiResponse { 
                    Success = check,
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
                    Phone = updateUser.Phone
                };
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
        [HttpGet("list")]
        [SwaggerOperation(Summary = "Get information user by email, by roleId and pagination")]
        public async Task<ActionResult> GetList(string email, Guid? roleId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                List<ResponseUserModel> users = await _userService.GetList(email, roleId, pageNumber, pageSize);
                return Ok(new ApiResponse { 
                    Success = true,
                    Data = users
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

        [Route("login-get-token")]
        [HttpGet]
        public IActionResult GetToken(string email) {
            return Ok(_userService.Login(email));
        }
    }
}
