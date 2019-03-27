using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DAL.Enums
{
    public class Schedule
    {
        [Key]
        public TimeSpan Open { get; set; }
        public TimeSpan Closed { get; set; }
        public DayOfWeek[] WorkingDays { get; set; }
    }
}
