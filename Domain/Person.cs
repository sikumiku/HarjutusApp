using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(128)]
        public string Lastname { get; set; }

        [Required]
        public string IdCode { get; set; }

        public DateTime Birthday { get; set; }

        public bool Active { get; set; } = true;

        public virtual List<Car> Cars { get; set; } = new List<Car>();
    }

}
