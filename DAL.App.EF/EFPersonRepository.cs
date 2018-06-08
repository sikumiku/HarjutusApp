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
    public class EFPersonRepository : EFRepository<Person>, IPersonRepository
    {
        public EFPersonRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(e => e.PersonId == id);
        }

        public Person FindByIdCode(string idCode)
        {
            return RepositoryDbSet
                .SingleOrDefault(x => x.IdCode == idCode);
        }

        // kui tahame kaasa anda ka kontaktid inimesega, peaks üle kirjutama EFRepository Find meetodi, antud meetod peaks olema virtual
        public override Person Find(params object[] id)
        {
            return RepositoryDbSet
                .Include(x => x.Cars)
                .SingleOrDefault(x => (int)id[0] == x.PersonId);
        }

        public override IEnumerable<Person> All()
        {
            return RepositoryDbSet.AsQueryable()
                .Include(x => x.Cars).ToList();
        }
    }
}
