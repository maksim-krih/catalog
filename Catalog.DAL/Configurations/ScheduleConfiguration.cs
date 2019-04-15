using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Catalog.DAL.Models;

namespace Catalog.DAL.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasData(                
                new Schedule
                {
                    Id = 1,
                    FacilityId = 1,
                    Open = TimeSpan.FromHours(8),
                    Closed = TimeSpan.FromHours(20),
                    WorkingDays = new DayOfWeek[] { DayOfWeek.Wednesday,
                                                    DayOfWeek.Thursday,
                                                    DayOfWeek.Friday,
                                                    DayOfWeek.Saturday}
                },
                new Schedule
                {
                    Id = 2,
                    FacilityId = 2,
                    Open = TimeSpan.FromHours(8),
                    Closed = TimeSpan.FromHours(20),
                    WorkingDays = new DayOfWeek[] { DayOfWeek.Wednesday,
                                                    DayOfWeek.Thursday,
                                                    DayOfWeek.Friday,
                                                    DayOfWeek.Saturday}
                },
                new Schedule
                {
                    Id = 3,
                    FacilityId = 3,
                    Open = TimeSpan.FromHours(8),
                    Closed = TimeSpan.FromHours(20),
                    WorkingDays = new DayOfWeek[] { DayOfWeek.Wednesday,
                                                    DayOfWeek.Thursday,
                                                    DayOfWeek.Friday,
                                                    DayOfWeek.Saturday}
                
                });
        }
    }
}
