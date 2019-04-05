using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.DAL.Models
{
    [Table("FacilityAddress")]
    public class FacilityAddress
    {
        [Key]
        public int Id { get; set; }

        public int FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; } 
        public int ZipCode { get; set; }
    }
}
