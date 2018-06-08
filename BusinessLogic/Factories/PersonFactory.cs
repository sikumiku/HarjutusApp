using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.DTO;
using Domain;

namespace BusinessLogic.Factories
{
    public interface IPersonFactory
    {
        PersonDTO Create(Person p);
        Person Create(PersonDTO dto);
    }

    public class PersonFactory : IPersonFactory
    {
        public PersonDTO Create(Person p)
        {
            return PersonDTO.CreateFromDomainWithCars(p);
        }

        public Person Create(PersonDTO dto)
        {
            return new Person
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                IdCode = dto.IdCode,
                Birthday = dto.Birthday
            };
        }
    }
}
