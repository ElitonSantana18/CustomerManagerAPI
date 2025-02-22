using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Email == "admin@admin.com" && request.Password == "admin")
            {
                var token = _authService.GenerateJwtToken(request.Email);
                return Ok(new { Token = token });
            }
            return Unauthorized("Credenciais inválidas");
        }
    }
}
