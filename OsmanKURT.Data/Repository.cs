using Microsoft.EntityFrameworkCore;
using OsmanKURT.Data.Contracts;
using System;
using System.Linq;

namespace OsmanKURT.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MainContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(MainContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity obj)
        {
            var dbResult = DbSet.Add(obj);
            return dbResult.Entity;
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual TEntity Update(TEntity obj)
        {
            var dbResult = DbSet.Update(obj);
            return dbResult.Entity;
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
