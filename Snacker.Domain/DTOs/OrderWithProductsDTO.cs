﻿using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.DTOs
{
    public class OrderWithProductsDTO : BaseDTO
    {
        public OrderWithProductsDTO(Order order)
        {
            Id = order.Id;
            OrderStatus = new OrderStatusDTO
            {
                Id = order.OrderStatus.Id,
                Name = order.OrderStatus.Name
            };
            ProductsWithQuantity = new List<ProductAndQuantityDTO>();
            foreach (var item in order.OrderHasProductCollection)
            {
                ProductsWithQuantity.Add(new ProductAndQuantityDTO(item));
            }
        }
        public OrderStatusDTO OrderStatus { get; set; }
        public ICollection<ProductAndQuantityDTO> ProductsWithQuantity { get; set; }
    }
}