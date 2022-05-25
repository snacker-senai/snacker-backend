using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
                return NotFound();

            return Execute(() => _userService.Add<UserValidator>(user).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            if (user == null)
                return NotFound();

            return Execute(() => _userService.Update<UserValidator>(user));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _userService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _userService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _userService.GetById(id));
        }

        [Authorize]
        [HttpGet("FromRestaurant")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_userService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _userService.GetFromRestaurant(restaurantId));
        }

        [Authorize]
        [HttpGet("FromRestaurant/{id}")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization, long id)
        {
            if (id == 0)
                return NotFound();

            var restaurantId = long.Parse(_userService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _userService.GetFromRestaurantById(restaurantId, id));
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var token = _userService.Login(login.Email, login.Password);
            if (token == null)
                return NotFound("Incorrect email or password.");

            return Ok(token);
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
