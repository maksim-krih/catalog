using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.DAL.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        
        public int FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }

        public TimeSpan Open { get; set; }
        public TimeSpan Closed { get; set; }
        //TODO: make this mapped
        //Exceptions: DayOfWeek[] is not mappable
        [NotMapped]
        public DayOfWeek[] WorkingDays { get; set; }
    }
}
