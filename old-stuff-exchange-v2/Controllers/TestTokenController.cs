using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Old_stuff_exchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTokenController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "[Authorize] Verify token")]
        public IActionResult GetData() {
            return Ok("Get data success");
        }

        [HttpGet("demo")]
        public IActionResult GetDataDemo()
        {
            return BadRequest(new{ Error = StatusCodes.Status300MultipleChoices});
        }

    }
}
