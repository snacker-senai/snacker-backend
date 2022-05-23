using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IBaseService<Restaurant> _baseRestaurantService;

        public RestaurantController(IBaseService<Restaurant> baseRestaurantService)
        {
            _baseRestaurantService = baseRestaurantService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
                return NotFound();

            return Execute(() => _baseRestaurantService.Add<RestaurantValidator>(restaurant).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
                return NotFound();

            return Execute(() => _baseRestaurantService.Update<RestaurantValidator>(restaurant));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseRestaurantService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseRestaurantService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseRestaurantService.GetById(id));
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
