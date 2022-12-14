using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;

namespace Snacker.Domain.Services
{
    public class BillService : BaseService<Bill>, IBillService
    {
        private readonly IBillRepository _billRepository;
        public BillService(IBillRepository billRepository) : base(billRepository)
        {
            _billRepository = billRepository;
        }

        BillDTO IBillService.GetById(long billId)
        {
            return new BillDTO(_billRepository.SelectByIdWithoutRelationships(billId));
        }
    }
}
