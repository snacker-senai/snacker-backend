using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        Order Add<TValidator>(CreateOrderDTO dto, long tableId);
    }
}
