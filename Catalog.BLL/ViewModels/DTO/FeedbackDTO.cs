using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.ViewModels.DTO
{
    public class FeedbackDTO
    {            
        public string Message { get; set; }
        public int Rating { get; set; }
        public int Price { get; set; }     
        public string Author { get; set; }
    }
}
