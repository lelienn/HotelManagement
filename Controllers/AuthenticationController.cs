using HotelManagement.Interfaces;
using HotelManagement.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    //[Route("api/[controller]/[Action]")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _config;
        private IAuthenticationnService authenticationService;

        public AuthenticationController(IConfiguration config, IAuthenticationnService authenticationService)
        {
            _config = config;
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestModel request)
        {
            IActionResult response = Unauthorized();
            var tokenString = authenticationService.Login(request);
            if (!tokenString.Status)
                return response;

            return Ok(tokenString);
        }
    }
}