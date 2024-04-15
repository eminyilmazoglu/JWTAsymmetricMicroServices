using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // Test...
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpGet("AuthorizedTest")]
        [Authorize]
        public IActionResult AuthorizedTest()
        {
            return Ok();
        }

        [HttpGet("SVTest")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult SVTest()
        {
            return Ok("SVTest");
        }

        [HttpGet("USTest")]
        [Authorize(Roles = "USER")]
        public IActionResult USTest()
        {
            return Ok("USTest");
        }
    }
}
