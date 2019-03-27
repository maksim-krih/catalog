using System.ComponentModel.DataAnnotations;

namespace Catalog.DAL.Enums
{
    public class FacilityAddress
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        //public int AppartmentNumber { get; set; }
        public int ZipCode { get; set; }
    }
}
