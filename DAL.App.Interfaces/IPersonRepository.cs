using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using Domain;

namespace DAL.App.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        /// <summary>
        /// Check for entity existance by PK value
        /// </summary>
        /// <param name="id">Person PK value</param>
        /// <returns></returns>
        bool Exists(int id);

        Person FindByIdCode(string idCode);
    }
}
