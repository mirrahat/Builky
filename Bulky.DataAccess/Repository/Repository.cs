﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDBContext _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDBContext db)
        {
            _db = db;
            this._dbSet=_db.Set<T>();
           // _db.Products.Include(u => u.Category);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

      

        T IRepository<T>.Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        //we will recevie include value as separated values Category,CoverType
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> ? filter= null,string? includeProperties=null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null) {
                query = query.Where(filter); 
            }
            if (!string.IsNullOrEmpty(includeProperties)) 
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(includeProp);
                }
            }

                return query.ToList();
        }

        public void Remove(T entity)
        {
           _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

       
    }
}
