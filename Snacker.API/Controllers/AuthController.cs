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


        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            var token = _authService.ChangePassword(changePassword.Email, changePassword.NewPassword, changePassword.OldPassword);
            if (token == null)
                return NotFound("Incorrect email or password.");

            return Ok(token);
        }

        [Authorize(Roles = "Admin, Gerente, Garçom")]
        [HttpPost("GenerateClientToken")]
        public IActionResult GenerateClientToken([FromBody] long tableId)
        {
            return Execute(() => _authService.GenerateClientToken(tableId));
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("ClientSessionInfo")]
        public IActionResult GetClientSessionInfo([FromHeader] string authorization)
        {
            var tableId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId"));

            return Execute(() => _baseTableService.GetById(tableId));
        }

        [Authorize]
        [HttpGet("TokenClaims")]
        public IActionResult GetTokenClaims([FromHeader] string authorization)
        {
            try
            {
                var role = _authService.GetTokenValue(authorization.Split(" ")[1], "role");
                if (role == "Cliente")
                {
                    return Ok (new
                    {
                        Role = role,
                        TableId = _authService.GetTokenValue(authorization.Split(" ")[1], "TableId"),
                        RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId")),
                        BillId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "BillId"))
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Role = role,
                        Email = _authService.GetTokenValue(authorization.Split(" ")[1], "email"),
                        RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"))
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
