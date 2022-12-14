using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;

namespace Snacker.Domain.Services
{
    public class BillService : BaseService<Bill>, IBillService
    {
        private readonly IBaseRepository<Bill> _billRepository;
        public BillService(IBaseRepository<Bill> billRepository) : base(billRepository)
        {
            _billRepository = billRepository;
        }

        public BillDTO GetById(long billId)
        {
            return new BillDTO(_billRepository.Select(billId));
        }
    }
}
