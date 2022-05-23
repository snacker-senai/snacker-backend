using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantCategoryController : ControllerBase
    {
        private readonly IBaseService<RestaurantCategory> _baseRestaurantCategoryService;

        public RestaurantCategoryController(IBaseService<RestaurantCategory> baseRestaurantCategoryService)
        {
            _baseRestaurantCategoryService = baseRestaurantCategoryService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RestaurantCategory restaurantCategory)
        {
            if (restaurantCategory == null)
                return NotFound();

            return Execute(() => _baseRestaurantCategoryService.Add<RestaurantCategoryValidator>(restaurantCategory).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] RestaurantCategory restaurantCategory)
        {
            if (restaurantCategory == null)
                return NotFound();

            return Execute(() => _baseRestaurantCategoryService.Update<RestaurantCategoryValidator>(restaurantCategory));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseRestaurantCategoryService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseRestaurantCategoryService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseRestaurantCategoryService.GetById(id));
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
