using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DAL.Models
{
    //[Table("Feedback")]
    public class Feedback
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }

        public int FacilityId { get; set; }
        public Facility Facility { get; set; }

    }
}
