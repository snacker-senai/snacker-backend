using Microsoft.IdentityModel.Tokens;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Snacker.Domain.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
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

        public object Login(string email, string password)
        {
            var user = ValidateUser(email, password);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("12D1738B9A6444BB9E04BB535E3B83C9");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.UserType.Name),
                    }),
                    Expires = DateTime.UtcNow.AddHours(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            return null;
        }

        public User ValidateUser(string email, string password)
        {
            User user = _userRepository.ValidateUser(email, password);

            if (user == null) return null;

            return user;
        }
    }
}
