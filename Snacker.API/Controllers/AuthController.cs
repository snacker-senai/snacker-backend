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
        private readonly ITableService _baseTableService;
        private readonly IBillService _billService;
        private readonly IThemeService _themeService;

        public AuthController(IAuthService authService, ITableService baseTableService, IBillService billService, IThemeService themeService)
        {
            _authService = authService;
            _baseTableService = baseTableService;
            _billService = billService;
            _themeService = themeService;
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

        [Authorize(Roles = "Admin, Gestão, Entrega")]
        [HttpPost("GenerateClientToken/{tableId}")]
        public IActionResult GenerateClientToken(long tableId)
        {
            return Execute(() => _authService.GenerateClientToken(tableId));
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
                    var billId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "BillId"));
                    var bill = _billService.GetById(billId);
                    if (!bill.Active)
                    {
                        return Unauthorized();
                    }

                    return Ok(new
                    {
                        Role = role,
                        TableId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId")),
                        RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId")),
                        BillId = billId,
                        ThemeId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "ThemeId")),
                        Theme = _themeService.GetById(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "ThemeId"))),
                        TableNumber = _baseTableService.GetTableNumber(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId")))
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Role = role,
                        Email = _authService.GetTokenValue(authorization.Split(" ")[1], "email"),
                        RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId")),
                        ThemeId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "ThemeId")),
                        Theme = _themeService.GetById(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "ThemeId")))
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
