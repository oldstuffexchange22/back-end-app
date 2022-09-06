using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Helper;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.User;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Entities.Extentions;
using old_stuff_exchange_v2.Enum.User;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Controllers
{
    public class AuthorizeController : BaseApiController
    {
        private readonly UserService _userService;
        private readonly IJwtHelper _jwtHelper;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthorizeController(UserService userService, IJwtHelper jwtHelper, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Login user name for admin")]
        public async Task<IActionResult> Login(LoginModel model) {
            try
            {
                User user = await _userService.Login(model.UserName, model.Password);
                if(user == null) return NotFound();
                if(user.Status == UserStatus.INACTIVE) return BadRequest("User is inactive");
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = new
                    {
                        token = _jwtHelper.generateJwtToken(user)
                    }
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

        [HttpPost("firebase")]
        public async Task<ActionResult> LoginFirebase(LoginFirebaseModel loginModel)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(loginModel.Token);
                string uid = decodedToken.Uid;
                UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
                string response = _userService.Login(user.Email,user);
                ResponseUserModel userDb = (await _userService.GetByEmail(user.Email)).ToResponseModel();
                if (response == null || response == UserStatus.INACTIVE)
                {
                    return BadRequest(new { message = "Token is invalid or account is blocked" });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = new { token = response, user = userDb }
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
