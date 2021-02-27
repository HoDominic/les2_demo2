using System;
using System.ComponentModel.DataAnnotations;

namespace les2_demo2.Models
{
    public class VaccinationRegistration
    {
        public Guid VaccinationRegistration { get; set; }



        [Required]
        public string Name { get; set; }


        [Required]
        public string FirstName { get; set; }


        [EmailAddress]
        public string Email { get; set; }

        [Range(18, 120)]
        public int Age { get; set; }


        [Required]
        public string VaccinationDate { get; set; }

        [Required]
        public Guid VaccinTypeId { get; set; }


        [Required]
        public Guid VaccinLocationId { get; set; }

    }
}
