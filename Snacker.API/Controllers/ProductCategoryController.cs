using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCategory productCategory)
        {
            if (productCategory == null)
                return NotFound();

            return Execute(() => _productCategoryService.Add<ProductCategoryValidator>(productCategory).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductCategory productCategory)
        {
            if (productCategory == null)
                return NotFound();

            return Execute(() => _productCategoryService.Update<ProductCategoryValidator>(productCategory));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _productCategoryService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _productCategoryService.Get());
        }

        [HttpGet("WithProducts")]
        public IActionResult GetWithProducts()
        {
            return Execute(() => _productCategoryService.GetWithProducts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _productCategoryService.GetById(id));
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
