using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IBaseService<Table> _baseTableService;

        public AuthController(IAuthService authService, IBaseService<Table> baseTableService)
        {
            _authService = authService;
            _baseTableService = baseTableService;
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

        [Authorize(Roles = "Cliente")]
        [HttpGet("ClientSessionInfo")]
        public IActionResult GetClientSessionInfo([FromHeader] string authorization)
        {
            var tableId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId"));

            return Execute(() => _baseTableService.GetById(tableId));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
