using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Interfaces;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var token = _authService.Login(login.Email, login.Password);
            if (token == null)
                return NotFound("Incorrect email or password.");

            return Ok(token);
        }

        [HttpPost("GenerateClientToken")]
        public IActionResult GenerateClientToken([FromBody] long tableId)
        {
            var token = _authService.GenerateClientToken(tableId);
            if (token == null)
                return NotFound("Table not found.");

            return Ok(token);
        }
    }
}
