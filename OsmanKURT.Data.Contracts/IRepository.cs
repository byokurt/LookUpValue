using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmanKURT.Data.Contracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        TEntity Update(TEntity obj);
        void Remove(int id);
    }
}
