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
        private readonly IBaseService<Bill> _baseBillService;
        private readonly IThemeService _themeService;

        public AuthController(IAuthService authService, IBaseService<Table> baseTableService, IBaseService<Bill> baseBillService, IThemeService themeService)
        {
            _authService = authService;
            _baseTableService = baseTableService;
            _baseBillService = baseBillService;
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

        [Authorize(Roles = "Cliente")]
        [HttpGet("ClientSessionInfo")]
        public IActionResult GetClientSessionInfo([FromHeader] string authorization)
        {
            var tableId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId"));
            var billId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "BillId"));
            var bill = (Bill)_baseBillService.GetById(billId);

            if (!bill.Active)
            {
                return Unauthorized();
            }

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
                    return Ok(new
                    {
                        Role = role,
                        TableId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId")),
                        RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId")),
                        BillId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "BillId")),
                        ThemeId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "ThemeId")),
                        Theme = _themeService.GetById(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "ThemeId"))),
                        Table = _baseTableService.GetById(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId")))
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
