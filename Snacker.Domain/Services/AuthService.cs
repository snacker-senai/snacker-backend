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
        private readonly IThemeRepository _themeRepository;
        private readonly byte[] key = Encoding.ASCII.GetBytes("12D1738B9A6444BB9E04BB535E3B83C9");

        public AuthService(IUserRepository userRepository, IBaseRepository<Table> tableRepository, IBillRepository billRepository, IThemeRepository themeRepository)
        {
            _userRepository = userRepository;
            _tableRepository = tableRepository;
            _billRepository = billRepository;
            _themeRepository = themeRepository;
        }

        public object ChangePassword(string email, string newPassword, string oldPassword)
        {
            var user = ValidateUser(email, oldPassword);
            if (user != null)
            {
                user.Password = newPassword;
                user.ChangePassword = false;
                _userRepository.Update(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.UserType.Name),
                            new Claim("RestaurantId", user.Person.RestaurantId.ToString())
                    }, JwtBearerDefaults.AuthenticationScheme),
                    Expires = DateTime.UtcNow.AddYears(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(createdToken);
                return new { token, user.ChangePassword };
            }
            return null;
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
                var theme = _themeRepository.SelectFromRestaurant(table.RestaurantId);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "Cliente"),
                        new Claim("TableId", table.Id.ToString()),
                        new Claim("RestaurantId", table.RestaurantId.ToString()),
                        new Claim("BillId", billId.ToString()),
                        new Claim("Color", theme.Any() ? theme.FirstOrDefault().Color : "#1154A3")
                    }, JwtBearerDefaults.AuthenticationScheme),
                    Expires = DateTime.UtcNow.AddYears(6),
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
            string token = null;
            string message = null;
            var user = ValidateUser(email, password);
            if (user != null)
            {
                if (!user.Person.Restaurant.Active)
                {
                    message = "Não é possível se conectar pois o restaurante está inativo";
                    return new { token, user.ChangePassword, message };
                }
                if (!user.ChangePassword)
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
                        Expires = DateTime.UtcNow.AddYears(6),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                    token = tokenHandler.WriteToken(createdToken);
                    return new { token, user.ChangePassword, message };
                }
                return new { token, user.ChangePassword, message };
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
