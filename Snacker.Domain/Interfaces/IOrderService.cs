using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        Order Add<TValidator>(CreateOrderDTO dto, long tableId);
        ICollection<object> GetByBill(long billId);
        ICollection<object> GetByTable(long tableId);
        ICollection<Order> GetEntireOrderByTable(long tableId);
        ICollection<object> GetByStatus(long restaurantId, long statusId);
    }
}
