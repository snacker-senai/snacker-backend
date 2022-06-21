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
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly IAuthService _authService;

        public TableController(ITableService tableService, IAuthService authService)
        {
            _tableService = tableService;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Table table)
        {
            if (table == null)
                return NotFound();

            return Execute(() => _tableService.Add<TableValidator>(table).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Table table)
        {
            if (table == null)
                return NotFound();

            return Execute(() => _tableService.Update<TableValidator>(table));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _tableService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _tableService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _tableService.GetById(id));
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet("FromRestaurant")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _tableService.GetFromRestaurant(restaurantId));
        }

        [Authorize(Roles = "Gerente")]
        [HttpPost("FromRestaurant")]
        public IActionResult CreateFromRestaurant([FromBody] Table table, [FromHeader] string authorization)
        {
            if (table == null)
                return NotFound();

            table.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _tableService.Add<TableValidator>(table).Id);
        }

        [Authorize(Roles = "Gerente")]
        [HttpPut("FromRestaurant")]
        public IActionResult UpdateFromRestaurant([FromBody] Table table, [FromHeader] string authorization)
        {
            if (table == null)
                return NotFound();

            table.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _tableService.Update<TableValidator>(table));
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
