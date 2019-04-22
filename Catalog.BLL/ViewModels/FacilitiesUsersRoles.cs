using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Models;

namespace Catalog.BLL.ViewModels
{
    public class FacilitiesUsersRoles
    {
        public IEnumerable<Facility> Facilities { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
