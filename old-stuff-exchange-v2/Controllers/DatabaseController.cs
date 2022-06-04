using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Service;

namespace Old_stuff_exchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        DatabaseService _databaseService;
        public DatabaseController(DatabaseService service) { 
            _databaseService = service;
        }
        [HttpGet]
        public IActionResult GererateDatabase() {
            _databaseService.GererateData();
            return Ok();
        }
    }
}
