using System;
using System.ComponentModel.DataAnnotations;

namespace les2_demo2.Models
{
    public class VaccinType
    {
        public Guid VaccinTypeId { get; set; }

        public string Name { get; set; }
    }
}
