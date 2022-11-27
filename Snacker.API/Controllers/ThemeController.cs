using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeService;
        private readonly IAuthService _authService;

        public ThemeController(IThemeService themeService, IAuthService authService)
        {
            _themeService = themeService;
            _authService = authService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] Theme theme)
        {
            if (theme == null)
                return NotFound();

            return Execute(() => _themeService.Add<ThemeValidator>(theme).Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update([FromBody] Theme theme)
        {
            if (theme == null)
                return NotFound();

            return Execute(() => _themeService.Update<ThemeValidator>(theme));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _themeService.Get());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _themeService.GetById(id));
        }

        [Authorize()]
        [HttpGet("FromRestaurant")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _themeService.GetFromRestaurant(restaurantId));
        }

        [Authorize()]
        [HttpPost("FromRestaurant")]
        public IActionResult CreateFromRestaurant([FromBody] Theme theme, [FromHeader] string authorization)
        {
            if (theme == null)
                return NotFound();

            theme.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _themeService.Add<ThemeValidator>(theme));
        }

        [Authorize()]
        [HttpPut("FromRestaurant")]
        public IActionResult UpdateFromRestaurant([FromBody] Theme theme, [FromHeader] string authorization)
        {
            if (theme == null)
                return NotFound();

            theme.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _themeService.Update<ThemeValidator>(theme));
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
