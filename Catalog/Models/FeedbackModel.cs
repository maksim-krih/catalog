using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class FeedbackModel
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
