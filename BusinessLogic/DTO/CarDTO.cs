using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Domain;

namespace BusinessLogic.DTO
{
    public class CarDTO
    {
        public int CarId { get; set; }
        [Required]
        [MaxLength(4)]
        public string ManufactureYear { get; set; }

        //automark
        [Required]
        [MaxLength(54)]
        public string CarBrand { get; set; }

        //automudel
        [Required]
        [MaxLength(54)]
        public string CarModel { get; set; }

        [Required]
        [MaxLength(10)]
        public string LicensePlate { get; set; }

        [Required]
        public int PersonId { get; set; }

        public static CarDTO CreateFromDomain(Car car)
        {
            return new CarDTO
            {
                CarId = car.CarId,
                ManufactureYear = car.ManufactureYear,
                CarBrand = car.CarBrand,
                CarModel = car.CarModel,
                LicensePlate = car.LicensePlate,
                PersonId = car.PersonId
            };
        }


    }
}
