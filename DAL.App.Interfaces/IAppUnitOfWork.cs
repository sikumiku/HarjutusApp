using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;

namespace DAL.App.Interfaces
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IPersonRepository People { get; }

        ICarRepository Cars { get; }

    }
}
