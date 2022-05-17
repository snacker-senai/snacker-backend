using FluentValidation;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.Insert(obj);
            return obj;
        }

        public virtual void Delete(long id) => _baseRepository.Delete(id);

        public virtual ICollection<object> Get()
        {
            var itens = _baseRepository.Select();
            ICollection<object> result = new List<object>();
            foreach (var item in itens)
            {
                result.Add(item);
            }
            return result;
        }

        public virtual object GetById(long id) => _baseRepository.Select(id);

        public virtual TEntity Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.Update(obj);
            return obj;
        }

        protected static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Null object");

            validator.ValidateAndThrow(obj);
        }
    }
}
