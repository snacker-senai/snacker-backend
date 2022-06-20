using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Linq;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IBaseService<Restaurant> _baseRestaurantService;
        private readonly IBaseService<Address> _baseAddressService;
        private readonly IUserService _userService;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public RestaurantController(IBaseService<Restaurant> baseRestaurantService, IBaseService<Address> baseAddressService, IUserService userService)
        {
            _baseRestaurantService = baseRestaurantService;
            _baseAddressService = baseAddressService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRestaurantDTO dto)
        {
            try
            {
                if (dto.User == null || dto.Address == null || dto.Person == null)
                    return NotFound();

                var address = _baseAddressService.Add<AddressValidator>(new Address
                {
                    CEP = dto.Address.CEP,
                    City = dto.Address.City,
                    Country = dto.Address.Country,
                    District = dto.Address.District,
                    Number = dto.Address.Number,
                    State = dto.Address.State,
                    Street = dto.Address.Street
                });
                var restaurantId = _baseRestaurantService.Add<RestaurantValidator>(new Restaurant
                {
                    Active = dto.Active,
                    AddressId = address.Id,
                    Address = address,
                    Description = dto.Description,
                    Name = dto.Name,
                    RestaurantCategoryId = dto.RestaurantCategoryId,
                }).Id;

                var random = new Random();
                var generatedPassword = new string(
                    Enumerable.Repeat(chars, 7)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                var user = _userService.Add<UserValidator>(new User
                {
                     Email = dto.User.Email,
                     UserTypeId = 34,
                     Person = new Person
                     {
                         RestaurantId = restaurantId,
                         BirthDate = dto.Person.BirthDate,
                         Document = dto.Person.Document,
                         Name = dto.Person.Name,
                         Phone = dto.Person.Phone,
                     },
                     Password = generatedPassword,
                     ChangePassword = true
                });

                return Ok(_userService.GetById(user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        public IActionResult Update([FromBody] Restaurant restaurant)
        {
            try
            {
                if (restaurant == null || restaurant.Address == null)
                    return NotFound();

                _baseAddressService.Update<AddressValidator>(restaurant.Address);
                var updatedRestaurant = _baseRestaurantService.Update<RestaurantValidator>(restaurant);
                return Ok(_baseRestaurantService.GetById(updatedRestaurant.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseRestaurantService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseRestaurantService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseRestaurantService.GetById(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
