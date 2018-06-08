﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces;
using DAL.Interfaces;

namespace DAL.App.EF
{
    public class EFRepositoryProvider : IRepositoryProvider
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IRepositoryFactory _repositoryFactory;

        // here we keep our already created repos!
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();

        public EFRepositoryProvider(IDataContext dataContext, IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _applicationDbContext = dataContext as ApplicationDbContext;
            if (_applicationDbContext == null)
            {
                throw new NullReferenceException("No EF dbcontext found in UOW");
            }
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            return GetOrMakeRepository<IRepository<TEntity>>
                (_repositoryFactory.GetStandardRepositoryFactory<TEntity>());

        }

        private TRepository GetOrMakeRepository<TRepository>(Func<IDataContext, object> factory)
            where TRepository : class
        {
            if (_repositoryCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository)repo;
            }

            if (factory == null)
            {
                throw new ArgumentNullException($"No factory found for type {typeof(TRepository).Name}");
            }

            repo = factory(_applicationDbContext);

            _repositoryCache.Add(typeof(TRepository), repo);

            return (TRepository)repo;

        }

        public TRepository GetCustomRepository<TRepository>()
            where TRepository : class
        {
            return GetOrMakeRepository<TRepository>(
                _repositoryFactory.GetCustomRepositoryFactory<TRepository>());
        }
    }
}
