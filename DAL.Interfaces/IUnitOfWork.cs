using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();

        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;

        TRepositoryInterface GetCustomRepository<TRepositoryInterface>() where TRepositoryInterface : class;

    }
}
