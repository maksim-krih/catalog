using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DAL.Models
{
    //[Table("Schedule")]
    public class Schedule
    {
        public int FacilityId { get; set; }
        public TimeSpan Open { get; set; }
        public TimeSpan Closed { get; set; }
        public DayOfWeek[] WorkingDays { get; set; }
    }
}
