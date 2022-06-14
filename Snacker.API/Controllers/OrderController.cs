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
        private readonly IBaseService<OrderStatus> _baseStatusService;

        public OrderController(IOrderService orderService, IAuthService authService, IBaseService<OrderStatus> baseStatusService)
        {
            _orderService = orderService;
            _authService = authService;
            _baseStatusService = baseStatusService;
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

        [HttpGet("ByBill/{billId}")]
        public IActionResult GetByBill(long billId)
        {
            return Execute(() => _orderService.GetByBill(billId));
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("ByBill")]
        public IActionResult GetByBill([FromHeader] string authorization)
        {
            return Execute(() => _orderService.GetByBill(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "BillId"))));
        }

        [Authorize(Roles = "Gerente, Garçom, Cozinheiro")]
        [HttpGet("ByStatus/{statusId}")]
        public IActionResult GetByStatus(long statusId)
        {
            return Execute(() => _orderService.GetByStatus(statusId));
        }

        [Authorize(Roles = "Gerente")]
        [HttpPut("ChangeStatus/{id}")]
        public IActionResult UpdateFromRestaurant(long orderId, [FromBody] long statusId)
        {
            var order = (Order)_orderService.GetById(orderId);
            var status = (OrderStatus)_baseStatusService.GetById(statusId);

            if (order == null || status == null)
                return NotFound();

            order.OrderStatusId = status.Id;

            return Execute(() => _orderService.Update<OrderValidator>(order));
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
