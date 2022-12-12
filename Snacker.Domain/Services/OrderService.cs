using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Domain.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBaseRepository<OrderHasProduct> _orderHasProductRepository;
        private readonly IBillRepository _billRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IBaseRepository<OrderHasProduct> orderHasProductRepository, IBillRepository billRepository, IProductRepository productRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _orderHasProductRepository = orderHasProductRepository;
            _billRepository = billRepository;
            _productRepository = productRepository;
        }

        public Order Add<TValidator>(CreateOrderDTO dto, long tableId)
        {
            var bill = _billRepository.SelectActiveFromTable(tableId).First();
            var order = new Order
            {
                OrderStatusId = 1,
                TableId = tableId,
                BillId = bill.Id,
                CreatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"))
            };
            _orderRepository.Insert(order);
            foreach (var productWithQuantity in dto.ProductsWithQuantity)
            {
                var orderHasProduct = new OrderHasProduct
                {
                    OrderId = order.Id,
                    ProductId = productWithQuantity.ProductId,
                    Quantity = productWithQuantity.Quantity,
                    Details = productWithQuantity.Details,
                };
                var product = _productRepository.Select(productWithQuantity.ProductId);
                if (!product.PreReady) orderHasProduct.OrderStatusId = 1;
                else orderHasProduct.OrderStatusId = 2;

                _orderHasProductRepository.Insert(orderHasProduct);
            }
            return _orderRepository.Select(order.Id);
        }

        public ICollection<object> GetByBill(long billId)
        {
            var itens = _orderRepository.SelectByBill(billId);
            var result = new List<object>();
            foreach (var item in itens)
            {
                if (item.OrderHasProductCollection.Any())
                {
                    var dto = new OrderWithProductsDTO(item);
                    result.Add(dto);
                }
            }
            return result;
        }

        public ICollection<object> GetByTable(long tableId)
        {
            var itens = _orderRepository.SelectByTable(tableId);
            var result = new List<object>();
            foreach (var item in itens)
            {
                if (item.OrderHasProductCollection.Any())
                {
                    var dto = new OrderWithProductsDTO(item);
                    result.Add(dto);
                }
            }
            return result;
        }

        public ICollection<Order> GetEntireOrderByTable(long tableId)
        {
            return _orderRepository.SelectByTable(tableId);
        }

        public ICollection<object> GetByStatus(long restaurantId, long statusId)
        {
            var orders = _orderRepository.SelectByStatus(restaurantId, statusId);
            var result = new List<object>();
            foreach (var order in orders)
            {
                if (order.OrderHasProductCollection.Any())
                {
                    foreach (var orderHasProduct in order.OrderHasProductCollection)
                    {
                        if (orderHasProduct.OrderStatusId != statusId)
                        {
                            order.OrderHasProductCollection.Remove(orderHasProduct);
                        }
                    }
                    if (order.OrderHasProductCollection.Any())
                    {
                        result.Add(new OrderWithProductsDTO(order));
                    }
                }
            }
            return result;
        }
    }
}
