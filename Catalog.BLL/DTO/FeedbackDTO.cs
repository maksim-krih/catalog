using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.DTO
{
    class FeedbackDTO
    {
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime? Date { get; set; }
        public int Rating { get; set; }
        public int FacilityId { get; set; }
    }
}
