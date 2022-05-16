using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MySqlContext _mySqlContext;

        public BaseRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public virtual void Insert(TEntity obj)
        {
            _mySqlContext.Set<TEntity>().Add(obj);
            _mySqlContext.SaveChanges();
        }

        public virtual void Update(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mySqlContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            _mySqlContext.Set<TEntity>().Remove(Select(id));
            _mySqlContext.SaveChanges();
        }

        public virtual ICollection<TEntity> Select() =>
            _mySqlContext.Set<TEntity>().ToList();

        public virtual TEntity Select(int id) =>
            _mySqlContext.Set<TEntity>().Find(id);

    }
}
