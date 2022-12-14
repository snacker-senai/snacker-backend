using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class TableService : BaseService<Table>, ITableService
    {
        private readonly ITableRepository _tableRepository;
        public TableService(ITableRepository tableRepository) : base(tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public ICollection<object> GetFromRestaurant(long restaurantId)
        {
            var itens = _tableRepository.SelectFromRestaurant(restaurantId);
            var result = new List<object>();
            foreach (var item in itens)
            {
                result.Add(item);
            }
            return result;
        }

        public string GetTableNumber(long tableId)
        {
            return _tableRepository.GetTableNumber(tableId);
        }
    }
}
