using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet("get")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("Hi there!"); // Giriş yapmadan herkese açık...
        }

        [HttpGet("Profile")]
        [Authorize]
        public IActionResult UserProfile()
        {
            return Ok("You are Authorized"); // Rol bağımsız giriş yapan user...
        }
    }
}
