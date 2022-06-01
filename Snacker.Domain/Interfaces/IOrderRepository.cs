using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        ICollection<Order> SelectByBill(long billId);
    }
}
