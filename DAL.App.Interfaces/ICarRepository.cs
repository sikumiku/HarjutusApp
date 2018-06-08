using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using Domain;

namespace DAL.App.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        /// <summary>
        /// Check for entity existance by PK value
        /// </summary>
        /// <param name="id">Person PK value</param>
        /// <returns></returns>
        bool Exists(int id);

        List<Car> FindByLicensePlate(string licensePlate);

        List<Car> FindByPersonId(int personId);
    }
}
