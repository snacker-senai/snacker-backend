using FluentValidation;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

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
    }
}
