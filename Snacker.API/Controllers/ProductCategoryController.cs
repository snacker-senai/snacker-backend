using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Linq;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IProductService productService, IAuthService authService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _authService = authService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] ProductCategory productCategory)
        {
            if (productCategory == null)
                return NotFound();

            return Execute(() => _productCategoryService.Add<ProductCategoryValidator>(productCategory).Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update([FromBody] ProductCategory productCategory)
        {
            if (productCategory == null)
                return NotFound();

            return Execute(() => _productCategoryService.Update<ProductCategoryValidator>(productCategory));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _productCategoryService.Get());
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("WithProducts")]
        public IActionResult GetWithProducts([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _productCategoryService.GetWithProducts(restaurantId));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _productCategoryService.GetById(id));
        }

        [Authorize(Roles = "Admin, Gerente")]
        [HttpGet("FromRestaurant")]
        public IActionResult GetFromRestaurant([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _productCategoryService.GetFromRestaurant(restaurantId));
        }

        [Authorize(Roles = "Admin, Gerente")]
        [HttpPost("FromRestaurant")]
        public IActionResult CreateFromRestaurant([FromBody] ProductCategory productCategory, [FromHeader] string authorization)
        {
            if (productCategory == null)
                return NotFound();

            productCategory.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _productCategoryService.Add<ProductCategoryValidator>(productCategory));
        }

        [Authorize(Roles = "Admin, Gerente")]
        [HttpPut("FromRestaurant")]
        public IActionResult UpdateFromRestaurant([FromBody] ProductCategory productCategory, [FromHeader] string authorization)
        {
            try
            {
                if (productCategory == null)
                    return NotFound();

                productCategory.RestaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

                if (!productCategory.Active)
                {
                    var productCategoryWithProducts = _productCategoryService.GetWithProducts(productCategory.RestaurantId).Where(p => p.Id == productCategory.Id).FirstOrDefault();
                    if (productCategoryWithProducts != null && productCategoryWithProducts.Products != null)
                    {
                        foreach (var product in productCategoryWithProducts.Products)
                        {
                            _productService.Update<ProductValidator>(new Product
                            {
                                Id = product.Id,
                                Active = productCategory.Active,
                                Description = product.Description,
                                Image = product.Image,
                                Name = product.Name,
                                Price = product.Price,
                                ProductCategoryId = productCategory.Id,
                                RestaurantId = productCategory.RestaurantId
                            });
                        }
                    }
                }
                _productCategoryService.Update<ProductCategoryValidator>(productCategory);
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
