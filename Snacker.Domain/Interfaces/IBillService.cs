using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IBillService : IBaseService<Bill>
    {
        new BillDTO GetById(long billId);
    }
}
