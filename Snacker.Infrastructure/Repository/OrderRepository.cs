using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public ICollection<Order> SelectByBill(long billId)
        {
            return _mySqlContext.Set<Order>().Include(p => p.OrderStatus).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.Product).Where(p => p.BillId == billId).ToList();
        }
        public ICollection<Order> SelectByStatus(long statusId)
        {
            return _mySqlContext.Set<Order>().Include(p => p.OrderStatus).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.Product).Where(p => p.OrderStatusId == statusId).ToList();
        }
    }
}
