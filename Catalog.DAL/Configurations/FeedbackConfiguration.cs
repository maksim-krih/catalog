using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Catalog.DAL.Models;

namespace Catalog.DAL.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasData(
                new List<Feedback>
                        {
                            new Feedback
                            {
                                Id = 1,
                                FacilityId = 1,
                                Author = "Anonynous",
                                Date = DateTime.Now,
                                Rating = 4,
                                Message = "Feedback message"
                            },
                             new Feedback
                            {
                                Id = 2,
                                FacilityId = 1,
                                Author = "Anonynous 2",
                                Date = DateTime.Now,
                                Rating = 3,
                                Message = "Feedback message 2"
                            },
                        });

        }
    }
}
