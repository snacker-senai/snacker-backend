using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public OrderController(IOrderService orderService, IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService;
        }

        [Authorize(Roles = "Cliente, Garçom")]
        [HttpPost]
        public IActionResult Create([FromHeader] string authorization, [FromBody] CreateOrderDTO dto)
        {
            var tableId = dto.TableId.HasValue ? dto.TableId.Value : long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId"));
            return Execute(() => _orderService.Add<OrderValidator>(dto, tableId).Id);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _orderService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _orderService.GetById(id));
        }

        [HttpGet("GetByBill/{billId}")]
        public IActionResult GetByBill(long billId)
        {
            return Execute(() => _orderService.GetByBill(billId));
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("GetByBill")]
        public IActionResult GetByBill(string authorization)
        {
            return Execute(() => _orderService.GetByBill(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "BillId"))));
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
