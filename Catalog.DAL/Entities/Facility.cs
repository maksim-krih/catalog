using System.ComponentModel.DataAnnotations;
using Catalog.DAL.Enums;

namespace Catalog.DAL.Entities
{
    public class Facility
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
