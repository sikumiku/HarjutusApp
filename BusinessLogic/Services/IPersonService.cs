using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.DTO;

namespace BusinessLogic.Services
{
    public interface IPersonService
    {
        IEnumerable<PersonDTO> GetAllPersons();

        PersonDTO GetPersonById(int id);

        IEnumerable<PersonDTO> FindPeopleByFirstname(string firstName);

        IEnumerable<PersonDTO> FindPeopleByIdCode(string idCode);


        PersonDTO GetPersonByIdCode(string idCode);

        PersonDTO AddNewPerson(PersonDTO dto);

        void UpdatePerson(int id, PersonDTO dto);

        void MarkAsInactive(int id);

        void DeletePerson(int id);
    }
}
