using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;

namespace DAL.App.Interfaces
{
    public interface IRepositoryProvider
    {
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;

        TRepository GetCustomRepository<TRepository>() where TRepository : class;
    }
}
