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
    public class OrderStatusController : ControllerBase
    {
        private readonly IBaseService<OrderStatus> _baseOrderStatusService;

        public OrderStatusController(IBaseService<OrderStatus> baseOrderStatusService)
        {
            _baseOrderStatusService = baseOrderStatusService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] OrderStatus orderStatus)
        {
            if (orderStatus == null)
                return NotFound();

            return Execute(() => _baseOrderStatusService.Add<OrderStatusValidator>(orderStatus).Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update([FromBody] OrderStatus orderStatus)
        {
            if (orderStatus == null)
                return NotFound();

            return Execute(() => _baseOrderStatusService.Update<OrderStatusValidator>(orderStatus));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseOrderStatusService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseOrderStatusService.Get());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseOrderStatusService.GetById(id));
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
