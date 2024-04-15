using IntegrationServer.Api.Model;
using IntegrationServer.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntegrationServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IntegrationServices _integrationServices;
        private string apiBaseUrl = "";

        public IntegrationController(IntegrationServices integrationServices, IConfiguration configuration)
        {
            _integrationServices = integrationServices;
            _configuration = configuration;
            apiBaseUrl = _configuration.GetValue<string>("UrlSettings:ExampleUrl1");
        }

        [HttpGet("GetApiExample")]
        [Authorize(Roles = "EXAMPLE1URLGET")]
        public async Task<IActionResult> GetApiExample()
        {
            var responseData = await _integrationServices.RequestIntegrationApiAsync(string.Concat(apiBaseUrl, "todos/2"),
                typeof(User), null, Enums.IntegrationMethodsEnums.GetMethod, null);
            return Ok(responseData);
        }

        [HttpPost("PostApiExample")]
        [Authorize(Roles = "EXAMPLE1URLPOST")]
        public async Task<IActionResult> PostApiExample()
        {
            var responseData = await _integrationServices.RequestIntegrationApiAsync(string.Concat(apiBaseUrl, "posts"),
                typeof(User), new User() { completed = true, title = "engineer", userId = 2222 }, Enums.IntegrationMethodsEnums.PostMethod, null);
            return Ok(responseData);
        }
    }
}
