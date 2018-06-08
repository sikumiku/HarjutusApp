using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.App.EF
{
    public class EFRepositoryFactory : IRepositoryFactory
    {
        private readonly Dictionary<Type, Func<IDataContext, object>> _customRepositoryFactories
            = GetCustomRepoFactories();

        private static Dictionary<Type, Func<IDataContext, object>> GetCustomRepoFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>()
            {
                {typeof(IPersonRepository), (dataContext) => new EFPersonRepository(dataContext as ApplicationDbContext) },
                {typeof(ICarRepository), (dataContext) => new EFCarRepository(dataContext as ApplicationDbContext) },
            };
        }

        public Func<IDataContext, object> GetCustomRepositoryFactory<TRepoInterface>() where TRepoInterface : class
        {
            _customRepositoryFactories.TryGetValue(
                typeof(TRepoInterface),
                out Func<IDataContext, object> factory
            );
            return factory;
        }

        public Func<IDataContext, object> GetStandardRepositoryFactory<TEntity>() where TEntity : class
        {

            return (dataContext) => new EFRepository<TEntity>(dataContext as ApplicationDbContext);
        }
    }
}
