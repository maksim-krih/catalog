using Catalog.Models;
﻿using Catalog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class IndexView
    {
        public PageView PageViewModel { get; set; }
        public IEnumerable<Facility> FacilityModels { get; set; }
        public FilterModel FilterModel { get; set; }
    }
}
