using Microsoft.AspNetCore.Mvc;

namespace FinanceBackend.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API working");
        }
    }
}