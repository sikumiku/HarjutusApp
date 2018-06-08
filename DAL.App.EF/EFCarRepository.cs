using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.App.Interfaces;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class EFCarRepository : EFRepository<Car>, ICarRepository
    {
        public EFCarRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(e => e.CarId == id);
        }

        public List<Car> FindByLicensePlate(string licensePlate)
        {
            return RepositoryDbSet.Where(e => e.LicensePlate == licensePlate).ToList();
        }


        public List<Car> FindByPersonId(int personId)
        {
            return RepositoryDbSet.Where(e => e.PersonId == personId).ToList();
        }
    }
}
