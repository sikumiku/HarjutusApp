using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.DTO;
using Domain;

namespace BusinessLogic.Factories
{
    public interface ICarFactory
    {
        CarDTO Create(Car c);
        Car Create(CarDTO dto);
    }
    public class CarFactory : ICarFactory
    {
        public CarDTO Create(Car c)
        {
            return CarDTO.CreateFromDomain(c);
        }

        public Car Create(CarDTO dto)
        {
            return new Car
            {
                ManufactureYear = dto.ManufactureYear,
                CarBrand = dto.CarBrand,
                CarModel = dto.CarModel,
                LicensePlate = dto.LicensePlate,
                PersonId = dto.PersonId
            };
        }
    }
}
