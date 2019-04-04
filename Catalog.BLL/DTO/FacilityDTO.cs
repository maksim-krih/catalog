using Catalog.DAL.Entities;
using Catalog.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.DTO
{
    class FacilityDTO
    {
        public string Name { get; set; }
        public FacilityAddress Address { get; set; }
        public Schedule Schedule { get; set; }
        public Price Price { get; set; }
        public double Rating { get; set; }
        public string Phone { get; set; }
        public FacilityType FacilityType { get; set; }
               
    }
}
