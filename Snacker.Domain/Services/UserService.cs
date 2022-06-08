using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System.Collections.Generic;

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
    }
}
