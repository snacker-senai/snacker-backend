using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;

namespace Snacker.Domain.Services
{
    public class BillService : BaseService<Bill>
    {
        private readonly IBaseRepository<Bill> _billRepository;
        public BillService(IBaseRepository<Bill> billRepository) : base(billRepository)
        {
            _billRepository = billRepository;
        }

        public override object GetById(long id)
        {
            return new BillDTO(_billRepository.Select(id));
        }
    }
}
