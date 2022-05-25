using Microsoft.IdentityModel.Tokens;
using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                var dto = new UserDTO(item);
                result.Add(dto);
            }
            return result;
        }

        public override object GetById(long id)
        {
            var item = _userRepository.Select(id);
            return new UserDTO(item);
        }

        public ICollection<object> GetFromRestaurant(long restaurantId)
        {
            var itens = _userRepository.SelectFromRestaurant(restaurantId);
            var result = new List<object>();
            foreach (var item in itens)
            {
                var dto = new UserDTO(item);
                result.Add(dto);
            }
            return result;
        }

        public object GetFromRestaurantById(long restaurantId, long id)
        {
            var item = _userRepository.SelectFromRestaurantById(restaurantId, id);
            return new UserDTO(item);
        }

        public string GetTokenValue(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token).Claims.First(c => c.Type == claimType).Value;
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
                    new Claim("RestaurantId", user.Person.RestaurantId.ToString())
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
