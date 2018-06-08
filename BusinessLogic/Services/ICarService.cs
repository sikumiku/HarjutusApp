using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.DTO;

namespace BusinessLogic.Services
{
    public interface ICarService
    {
        IEnumerable<CarDTO> GetAllCars();

        CarDTO GetCarById(int id);

        IEnumerable<CarDTO> FindCarsByLicensePlate(string licensePlate);

        IEnumerable<CarDTO> FindCarsByPersonId(int personId);

        CarDTO AddNewCar(CarDTO dto);

        void UpdateCar(int id, CarDTO dto);

        void DeleteCar(int id);
    }
}
