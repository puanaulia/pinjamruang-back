using Microsoft.AspNetCore.Mvc;

namespace PeminjamRuangAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("OK");
    }
}