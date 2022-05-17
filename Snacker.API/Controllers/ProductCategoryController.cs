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
        private readonly IBaseService<ProductCategory> _baseProductCategoryService;

        public ProductCategoryController(IBaseService<ProductCategory> baseProductCategoryService)
        {
            _baseProductCategoryService = baseProductCategoryService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCategory productCategory)
        {
            if (productCategory == null)
                return NotFound();

            return Execute(() => _baseProductCategoryService.Add<ProductCategoryValidator>(productCategory).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductCategory productCategory)
        {
            if (productCategory == null)
                return NotFound();

            return Execute(() => _baseProductCategoryService.Update<ProductCategoryValidator>(productCategory));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseProductCategoryService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseProductCategoryService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseProductCategoryService.GetById(id));
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
