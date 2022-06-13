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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        public ProductController(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null)
                return NotFound();

            return Execute(() => _productService.Add<ProductValidator>(product).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            if (product == null)
                return NotFound();

            return Execute(() => _productService.Update<ProductValidator>(product));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _productService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _productService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _productService.GetById(id));
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet("FromRestaurant")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _productService.GetFromRestaurant(restaurantId));
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet("TopSelling")]
        public IActionResult GetTopSelling([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _productService.GetTopSelling(restaurantId));
        }

        [Authorize(Roles = "Gerente")]
        [HttpPost("FromRestaurant")]
        public IActionResult CreateFromRestaurant([FromBody] Product product, [FromHeader] string authorization)
        {
            if (product == null)
                return NotFound();

            product.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _productService.Add<ProductValidator>(product));
        }

        [Authorize(Roles = "Gerente")]
        [HttpPut("FromRestaurant")]
        public IActionResult UpdateFromRestaurant([FromBody] Product product, [FromHeader] string authorization)
        {
            if (product == null)
                return NotFound();

            product.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _productService.Update<ProductValidator>(product));
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
