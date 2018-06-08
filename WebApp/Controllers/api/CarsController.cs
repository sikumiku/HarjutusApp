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
    [Route("api/v1/cars")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/v1/Cars
        [HttpGet]
        [ProducesResponseType(typeof(List<CarDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            return Ok(_carService.GetAllCars());
        }

        // GET: api/cars/find
        [HttpGet]
        [Route("find")]
        [ProducesResponseType(typeof(List<CarDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Find(string licensePlate, int personId)
        {
            IEnumerable<CarDTO> cars = new List<CarDTO>();
            if (licensePlate != null)
            {
                cars = _carService.FindCarsByLicensePlate(licensePlate);
            } else if (personId != 0)
            {
                cars = _carService.FindCarsByPersonId(personId);
            }
            else
            {
                return Ok(_carService.GetAllCars());
            }

            if (!cars.Any())
            {
                return NotFound();
            }

            return Ok(cars);
        }

        // GET: api/v1/Cars/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(CarDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            var p = _carService.GetCarById(id);
            if (p == null) return NotFound();
            return Ok(p);
        }
        
        // POST: api/v1/Cars
        [HttpPost]
        [ProducesResponseType(typeof(CarDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody]CarDTO car)
        {
            if (!ModelState.IsValid) return BadRequest();

            var newCar = _carService.AddNewCar(car);

            return CreatedAtAction("Get", new { id = newCar.CarId }, newCar);
        }

        // PUT: api/v1/Cars/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Put(int id, [FromBody]CarDTO car)
        {
            if (!ModelState.IsValid) return BadRequest();
            var p = _carService.GetCarById(id);

            if (p == null) return NotFound();
            _carService.UpdateCar(id, car);

            return NoContent();
        }

        /// <summary>
        /// Deletes a person by id.
        /// </summary>
        /// <param name="id">ID of person to delete</param>
        /// <response code="204">Person was successfully deleted, no content to be returned</response>
        /// <response code="404">Person not found by given ID</response>
        /// <response code="500">Internal error, unable to process request</response>
        // DELETE: api/v1/Cars/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            var p = _carService.GetCarById(id);
            if (p == null) return NotFound();
            _carService.DeleteCar(id);
            return NoContent();
        }
    }
}
