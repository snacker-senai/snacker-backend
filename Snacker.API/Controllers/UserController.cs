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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IBaseService<Person> _personService;

        public UserController(IUserService userService, IAuthService authService, IBaseService<Person> personService)
        {
            _userService = userService;
            _authService = authService;
            _personService = personService;
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

        [Authorize(Roles = "Gerente")]
        [HttpGet("FromRestaurant")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _userService.GetFromRestaurant(restaurantId));
        }

        [Authorize(Roles = "Gerente")]
        [HttpPost("FromRestaurant")]
        public IActionResult CreateFromRestaurant([FromBody] User user, [FromHeader] string authorization)
        {
            if (user == null)
                return NotFound();

            user.Person.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _userService.Add<UserValidator>(user).Id);
        }

        [Authorize(Roles = "Gerente")]
        [HttpPut("FromRestaurant")]
        public IActionResult UpdateFromRestaurant([FromBody] User user, [FromHeader] string authorization)
        {
            try
            {
                if (user == null)
                    return NotFound();

                user.Person.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));
                _userService.Update<UserValidator>(user);
                _personService.Update<PersonValidator>(user.Person);
                return Ok();
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
