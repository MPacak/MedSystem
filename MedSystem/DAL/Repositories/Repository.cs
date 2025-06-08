using DAL.IRepositories;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MedicalSystemContext _context;

        public Repository(MedicalSystemContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            
        }

        public void AddMany(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
           return _context.Set<T>().Any(predicate);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
           return _context.Set<T>().Count(predicate);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteMany(Expression<Func<T, bool>> predicate)
        {
           var entities = Find(predicate);
            _context.Set<T>().RemoveRange(entities);
        }


        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).Where(predicate);
        }

        public T FindOne(Expression<Func<T, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).FirstOrDefault(predicate)!;
        }

        public IQueryable<T> GetAll(FindOptions? findOptions = null)
        {
            return Get(findOptions);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);   
        }

        public void UpdateMany(Expression<Func<T, bool>> predicate)
        {
            var entities = Find(predicate);
            _context.UpdateRange(entities);
        }

        private DbSet<T> Get(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            var entity = _context.Set<T>();
            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes().AsNoTracking();
            }
            else if (findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes();
            }
            else if (findOptions.IsAsNoTracking)
            {
                entity.AsNoTracking();
            }
            return entity;
        }
    }
}

