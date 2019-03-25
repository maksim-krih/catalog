using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models.Facility
{
    public class FacilityModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public FacilityAddress Address { get; set; }
        public Schedule Schedule { get; set; }
        public Price Price { get; set; }
        public double Rating { get; set; }
        public string Phone { get; set; }
        public FacilityType FacilityType { get; set; }
    }
}
