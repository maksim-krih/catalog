using Catalog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.ViewModels.DTO
{
    //TODO: Data transfer objects are for using in controller methods

    public class FacilityDTO
    {
        public string Name { get; set; }
        public FacilityAddress Address { get; set; }
        public Schedule Schedule { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public string Phone { get; set; }
        public string FacilityType { get; set; }

    }
}
