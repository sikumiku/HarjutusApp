using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Domain;

namespace BusinessLogic.DTO
{
    public class PersonDTO //:BaseDTO<PersonDTO, Person>
    {
        public int PersonId { get; set; }
        [MinLength(3)]
        [MaxLength(25)]
        public string Firstname { get; set; }
        [MinLength(3)]
        [MaxLength(25)]
        public string Lastname { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string IdCode { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public int Age { get; set; }

        public List<CarDTO> Cars { get; set; }

        public static PersonDTO CreateFromDomain(Person p)
        {
            if (p == null) return null;

            return new PersonDTO()
            {
                PersonId = p.PersonId,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                IdCode = p.IdCode,
                Birthday = p.Birthday,
                Age = DateTime.Now.Year - p.Birthday.Year
            };
        }

        public static PersonDTO CreateFromDomainWithCars(Person p)
        {
            var person = CreateFromDomain(p);
            if (person == null) return null;

            person.Cars = p?.Cars?
                .Select(CarDTO.CreateFromDomain).ToList();
            return person;

        }
    }
}
