using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Linq;
using System.Net;

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
        private readonly IBaseService<OrderHasProduct> _baseOrderHasProductService;

        public OrderController(IOrderService orderService, IAuthService authService, IBaseService<OrderStatus> baseStatusService, IBaseService<Bill> baseBillService, IBaseService<OrderHasProduct> baseOrderHasProductService)
        {
            _orderService = orderService;
            _authService = authService;
            _baseStatusService = baseStatusService;
            _baseBillService = baseBillService;
            _baseOrderHasProductService = baseOrderHasProductService;
        }

        [Authorize(Roles = "Admin, Cliente, Gestão, Entrega")]
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

        [Authorize(Roles = "Admin, Gestão, Entrega")]
        [HttpGet("ByBill/{billId}")]
        public IActionResult GetByBill(long billId)
        {
            return Execute(() => _orderService.GetByBill(billId));
        }

        [Authorize(Roles = "Admin, Gestão, Entrega")]
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

        [Authorize(Roles = "Admin, Gestão, Entrega, Preparo")]
        [HttpGet("ByStatus/{statusId}")]
        public IActionResult GetByStatus([FromHeader] string authorization, long statusId)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _orderService.GetByStatus(restaurantId, statusId));
        }

        [Authorize(Roles = "Admin, Gestão")]
        [HttpGet("Finished")]
        public IActionResult GetFinishedOrders([FromHeader] string authorization)
        {
            var restaurantId = long.Parse(_authService.GetTokenValue(authorization.Split(" ")[1], "RestaurantId"));

            return Execute(() => _orderService.GetByStatus(restaurantId, 3).Count);
        }

        [Authorize(Roles = "Admin, Gestão, Entrega, Preparo")]
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
                foreach (var orderItem in order.OrderHasProductCollection)
                {
                    if (orderItem.OrderStatusId == statusId - 1)
                    {
                        orderItem.OrderStatusId = statusId;
                    }
                    _baseOrderHasProductService.Update<OrderHasProductValidator>(orderItem);
                }
                _orderService.Update<OrderValidator>(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "Admin, Gestão, Entrega, Preparo")]
        [HttpPut("ChangeItemStatus/{orderHasProductId}/{statusId}")]
        public IActionResult ChangeOrderItemStatus(long orderHasProductId, long statusId)
        {
            try
            {
                var orderHasProduct = (OrderHasProduct)_baseOrderHasProductService.GetById(orderHasProductId);
                var status = (OrderStatus)_baseStatusService.GetById(statusId);
                
                if (orderHasProduct == null || status == null)
                    return NotFound();

                var unfinishedItens = 0;
                foreach (var item in orderHasProduct.Order.OrderHasProductCollection)
                {
                    if (item.OrderStatusId != 3)
                    {
                        unfinishedItens++;
                    }
                }

                if (unfinishedItens <= 1)
                {
                    var order = orderHasProduct.Order;
                    order.OrderStatusId = 3;
                    _orderService.Update<OrderValidator>(order);
                }

                orderHasProduct.OrderStatusId = status.Id;
                _baseOrderHasProductService.Update<OrderHasProductValidator>(orderHasProduct);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "Admin, Gestão, Entrega")]
        [HttpPut("CloseBill/{tableId}")]
        public IActionResult CloseBill(long tableId)
        {
            try
            {
                var orders = _orderService.GetEntireOrderByTable(tableId);
                foreach (var order in orders)
                {
                    order.OrderStatusId = 3;
                    _orderService.Update<OrderValidator>(order);
                }
                var bill = orders.First().Bill;
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
