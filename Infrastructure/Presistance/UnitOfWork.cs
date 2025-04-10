﻿using Domain.Contracts;
using Domain.Entities;
using Presistance.Data;
using Presistance.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        //private readonly Dictionary<string,object> _repositories;
        private readonly ConcurrentDictionary<string,object> _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            //_repositories = new Dictionary<string,object>();
            _repositories = new ConcurrentDictionary<string,object>();
        }

        //public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        //{   
        //    var type= typeof(TEntity).Name;
        //    if (!_repositories.ContainsKey(type)) 
        //    {
        //        var repository = new GenericRepository<TEntity, Tkey>(_context);
        //        _repositories.Add(type, repository);
        //    }
        //    return (IGenericRepository<TEntity, Tkey>) _repositories[type];

        //}

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        => (IGenericRepository<TEntity, Tkey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, Tkey>(_context));
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
