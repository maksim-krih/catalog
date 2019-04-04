using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class IndexViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<FacilityModel> FacilityModels { get; set; }
    }
}
