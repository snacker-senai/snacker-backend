﻿using Microsoft.EntityFrameworkCore;
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
            return _mySqlContext.Set<Order>().Include(p => p.Bill).Include(p => p.Bill.Table).Include(p => p.OrderStatus).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.Product).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.OrderStatus).Where(p => p.BillId == billId).ToList();
        }

        public ICollection<Order> SelectByTable(long tableId)
        {
            return _mySqlContext.Set<Order>().Include(p => p.Bill).Include(p => p.Bill.Table).Include(p => p.OrderStatus).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.Product).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.OrderStatus).Where(p => p.Bill.Active && p.Bill.TableId == tableId).ToList();
        }

        public ICollection<Order> SelectByStatus(long restaurantId, long statusId)
        {
            return _mySqlContext.Set<Order>().Include(p => p.Bill).Include(p => p.Bill.Table).Include(p => p.OrderStatus).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.Product).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.OrderStatus).Where(p => p.Table.RestaurantId == restaurantId).ToList();
        }

        public override Order Select(long id)
        {
            return _mySqlContext.Set<Order>().Include(p => p.Bill).Include(p => p.Bill.Table).Include(p => p.OrderStatus).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.Product).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.OrderStatus).Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
