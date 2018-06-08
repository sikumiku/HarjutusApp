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
    public class CarService : ICarService
    {

        private readonly IAppUnitOfWork _uow;
        private readonly ICarFactory _carFactory;

        public CarService(IAppUnitOfWork uow, ICarFactory carFactory)
        {
            _uow = uow;
            _carFactory = carFactory;
        }
        public IEnumerable<CarDTO> GetAllCars()
        {
            return _uow.Cars.All()
                .Select(car => _carFactory.Create(car));
        }

        public CarDTO GetCarById(int id)
        {
            var car = _uow.Cars.Find(id);
            if (car == null) return null;

            return _carFactory.Create(car);
        }

        public IEnumerable<CarDTO> FindCarsByLicensePlate(string licensePlate)
        {
            if (String.IsNullOrEmpty(licensePlate)) return null;

            return _uow.Cars.All().Where(x => x.LicensePlate.Contains(licensePlate))
                .Select(car => _carFactory.Create(car)).ToList();
        }

        public IEnumerable<CarDTO> FindCarsByPersonId(int personId)
        {
            return _uow.Cars.All().Where(x => x.PersonId == personId)
                .Select(car => _carFactory.Create(car)).ToList();
        }

        public List<CarDTO> FindCars(string licensePlate, int personId)
        {
            List<Car> cars = new List<Car>();
            if (licensePlate != null)
            {
                cars = _uow.Cars.FindByLicensePlate(licensePlate);
            } else if (personId != 0)
            {
                cars = _uow.Cars.FindByPersonId(personId);
            }
            
            if (cars == null  || cars.Count == 0) return null;

            var carCollection = new List<CarDTO>();

            foreach (var car in cars)
            {
                carCollection.Add(_carFactory.Create(car));
            }

            return carCollection;
        }

        public CarDTO AddNewCar(CarDTO dto)
        {
            var newCar = _carFactory.Create(dto);
            _uow.Cars.Add(newCar);
            _uow.SaveChanges();
            return _carFactory.Create(newCar);
        }

        public void UpdateCar(int id, CarDTO dto)
        {
            Car car = _uow.Cars.Find(id);
            car.ManufactureYear = dto.ManufactureYear;
            car.CarBrand = dto.CarBrand;
            car.CarModel = dto.CarModel;
            car.LicensePlate = dto.LicensePlate;
            _uow.Cars.Update(car);
            _uow.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            Car car = _uow.Cars.Find(id);
            _uow.Cars.Remove(car);
            _uow.SaveChanges();
        }
    }
}
