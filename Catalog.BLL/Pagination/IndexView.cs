﻿using Catalog.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.BLL.ViewModels;

namespace Catalog.BLL.Pagination
{
    public class IndexView
    {
        public PageView PageViewModel { get; set; }
        public IEnumerable<Facility> FacilityModels { get; set; }
        public FilterModel FilterModel { get; set; }
    }
}
