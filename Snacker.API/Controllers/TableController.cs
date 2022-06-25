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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] Table table)
        {
            if (table == null)
                return NotFound();

            return Execute(() => _tableService.Add<TableValidator>(table).Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update([FromBody] Table table)
        {
            if (table == null)
                return NotFound();

            return Execute(() => _tableService.Update<TableValidator>(table));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _tableService.Get());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _tableService.GetById(id));
        }

        [Authorize(Roles = "Admin, Gerente")]
        [HttpGet("FromRestaurant")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _tableService.GetFromRestaurant(restaurantId));
        }

        [Authorize(Roles = "Admin, Gerente")]
        [HttpPost("FromRestaurant")]
        public IActionResult CreateFromRestaurant([FromBody] Table table, [FromHeader] string authorization)
        {
            if (table == null)
                return NotFound();

            table.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _tableService.Add<TableValidator>(table));
        }

        [Authorize(Roles = "Admin, Gerente")]
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
