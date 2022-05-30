using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IBaseRepository<Table> _tableRepository;
        private readonly IBillRepository _billRepository;
        private readonly byte[] key = Encoding.ASCII.GetBytes("12D1738B9A6444BB9E04BB535E3B83C9");

        public AuthService(IUserRepository userRepository, IBaseRepository<Table> tableRepository, IBillRepository billRepository)
        {
            _userRepository = userRepository;
            _tableRepository = tableRepository;
            _billRepository = billRepository;
        }

        public object GenerateClientToken(long tableId)
        {
            var table = _tableRepository.Select(tableId);
            var bills = _billRepository.SelectActiveFromTable(table.Id);
            long billId;
            if (bills.Any())
            {
                billId = bills.First().Id;
            }
            else
            {
                _billRepository.Insert(new Bill { Active = true, TableId = table.Id });
                billId = _billRepository.SelectActiveFromTable(table.Id).First().Id;
            }
            if (table != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "Cliente"),
                        new Claim("TableId", table.Id.ToString()),
                        new Claim("RestaurantId", table.RestaurantId.ToString()),
                        new Claim("BillId", billId.ToString())
                    }, JwtBearerDefaults.AuthenticationScheme),
                    Expires = DateTime.UtcNow.AddHours(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            return null;
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
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.UserType.Name),
                        new Claim("RestaurantId", user.Person.RestaurantId.ToString())
                    }, JwtBearerDefaults.AuthenticationScheme),
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
