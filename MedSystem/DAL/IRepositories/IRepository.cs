﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(FindOptions? findOptions = null);
        T FindOne(Expression<Func<T, bool>> predicate, FindOptions? findOptions = null);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, FindOptions? findOptions = null);
        void Add(T entity);
        void AddMany(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateMany(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        void DeleteMany(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);

    }
}
