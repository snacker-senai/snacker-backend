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
        private readonly IBaseService<Product> _baseProductService;

        public ProductController(IBaseService<Product> baseProductService)
        {
            _baseProductService = baseProductService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null)
                return NotFound();

            return Execute(() => _baseProductService.Add<ProductValidator>(product).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            if (product == null)
                return NotFound();

            return Execute(() => _baseProductService.Update<ProductValidator>(product));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseProductService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseProductService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseProductService.GetById(id));
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
