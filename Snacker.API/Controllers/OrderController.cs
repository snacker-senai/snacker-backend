using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Linq;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        private readonly IBaseService<OrderStatus> _baseStatusService;
        private readonly IBaseService<Bill> _baseBillService;

        public OrderController(IOrderService orderService, IAuthService authService, IBaseService<OrderStatus> baseStatusService, IBaseService<Bill> baseBillService)
        {
            _orderService = orderService;
            _authService = authService;
            _baseStatusService = baseStatusService;
            _baseBillService = baseBillService;
        }

        [Authorize(Roles = "Admin, Cliente, Gerente, Garçom")]
        [HttpPost]
        public IActionResult Create([FromHeader] string authorization, [FromBody] CreateOrderDTO dto)
        {
            var tableId = dto.TableId.HasValue ? dto.TableId.Value : long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "TableId"));
            return Execute(() => _orderService.Add<OrderValidator>(dto, tableId).Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _orderService.Get());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _orderService.GetById(id));
        }

        [Authorize(Roles = "Admin, Gerente, Garçom")]
        [HttpGet("ByBill/{billId}")]
        public IActionResult GetByBill(long billId)
        {
            return Execute(() => _orderService.GetByBill(billId));
        }

        [Authorize(Roles = "Admin, Gerente, Garçom")]
        [HttpGet("ByTable/{tableId}")]
        public IActionResult GetByTable(long tableId)
        {
            return Execute(() => _orderService.GetByTable(tableId));
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("ByBill")]
        public IActionResult GetByBill([FromHeader] string authorization)
        {
            return Execute(() => _orderService.GetByBill(long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "BillId"))));
        }

        [Authorize(Roles = "Admin, Gerente, Garçom, Cozinheiro")]
        [HttpGet("ByStatus/{statusId}")]
        public IActionResult GetByStatus(long statusId)
        {
            return Execute(() => _orderService.GetByStatus(statusId));
        }

        [Authorize(Roles = "Admin, Gerente, Garçom, Cozinheiro")]
        [HttpPut("ChangeStatus/{orderId}/{statusId}")]
        public IActionResult ChangeOrderStatus(long orderId, long statusId)
        {
            try
            {
                var order = (Order)_orderService.GetById(orderId);
                var status = (OrderStatus)_baseStatusService.GetById(statusId);

                if (order == null || status == null)
                    return NotFound();

                order.OrderStatusId = status.Id;
                _orderService.Update<OrderValidator>(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "Admin, Gerente, Garçom")]
        [HttpPut("CloseBill/{tableId}")]
        public IActionResult CloseBill(long tableId)
        {
            try
            {
                var bill = _orderService.GetEntireOrderByTable(tableId).First().Bill;
                bill.Active = false;
                _baseBillService.Update<BillValidator>(bill);
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
