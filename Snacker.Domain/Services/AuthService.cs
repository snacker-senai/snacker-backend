using Microsoft.IdentityModel.Tokens;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Snacker.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
