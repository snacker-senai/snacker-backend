using FluentValidation;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class UserService : BaseService<User>
    {
        private readonly IBaseRepository<User> _userRepository;
        public UserService(IBaseRepository<User> userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override User Add<TValidator>(User obj)
        {
            Validate(obj, Activator.CreateInstance<UserValidator>());
            _userRepository.Insert(obj);
            return obj;
        }


        public override ICollection<object> Get()
        {
            var itens = _userRepository.Select();
            var result = new List<object>();
            foreach (var item in itens)
            {
                var dto = new UserDTO()
                {
                    Email = item.Email,
                    Password = item.Password,
                    Person = new PersonDTO()
                    {
                        Name = item.Person.Name,
                        BirthDate = item.Person.BirthDate,
                        Phone = item.Person.Phone,
                        Document = item.Person.Document,
                        Address = new AddressDTO()
                        {
                            CEP = item.Person.Address.CEP,
                            City = item.Person.Address.City,
                            Country = item.Person.Address.Country,
                            District = item.Person.Address.District,
                            Number = item.Person.Address.Number,
                            Street = item.Person.Address.Street
                        },
                        AddressId = item.Person.AddressId,
                        Restaurant = new RestaurantDTO()
                        {
                            Name = item.Person.Restaurant.Name,
                            Description = item.Person.Restaurant.Description,
                            Address = new AddressDTO()
                            {
                                CEP = item.Person.Restaurant.Address.CEP,
                                City = item.Person.Restaurant.Address.City,
                                Country = item.Person.Restaurant.Address.Country,
                                District = item.Person.Restaurant.Address.District,
                                Number = item.Person.Restaurant.Address.Number,
                                Street = item.Person.Restaurant.Address.Street
                            },
                            AddressId = item.Person.Restaurant.AddressId,
                            RestaurantCategory = new RestaurantCategoryDTO()
                            {
                                Name = item.Person.Restaurant.RestaurantCategory.Name
                            },
                            RestaurantCategoryId = item.Person.Restaurant.RestaurantCategoryId
                        },
                        RestaurantId = item.Person.RestaurantId
                    },
                    PersonId = item.PersonId,
                    UserType = new UserTypeDTO()
                    {
                        Name = item.UserType.Name
                    },
                    UserTypeId = item.UserTypeId
                };
                result.Add(dto);
            }
            return result;
        }

        public override object GetById(long id)
        {
            var item = _userRepository.Select(id);
            return new UserDTO()
            {
                Email = item.Email,
                Password = item.Password,
                Person = new PersonDTO()
                {
                    Name = item.Person.Name,
                    BirthDate = item.Person.BirthDate,
                    Phone = item.Person.Phone,
                    Document = item.Person.Document,
                    Address = new AddressDTO()
                    {
                        CEP = item.Person.Address.CEP,
                        City = item.Person.Address.City,
                        Country = item.Person.Address.Country,
                        District = item.Person.Address.District,
                        Number = item.Person.Address.Number,
                        Street = item.Person.Address.Street
                    },
                    AddressId = item.Person.AddressId,
                    Restaurant = new RestaurantDTO()
                    {
                        Name = item.Person.Restaurant.Name,
                        Description = item.Person.Restaurant.Description,
                        Address = new AddressDTO()
                        {
                            CEP = item.Person.Restaurant.Address.CEP,
                            City = item.Person.Restaurant.Address.City,
                            Country = item.Person.Restaurant.Address.Country,
                            District = item.Person.Restaurant.Address.District,
                            Number = item.Person.Restaurant.Address.Number,
                            Street = item.Person.Restaurant.Address.Street
                        },
                        AddressId = item.Person.Restaurant.AddressId,
                        RestaurantCategory = new RestaurantCategoryDTO()
                        {
                            Name = item.Person.Restaurant.RestaurantCategory.Name
                        },
                        RestaurantCategoryId = item.Person.Restaurant.RestaurantCategoryId
                    },
                    RestaurantId = item.Person.RestaurantId
                },
                PersonId = item.PersonId,
                UserType = new UserTypeDTO()
                {
                    Name = item.UserType.Name
                },
                UserTypeId = item.UserTypeId
            };
        }
    }
}
