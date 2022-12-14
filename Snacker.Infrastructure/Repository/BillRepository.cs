using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public ICollection<Bill> SelectActiveFromTable(long tableId)
        {
            return _mySqlContext.Set<Bill>().Where(p => p.Active).Where(p => p.TableId == tableId).ToList();
        }
    }
}
