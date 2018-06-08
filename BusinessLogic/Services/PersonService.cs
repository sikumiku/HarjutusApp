using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.DTO;
using BusinessLogic.Factories;
using DAL.App.Interfaces;
using Domain;

namespace BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly IPersonFactory _personFactory;

        public PersonService(IAppUnitOfWork uow, IPersonFactory personFactory)
        {
            _uow = uow;
            _personFactory = personFactory;
        }

        public PersonDTO GetPersonByIdCode(string idCode)
        {
            return _personFactory.Create(_uow.People.FindByIdCode(idCode));
        }

        public PersonDTO AddNewPerson(PersonDTO dto)
        {
            var newPerson = _personFactory.Create(dto);
            _uow.People.Add(newPerson);
            _uow.SaveChanges();
            return _personFactory.Create(newPerson);
        }

        public IEnumerable<PersonDTO> GetAllPersons()
        {
            return _uow.People.All().Where(p => p.Active == true)
                .Select(per => _personFactory.Create(per));
        }

        public PersonDTO GetPersonById(int id)
        {
            var p = _uow.People.Find(id);
            if (p == null) return null;

            return _personFactory.Create(p);
        }

        public IEnumerable<PersonDTO> FindPeopleByFirstname(string firstName)
        {
            if (String.IsNullOrEmpty(firstName)) return null;

            return _uow.People.All().Where(x => x.Firstname.Contains(firstName))
                .Select(person => _personFactory.Create(person)).ToList();
        }

        public IEnumerable<PersonDTO> FindPeopleByIdCode(string idCode)
        {
            if (String.IsNullOrEmpty(idCode)) return null;

            return _uow.People.All().Where(x => x.IdCode.Contains(idCode))
                .Select(person => _personFactory.Create(person)).ToList();
        }

        public void UpdatePerson(int id, PersonDTO updatedPerson)
        {
            Person person = _uow.People.Find(id);
            person.Firstname = updatedPerson.Firstname;
            person.Lastname = updatedPerson.Lastname;
            _uow.People.Update(person);
            _uow.SaveChanges();
        }

        public void MarkAsInactive(int id)
        {
            Person person = _uow.People.Find(id);
            person.Active = false;
            _uow.People.Update(person);
            _uow.SaveChanges();
        }

        public void DeletePerson(int id)
        {
            Person person = _uow.People.Find(id);
            _uow.People.Remove(person);
            _uow.SaveChanges();
        }
    }
}
