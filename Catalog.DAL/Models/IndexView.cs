using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.DAL.Models
{
    public class IndexView
    {
        public PageView PageViewModel { get; set; }
        public IEnumerable<Facility> FacilityModels { get; set; }
    }
}
