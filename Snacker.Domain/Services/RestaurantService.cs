using FluentValidation;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.Domain.Services
{
    public class RestaurantService : BaseService<Restaurant>
    {
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        public RestaurantService(IBaseRepository<Restaurant> restaurantRepository) : base(restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public override Restaurant Add<TValidator>(Restaurant obj)
        {
            Validate(obj, Activator.CreateInstance<RestaurantValidator>());
            _restaurantRepository.Insert(obj);
            return obj;
        }
    }
}
