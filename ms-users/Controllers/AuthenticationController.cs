using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ms_users.Interface;

namespace ms_users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.Login login)
        {
            try
            {
                var _ = _authenticationService.Login(login);

                return Ok(_);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
