using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.DAL.Models
{
   // [Table("Facility")]
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FacilityAddress Address { get; set; }
        public Schedule Schedule { get; set; }
        public string FacilityType { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        [RegularExpression(@"0[0-9]{9}")]
        public string Phone { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public Facility()
        {
            Feedbacks = new List<Feedback>();
            Photos = new List<Photo>();
        }

    }
}
