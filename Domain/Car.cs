using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Car
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

        public DateTime UpdateTime { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

    }
}
