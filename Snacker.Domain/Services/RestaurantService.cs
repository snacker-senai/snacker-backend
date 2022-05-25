using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class RestaurantService : BaseService<Restaurant>
    {
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        public RestaurantService(IBaseRepository<Restaurant> restaurantRepository) : base(restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public override ICollection<object> Get()
        {
            var itens = _restaurantRepository.Select();
            var result = new List<object>();
            foreach (var item in itens)
            {
                var dto = new RestaurantDTO(item);
                result.Add(dto);
            }
            return result;
        }

        public override object GetById(long id)
        {
            var item = _restaurantRepository.Select(id);
            return new RestaurantDTO(item);
        }
    }
}
