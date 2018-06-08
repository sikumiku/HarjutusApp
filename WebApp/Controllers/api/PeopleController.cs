using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.DTO;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Produces("application/json")]
    [Route("api/v1/People")]
    public class PeopleController : Controller
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/People
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            return Ok(_personService.GetAllPersons());
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            var p = _personService.GetPersonById(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        // GET: api/People/find
        [HttpGet]
        [Route("find")]
        [ProducesResponseType(typeof(List<PersonDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Find(string idCode, string firstname)
        {
            IEnumerable<PersonDTO> persons = new List<PersonDTO>();
            if (firstname != null)
            {
                persons = _personService.FindPeopleByFirstname(firstname);
            } else if (idCode != null)
            {
                persons = _personService.FindPeopleByIdCode(idCode);
            } else
            {
                return Ok(_personService.GetAllPersons());
            }

            if (!persons.Any())
                {
                    return NotFound();
                }
            
            return Ok(persons);
        }

        // POST: api/People
        [HttpPost]
        [ProducesResponseType(typeof(PersonDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody]PersonDTO person)
        {
            if (!ModelState.IsValid) return BadRequest();

            var p = _personService.GetPersonByIdCode(person.IdCode);

            if (p != null)
            {
                return BadRequest("Such person already exists.");
            }

            var newPerson = _personService.AddNewPerson(person);

            //tagastab õige staatuskoodi (201), uue inimese ning õige aadressi, kust seda inimest kätte saab
            return CreatedAtAction("Get", new { id = newPerson.PersonId }, newPerson);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Put(int id, [FromBody]PersonDTO person)
        {
            if (!ModelState.IsValid) return BadRequest();
            var p = _personService.GetPersonById(id);

            if (p == null) return NotFound();
            _personService.UpdatePerson(id, person);

            return NoContent();
        }


        [HttpDelete("deactivate/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Deactivate(int id)
        {
            var p = _personService.GetPersonById(id);
            if (p == null) return NotFound();

            _personService.MarkAsInactive(id);
            return NoContent();
        }

        /// <summary>
        /// Deletes a person by id.
        /// </summary>
        /// <param name="id">ID of person to delete</param>
        /// <response code="204">Person was successfully deleted, no content to be returned</response>
        /// <response code="404">Person not found by given ID</response>
        /// <response code="500">Internal error, unable to process request</response>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            var p = _personService.GetPersonById(id);
            if (p == null) return NotFound();
            _personService.DeletePerson(id);
            return NoContent();
        }
    }
}

