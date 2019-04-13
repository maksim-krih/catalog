using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.DAL.Models
{   
    [Table("Photo")]
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }

        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}
