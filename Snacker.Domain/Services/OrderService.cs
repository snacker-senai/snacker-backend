﻿using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Domain.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<OrderHasProduct> _orderHasProductRepository;
        private readonly IBillRepository _billRepository;
        public OrderService(IBaseRepository<Order> orderRepository, IBaseRepository<OrderHasProduct> orderHasProductRepository, IBillRepository billRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _orderHasProductRepository = orderHasProductRepository;
            _billRepository = billRepository;
        }

        public Order Add<TValidator>(CreateOrderDTO dto, long tableId)
        {
            var bill = _billRepository.SelectActiveFromTable(tableId).First();
            var order = new Order
            {
                OrderStatusId = 1,
                TableId = tableId,
                BillId = bill.Id
            };
            _orderRepository.Insert(order);
            foreach (var productWithQuantity in dto.ProductsWithQuantity)
            {
                _orderHasProductRepository.Insert(new OrderHasProduct
                {
                    OrderId = order.Id,
                    ProductId = productWithQuantity.ProductId,
                    Quantity = productWithQuantity.Quantity
                });
            }
            return _orderRepository.Select(order.Id);
        }
    }
}