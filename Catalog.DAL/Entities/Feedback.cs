using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DAL.Entities
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
    }
}
