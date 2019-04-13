using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Catalog.DAL.Models
{
    public class User 
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        //public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //public int PhotoId { get; set; }
        //public Photo Photo { get; set; }

        public int? Roleid { get; set; }
        public Role UserRole { get; set; }

        public ICollection<Facility> Facilities { get; set; }

        public User()
        {
            Facilities = new List<Facility>();
        }
    }
}
