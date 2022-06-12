using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Service;
using System;
using System.Threading.Tasks;

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
        [HttpGet("generate-data")]
        public IActionResult GererateDatabase() {
            try
            {
                _databaseService.GenerateDataWithBogus();
                return Ok();
            }
            catch (Exception ex) { 
                return Ok(ex);
            }
        }

        [HttpGet("remove-data")]
        public IActionResult RemoveDatabase()
        {
            try
            {
                _databaseService.DeleteAllData();
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        /*[HttpGet("generate-data/cateogories")]
        public IActionResult GererateCategories()
        {
            try
            {
                _databaseService.GenerateCategory();
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }*/
    }
}
