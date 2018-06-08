using System;
using DAL.Interfaces;

namespace DAL.App.Interfaces
{
    public interface IRepositoryFactory
    {
        Func<IDataContext, object> GetCustomRepositoryFactory<TRepoInterface>() where TRepoInterface : class;

        Func<IDataContext, object> GetStandardRepositoryFactory<TEntity>() where TEntity : class;

    }
}
