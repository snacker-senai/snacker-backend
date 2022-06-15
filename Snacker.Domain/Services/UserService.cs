using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Address> _addressRepository;
        private readonly IBaseRepository<Person> _personRepository;
        public UserService(IUserRepository userRepository, IBaseRepository<Address> addressRepository, IBaseRepository<Person> personRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _personRepository = personRepository;
        }

        public override User Add<TValidator>(User obj)
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            _personRepository.Insert(obj.Person);
            _userRepository.Insert(obj);
            return obj;
        }

        public override void Delete(long id)
        {
            var user = _userRepository.Select(id);
            _userRepository.Delete(id);
            _personRepository.Delete(user.PersonId);
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
