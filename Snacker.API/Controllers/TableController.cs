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
        private readonly IBaseService<Table> _baseTableService;

        public TableController(IBaseService<Table> baseTableService)
        {
            _baseTableService = baseTableService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Table table)
        {
            if (table == null)
                return NotFound();

            return Execute(() => _baseTableService.Add<TableValidator>(table).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Table table)
        {
            if (table == null)
                return NotFound();

            return Execute(() => _baseTableService.Update<TableValidator>(table));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseTableService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseTableService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseTableService.GetById(id));
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
