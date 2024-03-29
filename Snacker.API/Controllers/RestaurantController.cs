﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IBaseService<Restaurant> _baseRestaurantService;
        private readonly IBaseService<Address> _baseAddressService;
        private readonly IUserService _userService;
        private readonly IThemeService _themeService;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public RestaurantController(IBaseService<Restaurant> baseRestaurantService, IBaseService<Address> baseAddressService, IUserService userService, IThemeService themeService)
        {
            _baseRestaurantService = baseRestaurantService;
            _baseAddressService = baseAddressService;
            _userService = userService;
            _themeService = themeService;
        }

        [Authorize(Roles = "Admin")]
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
                _themeService.Add<ThemeValidator>(new Theme 
                {
                    Color = "#004b42",
                    FontColor = "#FFFFFF",
                    Icon = string.Empty,
                    SecondaryColor = "#145c53",
                    SecondaryFontColor = "#FFFFFF",
                    TertiaryFontColor = "#E5E5E5",
                    RestaurantId = restaurantId
                });

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

                SmtpClient client = new();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("contato.snacker@gmail.com", Environment.GetEnvironmentVariable("SMTP_PASSWORD"));
                client.Send("snacker.contato@gmail.com", user.Email, "Snacker - Nova Conta", $"Olá, {user.Person.Name}. A senha de acesso para sua conta Snacker é {user.Password}");

                return Ok(_userService.GetById(user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseRestaurantService.Get());
        }

        [Authorize(Roles = "Admin")]
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
