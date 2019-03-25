using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Catalog.Models.Facility
{
    public class Schedule
    {
        [Key]
        public DateTime Open { get; set; }
        public DateTime Closed { get; set; }
        public DayOfWeek WorkingDays { get; set; }
    }
}
